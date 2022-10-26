import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { BooksService } from '../_services/books.service';
import { BookDetailsComponent } from '../modals/book-details/book-details.component';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { HttpBackend, HttpParams, HttpClient } from '@angular/common/http';
const PARAMS = new HttpParams({
  fromObject: { action: 'opensearch', format: 'json', origin: '*' },
});
@Component({
  selector: 'app-show-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css'],
})
export class BooksComponent implements OnInit {
  private httpClient: HttpClient;
  public totalBooks: number;
  public books: Subject<any> = new Subject();
  public currentPage: number = 1;
  public itemsperPage: number = 10;
  public totalItems: any;
  bsModalRef: BsModalRef;
  uploadForm: FormGroup;
  model: any = {};
  book: any;
  isShowDiv = false;
  title: any;

  constructor(
    private booksService: BooksService,
    private modalService: BsModalService,
    private http: HttpClient,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    httpBackend: HttpBackend
  ) {
    
    this.initializeForm();
    this.httpClient = new HttpClient(httpBackend);
  }

  ngOnInit() {this.getPage(1);}

  // public paginate(page: number): void {

  //   this.currentPage = page;

  //   this.getPage(this.currentPage).subscribe(result => {

  //     this.books.next(result.books);

  //     this.totalItems = result.total;

  //   });

  // }

  initializeForm() {
    this.uploadForm = new FormGroup({
      book: new FormControl(this.model.book, Validators.required),
    });
    this.uploadForm.controls['book'].valueChanges.subscribe(() => {});
  }

  public getPage(page: number) {
    //this.httpClient.get('https://api.itbook.store/1.0/search/new?page=${page}`);
    this.currentPage = page;
    this.httpClient
      .get('https://api.itbook.store/1.0/search/new?page=' + page)
      .subscribe((result: any) => {
        console.log(result);
        this.books.next(result.books);

        return (this.totalItems = result.total);
      });
  }

  openDetailsModal(book: any) {
    const initialState = {
      class: 'modal-dialog-centered',
      initialState: {
        book,
      },
    };
    this.bsModalRef = this.modalService.show(
      BookDetailsComponent,
      initialState
    );
    this.bsModalRef.content.closeBtnName = 'Close';
  }
}

@Pipe({
  name: 'customslice'
})
export class CustomSlice implements PipeTransform {
  transform(val:string , length:number):string {
    return val.length > length ? `${val.substring(0, length)} ...` : val
  }
}