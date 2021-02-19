import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { IBook } from '../model/book';
import { ISubscribe } from '../model/subscribe';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private http: HttpClient) { }
  
  getBooks(): Observable<IBook[]> {
    return this.http.get<IBook[]>(environment.API_URL + 'api/v1/Books');
  }

  getBook(id: number): Observable<IBook> {
    return this.http.get<IBook>(environment.API_URL + 'api/v1/Books/byid/' + `${id}`);
  }
  subscribe(subscribe: ISubscribe): Observable<ISubscribe> {
    return this.http.post<ISubscribe>(environment.API_URL + 'api/v1/Subscription/subscribe', subscribe);
  }

  unSubscribe(bookid: number){
    return this.http.put(environment.API_URL + 'api/v1/Subscription/unsubscribe', bookid);
  }

}
