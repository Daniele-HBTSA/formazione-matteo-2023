import { Component, OnInit } from '@angular/core';
import { GetTableService } from '../service/get-table.service';
import { User } from '../model/User';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  //currentUser = 
  users: User[] = []

  constructor(private dbTable : GetTableService) { }

  ngOnInit(): void {

    this.dbTable.getTable().subscribe({
      next : (dbTable : User[]) => {
        this.users = dbTable;
  
        dbTable.forEach(element => {
          console.log(element)
        });
        
        return dbTable
      },
      error(err) {
      }
    })
  }
}
