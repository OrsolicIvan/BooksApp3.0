import { AccountService } from './_services/account.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'BooksApp';
  users: any;

constructor(private accountService: AccountService, private router: Router) {}

ngOnInit(){
  this.setCurrentUser();
  
}

setCurrentUser() {
  const user: User = JSON.parse(localStorage.getItem('user'))
  if(user) {
    this.accountService.setCurrentUser(user);
  }
}

}


