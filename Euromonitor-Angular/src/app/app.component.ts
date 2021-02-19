import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BroadcastService, MsalService } from '@azure/msal-angular';
import { Logger, CryptoUtils } from 'msal';
import { Subscription } from 'rxjs';

@Component({
  selector: 'pm-root',
  templateUrl: './app.component.html',
   styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  isIframe = false;
  loggedIn = false;
  private subscription: Subscription;

  constructor( private broadcastService: BroadcastService,
               private authService: MsalService, private router: Router) { }
  
  ngOnInit() {
    this.initializeAuth();
  }

  initializeAuth() {

    let loginSuccessSubscription: Subscription;
    this.isIframe = window !== window.parent && !window.opener;
    this.checkoutAccount();

    loginSuccessSubscription = this.broadcastService.subscribe('msal:loginSuccess', () => {
      this.checkoutAccount();
      this.router.navigate(['books-r']);
    });

    this.authService.setLogger(new Logger((logLevel, message, piiEnabled) => {
      console.log('MSAL Logging: ', message);
    }, {
      correlationId: CryptoUtils.createNewGuid(),
      piiLoggingEnabled: false
    }));
  }


  logIn() {
    const isIE0 = window.navigator.userAgent.indexOf('MSIE ') > -1;
    const isIE1 = window.navigator.userAgent.indexOf('Trident/') > -1;
    if (isIE0 || isIE1) {
      this.authService.loginRedirect();
    }
    else {
      this.authService.loginPopup();
    }
  }

  logOut() {
    this.authService.logout();
  }

checkoutAccount() {
  this.loggedIn = !!this.authService.getAccount();
}


ngOnDestroy() {
  this.broadcastService.getMSALSubject().next(1);
  if (this.subscription) {
    this.subscription.unsubscribe();
  }
}

}
