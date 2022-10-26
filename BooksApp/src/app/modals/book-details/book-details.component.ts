import { Content } from '@angular/compiler/src/render3/r3_ast';
import { Component, Input, OnInit } from '@angular/core';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { BooksService } from 'src/app/_services/books.service';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {
  books: any;  
  book: any;
  
  constructor(public bsModalRef: BsModalRef, private booksService:BooksService) { }

  ngOnInit(): void {
    
  }

  cancel(){
    this.bsModalRef.hide();
  }

  

}
