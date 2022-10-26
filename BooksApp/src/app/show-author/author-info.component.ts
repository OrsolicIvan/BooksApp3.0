import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Author } from '../_models/author.model';
import { AuthorService } from '../_services/author.service';

@Component({
  selector: 'app-author-info',
  templateUrl: './author-info.component.html',
  styles: [
  ]
})
export class AuthorInfoComponent implements OnInit {

  constructor(public service: AuthorService, 
    private toastr: ToastrService) {}

  ngOnInit(): void {
    this.service.refreshlist();
  }

  populateForm(selectedRecord:Author){
    this.service.formData = Object.assign({},selectedRecord);
  }

  onDelete(id:number){
    if(confirm('Jeste li sigurni da zelite obrisati ovog autora?'))
  {
    this.service.deleteAuthor(id)
    .subscribe(
      res => {
        this.service.refreshlist();
        this.toastr.error("Autor uspjesno obrisan", 'Author register')
      },
      err => {console.log(err)},
    )
  }
}

}
