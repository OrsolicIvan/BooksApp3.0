import { HttpClient } from '@angular/common/http';
import { Injectable, Output } from "@angular/core";
import { User } from '../_models/user'
import { map, ReplaySubject } from "rxjs";
import { Router } from '@angular/router';



@Injectable({
    providedIn: 'root'
})

export class AccountService {
  baseUrl = 'http://localhost:32679/api/';
  private currentUserSource = new ReplaySubject<User>(1)
  currentUser$ = this.currentUserSource.asObservable();
  user: User[];
  
  
  constructor(private httpClient: HttpClient, private router: Router){}

  login(model: any){
    return this.httpClient.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response
        if (user) {
          this.currentUserSource.next(user);
          localStorage.setItem('user', JSON.stringify(user));
          this.setCurrentUser(user);
        }
      })
    )
  }

  setCurrentUser(user: User){
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
    
  }

  getId(user:User){
    if(user) {
      const id = this.getDecodedToken(user.token).nameid; // get the id from the token  and return it
      return id;
    }
  }

  getUsername(user: User){
    if (user){
    const id = this.getDecodedToken(user.token).uniquename;  
    return id;
    }
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  register(model: any){
    return this.httpClient.post<User>(this.baseUrl + 'Account/registerMember' , model).pipe(
      map((user: User) => {
        if (user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
          this.setCurrentUser(user);
        }
      })
    )
  }

  getDecodedToken(token) {
    return JSON.parse(atob(token.split('.')[1]));
  }

  isLoggedIn() {
    if (this.user) {
      return true;
    }
  }

}