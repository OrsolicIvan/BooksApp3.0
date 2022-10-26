import { ToastrService } from 'ngx-toastr';
import { GetOwnedBooks } from './../../_models/book';
import { AccountService } from './../../_services/account.service';
import { BooksService } from 'src/app/_services/books.service';
import { Component, Input, OnInit } from '@angular/core';
import { Book } from 'src/app/_models/book';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AuthorFormComponent } from 'src/app/modals/add-author/author-form.component';
import { UpdateBookComponent } from 'src/app/modals/update-book/update-book.component';
import { AddImageComponent } from '../add-image/add-image.component';
import { HttpBackend, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-unrented-books-card',
  templateUrl: './unrented-books-card.component.html',
  styleUrls: ['./unrented-books-card.component.css']
})
export class UnrentedBooksCardComponent implements OnInit {
  @Input() book: GetOwnedBooks;
  noPicture: string;
  books: any; 
  bsModalRef: BsModalRef;
 

  constructor(private booksService: BooksService, private accountService: AccountService,
    private toastr: ToastrService, private modalService: BsModalService) { }
    
  ngOnInit(): void { 
  }

  deleteBook(id: number){
    if(confirm('Jeste li sigurni da zelite obrisati ovu knjigu?'))
  {
    this.booksService.deleteBook(id)
    .subscribe(
      res => {
       window.location.reload()
        this.toastr.error("Knjiga uspjesno obrisana")
        
      },
      err => {console.log(err)},
    )
    window.location.reload();
   
  }
}

openDetailsModal(book: Book) {
  const initialState = {
    class: 'modal-dialog-centered',
    initialState: {
      book
    }
  };
  this.bsModalRef = this.modalService.show(AuthorFormComponent, initialState);
  this.bsModalRef.content.closeBtnName = 'Close';
}
openUpdateModal(book: Book) {
  const initialState = {
    class: 'modal-dialog-centered',
    initialState: {
      book
    }
  };
  this.bsModalRef = this.modalService.show(UpdateBookComponent, initialState);
  this.bsModalRef.content.closeBtnName = 'Close';
}

openDialog(book: Book){
  const config={
    class: 'modal-dialog-centered',
    initialState: {
      book
    }
  }
  this.bsModalRef = this.modalService.show(AddImageComponent, config);
}


  



}
