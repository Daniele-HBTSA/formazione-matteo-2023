import { Component, OnInit, Output } from '@angular/core';
import { User } from '../models/User';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-form-dati',
  templateUrl: './form-dati.component.html',
  styleUrls: ['./form-dati.component.css']
})
export class FormDatiComponent implements OnInit {

  accedi = true;
  registrati = false;
  @Output()
  rispostaServer = false;

  accountAzienda = "";
  password = "";
  nomeAzienda = "";
  saldoAzienda = 0;

  constructor(private auth : AuthService) { }

  ngOnInit(): void {
    this.accedi = true;
    this.registrati = false;
    this.rispostaServer = false;
  }

  switchFooter(){
    console.log("general kenobi")
      this.accedi = !this.accedi;
      this.registrati = !this.registrati;
  }

  richiediAccesso(){
    const utente : User = {
      accountAzienda : this.accountAzienda,
      password : this.password
    }

    if(this.auth.tentaLogin(utente)) 
      this.rispostaServer = true;
  }

  richiediRegistraz() {
    const nuovoUtente : User = {
      accountAzienda : this.accountAzienda,
      password : this.password,
      nomeAzienda : this.nomeAzienda,
      saldoAzienda : this.saldoAzienda
    }

    if(this.auth.tentaRegistraz(nuovoUtente))
      this.rispostaServer = true;
  }
}
