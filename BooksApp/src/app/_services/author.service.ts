import { Injectable } from '@angular/core';
import { Author } from '../_models/author.model';
import { HttpClient } from "@angular/common/http";
import { Book, BookClass } from '../_models/book';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  baseUrl = 'http://localhost:32679/api/';

  constructor(private http:HttpClient) { }

  readonly baseURL = 'http://localhost:32679/api/Author'
  formData:Author = new Author();
  list: Author[];
  formDataa:Book= new BookClass();

  postAuthor(){
   return this.http.post(this.baseUrl + 'Author/add-author',this.formData);
  }
  putAuthor(){
    return this.http.put(`${this.baseURL}/${this.formData.authorId}`,this.formData);
   }

   deleteAuthor(id:number){
    return this.http.delete(`${this.baseURL}/${id}`);
   }

  refreshlist(){
    this.http.get(this.baseURL)
      .toPromise()
      .then(res => this.list = res as Author[]);
  }

  addAuthor(model: any){
    return this.http.post<Author>(this.baseUrl + 'Author/add-author', model)
        }

  addAuthors(model: any){
    return this.http.post<Author>(this.baseUrl + 'BookAuthor', model)
        }
}
