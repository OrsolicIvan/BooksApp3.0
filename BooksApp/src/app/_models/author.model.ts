export class Author {
    authorId:number=0;
    authorName: string='';
}
export class Book_Author{
    bookId:number=0;
    authorId:number=0;
    author:Author;
}