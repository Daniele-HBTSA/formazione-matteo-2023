import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-form-dati',
  templateUrl: './form-dati.component.html',
  styleUrls: ['./form-dati.component.css']
})
export class FormDatiComponent implements OnInit {

  accedi = true;
  registrati = false;

  constructor() { }

  ngOnInit(): void {
    this.accedi = true;
    this.registrati = false;
  }

  switchFooter(event : void){
    console.log("hello there")
    if(this.accedi == true && this.registrati == false) {
      this.accedi = false;
      this.registrati = true;
    } else {
      this.accedi = true;
      this.registrati = false;
    }
  }



}
