import { HttpBackend, HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { Book } from '../_models/book';
import { BooksService } from '../_services/books.service';

@Component({
  selector: 'app-added-books',
  templateUrl: './added-books.component.html',
  styleUrls: ['./added-books.component.css']
})
export class AddedBooksComponent implements OnInit {
  books: Book[];
  image: any;
  constructor(private httpClient: HttpClient, private booksService: BooksService) { }
  
  ngOnInit(): void {
    this.loadBooks();
    console.log(this.books);
  }

  loadBooks() {
    
    this.booksService.getBooks().subscribe(books=> {
      this.books = books['value'];
      console.log(this.books)
     
    })
  }


}
