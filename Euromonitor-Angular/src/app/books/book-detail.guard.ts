import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { MsalService } from '@azure/msal-angular';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookDetailGuard implements CanActivate {

  loggedIn = false;
  constructor(private router: Router, private authService: MsalService) { }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      this.loggedIn = !!this.authService.getAccount();
      console.log(this.loggedIn + 'from guard');
      if (!this.loggedIn){
        this.router.navigate(['/books']);
        return false;
      }
      return true;
  }
}
