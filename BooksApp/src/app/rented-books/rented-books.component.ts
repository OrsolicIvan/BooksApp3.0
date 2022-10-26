import { BooksService } from 'src/app/_services/books.service';
import { Component, OnInit } from '@angular/core';
import { Book, GetOwnedBooks } from '../_models/book';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';

@Component({
  selector: 'app-rented-books',
  templateUrl: './rented-books.component.html',
  styleUrls: ['./rented-books.component.css']
})
export class RentedBooksComponent implements OnInit {
  books: GetOwnedBooks[];
  image: any;
  user: any;

  
  constructor(private booksService: BooksService, private accountService: AccountService) { }

  ngOnInit(): void {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.loadBooks();
  }

  loadBooks() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    const id = this.accountService.getId(user);
    this.booksService.getUserRentedBooks(id).subscribe((books: GetOwnedBooks[]) => {
      this.books = books;
     console.log(books)
    })
  }

}
