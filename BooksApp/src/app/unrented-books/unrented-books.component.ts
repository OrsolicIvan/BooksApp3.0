import { HttpBackend, HttpClient } from '@angular/common/http';
import { AccountService } from './../_services/account.service';
import { BooksService } from 'src/app/_services/books.service';
import { Component, OnInit } from '@angular/core';
import { Book, GetOwnedBooks } from '../_models/book';
import { User } from '../_models/user';

@Component({
  selector: 'app-unrented-books',
  templateUrl: './unrented-books.component.html',
  styleUrls: ['./unrented-books.component.css']
})
export class UnrentedBooksComponent implements OnInit {
  books: GetOwnedBooks[];
  user: any;


  constructor(private booksService: BooksService, private accountService: AccountService) { }

  ngOnInit(): void {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.loadOwnedBooks();  

  }

  loadOwnedBooks() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    const id = this.accountService.getId(user);
    this.booksService.getOwnedBooks(id).subscribe((books: GetOwnedBooks[]) => {
      this.books = books;
      
    });
  }
  

}
