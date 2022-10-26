import { ToastrService } from 'ngx-toastr';
import { AccountService } from './../../_services/account.service';
import { Component, Input, OnInit } from '@angular/core';
import { Book, GetOwnedBooks, RentBookClass } from 'src/app/_models/book';
import { User } from 'src/app/_models/user';
import { BooksService } from 'src/app/_services/books.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-books-for-rent-card',
  templateUrl: './books-for-rent-card.component.html',
  styleUrls: ['./books-for-rent-card.component.css'],
})
export class BooksForRentCardComponent implements OnInit {
  @Input() public book: Book;
  noPicture: string;
  books: any;
  bookDetails: any;
  bookDetail: any;

  constructor(
    private accountService: AccountService,
    private booksService: BooksService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  setDefaultPic() {
    this.noPicture = 'assets/noimg.jpg';
  }

  onSubmit() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    const id = this.accountService.getId(user);
    this.booksService.rentformData.renterId = id;
    this.booksService.rentformData.bookId = this.book.bookId;
    console.log(this.book);
    if (confirm('Jeste li sigurni da želite iznajmiti ovu knjigu?')) {
      this.rentBook();
      this.toastr.success('Knjiga je uspješno iznajmljena');

      window.location.reload();
    }
  }

  rentBook() {
    this.booksService.rentBook().subscribe(
      (res) => {
        this.booksService.rentformData = new RentBookClass();
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
