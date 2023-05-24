import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-form-dati',
  templateUrl: './form-dati.component.html',
  styleUrls: ['./form-dati.component.css']
})
export class FormDatiComponent implements OnInit {

  accedi = true;
  registrati = false;
  accountAzienda = "";
  password = "";

  constructor() { }

  ngOnInit(): void {
    this.accedi = true;
    this.registrati = false;
  }

  switchFooter(event : void){
    console.log("general kenobi")
      this.accedi = !this.accedi;
      this.registrati = !this.registrati;
  }

  tentaAccesso(){
    console.log(this.accountAzienda + " " + this.password)
  }

  tentaRegistraz() {
    console.log(this.accountAzienda + " " + this.password)
  }


}
