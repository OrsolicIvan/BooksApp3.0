import { Book, BookUpdateClass, GetOwnedBooks, RentBookClass, UnRentBookClass } from './../_models/book';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { BookClass } from "../_models/book";
import { Observable } from 'rxjs';
import { Author } from '../_models/author.model';

const baseUrl = 'https://api.itbook.store/1.0/search/new';
const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token,
  }),
};

@Injectable({
    providedIn: 'root'
})

export class BooksService {
  baseUrl = environment.apiUrl;
  rentformData: RentBookClass = new RentBookClass();
  unRentformData: UnRentBookClass = new UnRentBookClass();
  upBookformData: BookUpdateClass = new BookUpdateClass();
  formData:Author= new Author();
  bookformData:BookClass= new BookClass();
  list: Book[];
  constructor(private httpClient: HttpClient){}


  getAllBooks(): Observable<any> {
    return this.httpClient.get(baseUrl);
  }
  
  postBook(bookForm: any){
    console.log(bookForm)
    return this.httpClient.post(this.baseUrl + `Books/UserAddBook` , bookForm);
  } 

  

  getBooks(){
    return this.httpClient.get<Book[]>(this.baseUrl + 'Books/unRentedBooks');
  }

  rentBook(){
    return this.httpClient.put(this.baseUrl + 'Books/RentBook', this.rentformData);
  }

  getImages(title: string): Observable<any> {
    return this.httpClient.get('https://api.itbook.store/1.0/search/new/' + title)
  }

  getRentedBooks(){
    return this.httpClient.get<Book[]>(this.baseUrl + 'Books/RentedBooks');
  }
  getUserRentedBooks(id: number) {
    return this.httpClient.get<Book[]>(
      this.baseUrl + `Books/RenterId/` + id
    );
  }

  unRentBook(){
    return this.httpClient.put(this.baseUrl + 'Books/unRentBook', this.unRentformData);
  }

  deleteBook(id: number){
    return this.httpClient.delete(this.baseUrl + `Books/Delete/`+ id);
  }

  getOwnedBooks(id: number){
    return this.httpClient.get<Book[]>(
      this.baseUrl + `Books/OwnerId/` + id
    );
  }

  postBookAuthor(authorForm: any){
    return this.httpClient.post(this.baseUrl + `BookAuthor/AddBookAuthor` ,authorForm);
  }

  updateBook(uploadForm){
    return this.httpClient.put(this.baseUrl + `Books/UpdateBook` ,uploadForm);
  }

}