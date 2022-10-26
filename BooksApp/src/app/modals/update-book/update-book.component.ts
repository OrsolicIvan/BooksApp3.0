import { BooksService } from './../../_services/books.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AccountService } from 'src/app/_services/account.service';
import { BookClass } from 'src/app/_models/book';
import { Book } from 'src/app/_models/book';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-update-book',
  templateUrl: './update-book.component.html',
  styleUrls: ['./update-book.component.css']
})
export class UpdateBookComponent implements OnInit {
  uploadForm: FormGroup;
  model: any = {}
  @Input() public book: Book;
  constructor( public bsModalRef: BsModalRef, public accountService: AccountService,
    public bookService: BooksService, public toastr: ToastrService) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  cancel(){
    this.bsModalRef.hide();
  }

  initializeForm() {
    this.uploadForm = new FormGroup({
      bookId: new FormControl(this.book.bookId),
      bookTitle: new FormControl(this.model.bookTitle, Validators.required ),
      bookYear: new FormControl(this.model.bookYear, [ Validators.required, Validators.minLength(4), Validators.maxLength(4)]),
    });

  }

  updateBook() {
    console.log(this.book);
    this.model = this.uploadForm.value
    this.bookService.upBookformData.bookId = this.book.bookId;
    this.bookService.updateBook(this.model).subscribe(
      (res) => {
        this.bookService.upBookformData = new BookClass();
        this.toastr.success('Knjiga uspješno ažurirana');
        window.location.reload();
      },
      err => {
        console.log(err);
        console.log(this.model);
      }
    );
  }
  numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;

  }


}
