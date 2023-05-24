import { Component, EventEmitter, OnInit, Output } from '@angular/core';
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
  rispostaServer = new EventEmitter<boolean>();

  AccountAzienda = "";
  PswAzienda = "";
  NomeAzienda? = "";
  SaldoAzienda = 0;

  constructor(private auth : AuthService) { }

  ngOnInit(): void {
    this.accedi = true;
    this.registrati = false;
  }

  switchFooter(){
    console.log("general kenobi")
    this.accedi = !this.accedi;
    this.registrati = !this.registrati;
  }

  richiediAccesso(){
    const utente : User = {
      AccountAzienda : this.AccountAzienda,
      PswAzienda : this.PswAzienda
    }

    this.auth.tentaLogin(utente).subscribe({
      next : (risposta : boolean) => {
        if(risposta){ 
          alert("Login riuscito");
          this.rispostaServer.emit(risposta);

        }
        else {
          alert("Il nome utente e/o la password sono errati!");
          this.rispostaServer.emit(risposta);

        }
      },
      error(err) {
        alert("Error: " + err);

      }
    })
  }

  richiediRegistraz() {
    const nuovoUtente : User = {
      AccountAzienda : this.AccountAzienda,
      PswAzienda : this.PswAzienda,
      NomeAzienda : this.NomeAzienda,
      SaldoAzienda : this.SaldoAzienda
    }

    this.auth.tentaRegistraz(nuovoUtente).subscribe({
      next : (risposta : boolean) => {
        if(risposta){ 
          alert("Registrazione riuscita, autenticati");
          this.rispostaServer.emit(risposta);

        }
        else {
          alert("Utente già esistente");
          this.rispostaServer.emit(risposta);

        }
      },
      error(err) {
        alert("Error: " + err);

      }
    })
  }



}
