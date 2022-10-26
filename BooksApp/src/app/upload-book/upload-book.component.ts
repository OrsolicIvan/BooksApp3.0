import { HttpClient, HttpParams, HttpBackend } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { BookClass } from '../_models/book';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';
import { BooksService } from '../_services/books.service';

@Component({
  selector: 'app-upload-book',
  templateUrl: './upload-book.component.html',
  styleUrls: ['./upload-book.component.css'],
})
export class UploadBookComponent implements OnInit {
  uploadForm: FormGroup;
  user: User;
  model: any = {};
  book: any;
  isShowDiv = false;
  private httpClient: HttpClient;

  constructor(
    private http: HttpClient,
    private toastr: ToastrService,
    private accountService: AccountService,
    private bookService: BooksService,
    httpBackend: HttpBackend
  ) {
    this.httpClient = new HttpClient(httpBackend);
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.uploadForm = new FormGroup({
      bookTitle: new FormControl(this.model.bookTitle, Validators.required),
      bookYear: new FormControl(this.model.bookYear,[ Validators.required, Validators.minLength(4), Validators.maxLength(4)]),
      ownerId: new FormControl(this.model.ownerId),
      url: new FormControl(this.model.url),
    });
  }

  postBook() {
    var name = this.uploadForm.get('bookTitle').value;
    console.log(name);
    
    this.httpClient
      .get('https://api.itbook.store/1.0/search/' + name)
      .subscribe((data: any) => {
       
        this.book = data.books[0];

        {
  
          this.isShowDiv = true;
          const user: User = JSON.parse(localStorage.getItem('user'));
          const id = this.accountService.getId(user);
          this.model = this.uploadForm.value;
          this.model.ownerId = id;
          console.log(this.book);
       
        
          this.bookService.postBook(this.model).subscribe(
            
            (res) => {
              
              this.bookService.bookformData = new BookClass();
              this.toastr.success('Knjiga je uspješno objavljena');
              
            },
            (err) => {
              console.log(err);
              console.log(this.model);
            }
          );
    
        }
        
      });
      
  }

  getDetails() {
    var name = this.uploadForm.get('bookTitle').value;
    console.log(name);
    this.httpClient
      .get('https://api.itbook.store/1.0/search/' + name)
      .subscribe((data: any) => {
        console.log('res', data);
        this.book = data.books[0];
        console.log(data.books[0]);
        console.log(this.book);
        if (this.book.Response == 'False') {
          this.toastr.error('Knjiga nije pronađena');
        } else {
          this.toastr.info('Knjiga je pronađena');
          // console.log(this.book)
          this.isShowDiv = true;
        }
      });
  }

  loadLoggedInUser() {
    var user: User = JSON.parse(localStorage.getItem('user'));
    const id = this.accountService.getId(user);
  }

  numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;

  }
}
