import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { User } from '../models/User';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

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

  IdAzienda? = 0;
  AccountAzienda = "";
  PswAzienda = "";
  NomeAzienda? = "";
  SaldoAzienda = 0;

  constructor(private auth : AuthService, private router : Router) { }

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
      next : (risposta : User) => {
        if(risposta != null){ 
          this.rispostaServer.emit(true);
          this.router.navigateByUrl("tabella/")
        }
        else {
          alert("Il nome utente e/o la password sono errati!");
          this.rispostaServer.emit(false);

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
      next : (risposta : User) => {
        if(risposta){ 
          alert("Registrazione riuscita, autenticati");

        }
        else {
          alert("Utente già esistente");

        }
      },
      error(err) {
        alert("Error: " + err);

      }
    })
  }
}
