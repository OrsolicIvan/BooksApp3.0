import { BsModalRef } from 'ngx-bootstrap/modal';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, NgForm, ValidatorFn, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Author } from 'src/app/_models/author.model';
import { AuthorService } from 'src/app/_services/author.service';
import { Book, BookClass, GetOwnedBooks } from '../../_models/book';
import { AccountService } from '../../_services/account.service';
import { User } from '../../_models/user';
import { BooksService } from '../../_services/books.service';

@Component({
  selector: 'app-author-form',
  templateUrl: './author-form.component.html',
  styles: [
  ]
})
export class AuthorFormComponent implements OnInit {
  submitForm: FormGroup;
  book: BookClass;
  author: Author;

  constructor(public service:AuthorService,
    private toastr:ToastrService,  private fb: FormBuilder,  public bsModalRef: BsModalRef,
     public accountService:AccountService, public bookService:BooksService) {}
     authorForm: FormGroup;
     model: any = {};

  ngOnInit(): void {
    this.initializeForm(); 
    
  }

  initializeForm() {
    this.authorForm = new FormGroup({
      bookId: new FormControl(this.model.id),
      authorName: new FormControl(this.model.authorName, Validators.required),
      
    });
    this.authorForm.controls['bookId'].setValue(this.book.bookId);

  }
 

  postBookAuthor() {
    console.log(this.book.bookId);
    const user: User = JSON.parse(localStorage.getItem('user'));
    const id = this.accountService.getId(user);
    this.model = this.authorForm.value;
    this.model.ownerId = id;
    this.authorForm.controls['bookId'].setValue(this.book.bookId);
    console.log(this.book)
    this.bookService.postBookAuthor(this.model).subscribe(
      (res) => {
        console.log('Uspjeh');
        this.toastr.success('Uspješno ste dodali autora');
        this.bsModalRef.hide();
        window.location.reload();
      },
      (err) => {
        console.log(err);
     
      } 
      
    );
  }

 

  cancel(){
    this.bsModalRef.hide();
  }

  updateRecord(form:NgForm){
    this.service.putAuthor().subscribe(
      res =>{
        this.resetForm(form);
        this.service.refreshlist();
        this.toastr.info('Autor uspjesno izmjenjen')
      },
      err =>{console.log(err);}
    );
  }

  resetForm(form:NgForm){
    form.form.reset();
    this.service.formData = new Author();
  }
  

}
