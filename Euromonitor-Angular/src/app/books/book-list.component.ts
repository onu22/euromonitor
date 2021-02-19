import { Component, OnInit } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { BookService } from '../core/book.service';
import { IBook } from '../model/book';
import { ISubscribe } from '../model/subscribe';


@Component({
  selector: 'pm-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {
  books: IBook[] = [];
  subscribe: ISubscribe;
  loggedIn = false;
  constructor(private bookService: BookService, private authService: MsalService) {
   }

  onSubUnSub(book: IBook): void {

    if (book.userIsSubscribed){
      this.bookService.unSubscribe(book.id).subscribe(() => {
        book.userIsSubscribed = false;
      });
    }

    if (!book.userIsSubscribed){
      let subscribe: ISubscribe;
      subscribe = { bookid: book.id, purchaseprice: book.price};
      this.bookService.subscribe(subscribe).subscribe(() => {
        book.userIsSubscribed = true;
      });
    }

  }

  ngOnInit(): void {

    this.loggedIn = !!this.authService.getAccount();
    this.bookService.getBooks().subscribe({
      next: books => {
        this.books = books;
      },
      error: err => console.log(err)
    });

  }

}

