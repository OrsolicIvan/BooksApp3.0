import { ContactFormComponent } from './contact-form/contact-form.component';
import { UnrentedBooksCardComponent } from './unrented-books/unrented-books-card/unrented-books-card.component';
import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorFormComponent } from './modals/add-author/author-form.component';
import { AuthorInfoComponent } from './show-author/author-info.component';
import { BooksComponent } from './show-boks/books.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { AdminGuard } from './_guards/admin.guard';
import { UploadBookComponent } from './upload-book/upload-book.component';
import { AddedBooksComponent } from './added-books/added-books.component';
import { RentedBooksComponent } from './rented-books/rented-books.component';
import { UnrentedBooksComponent } from './unrented-books/unrented-books.component';
import { UpdateBookComponent } from './modals/update-book/update-book.component';
import { MemberGuard } from './_guards/member.guard';
import { ModeratorGuard } from './_guards/moderator.guard';
import { LoginGuard } from './_guards/login.guard';


const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [

        {path: 'home', component: HomeComponent, canActivate: [LoginGuard]},
        {path: 'books', component: BooksComponent, canActivate: [AuthGuard]},
        {path: 'author-info',component: AuthorInfoComponent, canActivate: [AuthGuard]},
        {path: 'author-form',component: AuthorFormComponent, canActivate: [AuthGuard]},
        {path: 'admin',component: AdminPanelComponent, canActivate: [AuthGuard, AdminGuard]},
        {path: 'upload-book',component: UploadBookComponent, canActivate: [AuthGuard, ModeratorGuard]},
        {path: 'added-books', component: AddedBooksComponent, canActivate: [AuthGuard, MemberGuard]},
        {path: 'rented-books', component: RentedBooksComponent, canActivate: [AuthGuard, MemberGuard]},
        {path: 'unrented-books', component: UnrentedBooksComponent, canActivate: [AuthGuard, ModeratorGuard]},
        {path: 'contact-form', component: ContactFormComponent, canActivate: [AuthGuard]},
        {path: 'update-book', component: UpdateBookComponent, canActivate: [AuthGuard, ModeratorGuard]},
    ]
  },
        {path: '**', component: HomeComponent, pathMatch: 'full'},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  declarations: []
})
export class AppRoutingModule { }
