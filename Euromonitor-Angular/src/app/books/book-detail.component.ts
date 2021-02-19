import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MsalService } from '@azure/msal-angular';
import { BookService } from '../core/book.service';
import { IBook } from '../model/book';

@Component({
  selector: 'pm-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.css']
})
export class BookDetailComponent implements OnInit {
  pageTitle = 'Book Detail';
  selectedBook: IBook;
  loggedIn = false;
  constructor(private route: ActivatedRoute,
              private router: Router,
              private bookService: BookService,
              private authService: MsalService)  { }

  ngOnInit(): void {

    this.loggedIn = !!this.authService.getAccount();
    this.getBook(+this.route.snapshot.paramMap.get('id'));
  }

  getBook(id): void {
    this.bookService.getBook(id)
      .subscribe(
        book => {
          this.selectedBook = book;
        });
  }
  onBackButton(): void {
    this.router.navigate(['/books']);
  }

}
