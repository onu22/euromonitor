import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MsalModule } from '@azure/msal-angular';

import { AppComponent } from './app.component';
import { WelcomeComponent } from './home/welcome.component';
import { BookListComponent } from './books/book-list.component';
import { BookDetailComponent } from './books/book-detail.component';
import { BookDetailGuard } from './books/book-detail.guard';
import { AuthInterceptorService } from './core/auth-interceptor.service';

export const protectedResourceMap: [string, string[]][] = [
  ['https://graph.microsoft.com/v1.0/me', ['user.read']],
  ['https://onuorahpascal.onmicrosoft.com/mywebapi', ['myscope']]
];

@NgModule({
  declarations: [
    AppComponent,
    WelcomeComponent,
    BookListComponent,
    BookDetailComponent
  ],
  imports: [
    MsalModule.forRoot(
      {
        auth: {
          clientId: '27c4b83f-4d51-466a-92e8-cc26e1e438d5',
          authority: 'https://login.microsoftonline.com/cc7ab56c-798c-4a97-bc03-049c910f3bd0',
          // This is your redirect URI
          redirectUri: 'http://localhost:4220/welcome',
          postLogoutRedirectUri: 'http://localhost:4220/welcome',
          navigateToLoginRequestUrl: true,
        },
        cache: {
          cacheLocation: 'localStorage',
          storeAuthStateInCookie: false,
        },
      },
      {
        popUp: false,
        protectedResourceMap,
        extraQueryParameters: {}
      }
    ),
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: 'books', component: BookListComponent },
      { path: 'books-r', component: BookListComponent },
      { path: 'books/:id', canActivate: [BookDetailGuard], component: BookDetailComponent },
      { path: 'welcome', component: WelcomeComponent },
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true }
],
  bootstrap: [AppComponent]
})
export class AppModule { }
