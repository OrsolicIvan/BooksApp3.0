import { Book_Author } from "./author.model";
import { Image } from "./image";

export class BookClass {
  bookId: number = 0;
  bookTitle: string = '';
  bookYear: number = 0;
  authorName: string = '';
  ownerId: number = 0;
   book_Authors: Book_Author[] = [];
  images: Image;
}

export class RentBookClass {
  bookId: number = 0;
  renterId: number = 0;
  ownerId: number = 0;
}
export class UnRentBookClass {
  bookId: number = 0;
  renterId: number = 0;
  ownerId: number = 0;
}

export interface Book {
  bookId: number;
  bookTitle: string;
  bookYear: number;
  authorName: string;
  ownerId: number;
  images: Image;
  book_Authors: Book_Author[];

}
export class GetOwnedBooks {
    bookId: number = 0;
    ownerId: number = 0;
    renterId: number = 0;
    bookTitle: string ="";
    bookYear: number = 0;
    book_Authors: Book_Author[] = [];
    authorName: string = "";
    images: Image;

}
export class BookUpdateClass{
  bookId: number = 0;
  bookTitle: string = "";
  bookYear: number = 0;

}
