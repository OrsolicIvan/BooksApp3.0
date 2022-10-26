    using Books.Extensions;
using Books.Interfaces;
using Books.Models;
using Books.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Books
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public object ValidateIssuerSigningKey { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(Configuration);
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Books", Version = "v1" });
            });
            services.AddCors(options => {
            options.AddPolicy("EnableCORS", builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
            });
            services.AddIdentityServices(Configuration);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(appBuilder =>
              {
                  appBuilder.Run(async context =>
                  {
                      var logger = appBuilder.ApplicationServices.GetRequiredService<ILogger<Startup>>();
                      var feature = context.Features.Get<IExceptionHandlerFeature>();
                      if (feature.Error is not null)
                      {
                          logger.LogError(feature.Error, "Exception");
                          context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                          context.Response.ContentType = "application/json";
                          await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
                          {
                              error = "Something went wrong",
                              detail = feature.Error.Message
                          }));
                      }
                  });
              });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Books v1"));
            }

            app.UseRouting();

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}