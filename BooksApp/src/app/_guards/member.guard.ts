import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MemberGuard implements CanActivate {
  
  constructor(private accountService: AccountService, private router: Router, public toastr: ToastrService) {}

  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map(user => {
        if (user.roles.includes('Member')) {
          return true;
        }
        this.toastr.error('Nemate ulogu membera')
        this.router.navigateByUrl('/books');
      })
    )
  }
    
}