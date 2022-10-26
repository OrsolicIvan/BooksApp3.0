import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  loginMode=false; 
  users: any;
 
  constructor(private http: HttpClient,  private router: Router) { }

  ngOnInit(): void {  
  }  

  registerToggle(){  
    this.registerMode = !this.registerMode;   
  }
  loginToggle(){
    this.loginMode = !this.loginMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
  cancelLoginMode(event: boolean) {
    this.loginMode = event;
  }
}


