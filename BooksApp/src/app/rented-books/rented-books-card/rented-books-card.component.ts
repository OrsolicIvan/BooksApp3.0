import { BookDetailsComponent } from './../../modals/book-details/book-details.component';
import { AccountService } from './../../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Component, Input, OnInit } from '@angular/core';
import { Book, GetOwnedBooks, UnRentBookClass } from 'src/app/_models/book';
import { BooksService } from 'src/app/_services/books.service';
import { User } from 'src/app/_models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-rented-books-card',
  templateUrl: './rented-books-card.component.html',
  styleUrls: ['./rented-books-card.component.css'],
})
export class RentedBooksCardComponent implements OnInit {
  @Input() public book: Book;
  noPicture: string;
  books: any;
  bookDetails: any;
  bookDetail: any;

  constructor(
    private booksService: BooksService,
    private toastr: ToastrService,
    private accountService: AccountService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  onSubmit() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    const id = this.accountService.getId(user);
    this.booksService.unRentformData.renterId = id;
    this.booksService.unRentformData.bookId = this.book.bookId;
    console.log(this.book);
    if (confirm('Jeste li sigurni da želite odjaviti knjigu?')) {
      this.unRentBook();
      this.toastr.success('Knjiga je uspješno odjavljena');
      window.location.reload();
    }
  }

  unRentBook() {
    this.booksService.unRentBook().subscribe(
      (res) => {
        this.booksService.rentformData = new UnRentBookClass();
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
