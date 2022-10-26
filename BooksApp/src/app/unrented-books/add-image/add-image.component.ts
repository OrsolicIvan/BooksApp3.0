import { HttpBackend, HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Book } from 'src/app/_models/book';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-add-image',
  templateUrl: './add-image.component.html',
  styleUrls: ['./add-image.component.css']
})
export class AddImageComponent implements OnInit {
  uploader: FileUploader;
  hasBaseDropzoneOver = false;
  baseUrl = environment.apiUrl;
  user: User;
  book: Book;
  

  constructor(private accountService: AccountService,private toastr: ToastrService, httpBackend: HttpBackend) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  ngOnInit(): void {
    this.initializeUploader();
  }

  fileOverBase(e: any){
    this.hasBaseDropzoneOver = e; 
  }

  initializeUploader(){
    var user: User = JSON.parse(localStorage.getItem('user'));
    this.user = this.accountService.getId(user);
    this.uploader = new FileUploader({
      url: this.baseUrl + 'AppUser/add-image?id=' +  this.book.bookId,
      authToken: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10*1024*1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response){
        console
        if(response=="Ok"){
        this.toastr.warning("Greska");}
        else {this.toastr.success("Fotografija uspješno uploadana");
        window.setTimeout(function(){location.reload()},2000)}
      }
    }
   }

   loadLoggedInUser() {
    var user: User = JSON.parse(localStorage.getItem('user'));
    this.user = this.accountService.getId(user);
    console.log(this.user)

  }

}
