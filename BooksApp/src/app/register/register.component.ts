import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { map, ReplaySubject } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  registerForm: FormGroup;

  constructor(private accountService: AccountService, private fb: FormBuilder,
     private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();  
  }

  initializeForm(){
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', [Validators.required,
        Validators.minLength(4), Validators.maxLength(10)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    })

    this.registerForm.controls['password'].valueChanges.subscribe(() => {
      this.registerForm.controls['confirmPassword'].updateValueAndValidity();
    })
    
  }

  onChange(event: any){
    console.log(event.value);
  }

  matchValues(matchTo: string): ValidatorFn {
    return(control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value
      ? null : {isMatching: true}
    }
  }

  register(){ 
    this.accountService.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('/books')
      this.cancel();
      }, error => {
      console.log(error);
      })
      console.log(this.registerForm.value);
      alert('Ukoliko želite objavljivati knjige kontaktirajte admina. Idite na kontakt');
     
  }
  
  
  cancel() {
    this.cancelRegister.emit(false);
  }
}


