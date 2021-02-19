import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { MsalService } from '@azure/msal-angular';
import { from, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';


@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
  constructor(private authService: MsalService, private _router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (!!this.authService.getAccount()){
     return from( this.authService.acquireTokenSilent({
      scopes: ['https://onuorahpascal.onmicrosoft.com/mywebapi/myscope']
    })
    .then((result: any) => {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${result.accessToken}`);
      const authReq = req.clone({ headers });
      return next.handle(authReq).pipe(tap(_ => { }, error => {
        const respError = error as HttpErrorResponse;
        if (respError && (respError.status === 401 || respError.status === 403)) {
          this._router.navigate(['/welcome']);
        }
      })).toPromise();
    }));
}
else{
  const headers = new HttpHeaders({});
  return next.handle(req.clone({ headers }));
}



  }


}
