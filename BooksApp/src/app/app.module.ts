import { JwtInterceptor } from './_interceptors/jwt-interceptor.service';
import { ContactService } from './_services/contact.service';
import { NgxSpinnerModule } from 'ngx-spinner';
import { SharedModule } from './_modules/shared.module';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { AuthorInfoComponent } from './show-author/author-info.component';
import { AuthorFormComponent } from './modals/add-author/author-form.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { NavbarComponent } from './navbar/navbar.component';
import { AppRoutingModule} from './app-routing.module'
import { NgbModule } from '@ng-bootstrap/ng-bootstrap'
import { RegisterComponent } from './register/register.component';
import { BooksComponent } from './show-boks/books.component';
import { FooterComponent } from './footer/footer.component';
import { LoginComponent } from './login/login.component';
import { BookDetailsComponent } from './modals/book-details/book-details.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { RolesModalComponent } from './modals/roles-modal/roles-modal.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { UploadBookComponent } from './upload-book/upload-book.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { CommonModule } from '@angular/common';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { AddedBooksComponent } from './added-books/added-books.component';
import { BooksForRentCardComponent } from './added-books/books-for-rent-card/books-for-rent-card.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { RentedBooksComponent } from './rented-books/rented-books.component';
import { RentedBooksCardComponent } from './rented-books/rented-books-card/rented-books-card.component';
import { UnrentedBooksComponent } from './unrented-books/unrented-books.component';
import { UnrentedBooksCardComponent } from './unrented-books/unrented-books-card/unrented-books-card.component';
import { ContactFormComponent } from './contact-form/contact-form.component';
import { UpdateBookComponent } from './modals/update-book/update-book.component';
import { AddImageComponent } from './unrented-books/add-image/add-image.component';




 @NgModule({
  declarations: [ 
    AppComponent,
    AuthorInfoComponent,
    AuthorFormComponent,
    HomeComponent,
    NavbarComponent,
    RegisterComponent,
    BooksComponent,
    FooterComponent,
    LoginComponent,
    BookDetailsComponent,
    AdminPanelComponent,
    HasRoleDirective,
    UserManagementComponent,
    RolesModalComponent,
    TextInputComponent,
    UploadBookComponent,
    AddedBooksComponent,
    BooksForRentCardComponent,
    RentedBooksComponent,
    RentedBooksCardComponent,
    UnrentedBooksComponent,
    UnrentedBooksCardComponent,
    ContactFormComponent,
    UpdateBookComponent,
    AddImageComponent,


  ],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    NgbModule,
    SharedModule,
    ReactiveFormsModule,
    NgxPaginationModule,
    Ng2SearchPipeModule,
    NgxSpinnerModule,
    HttpClientModule,
 
    
  ],
  exports: [],
  
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
    ContactService,
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
