import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { TabellaMovimentiService } from '../services/tabella-movimenti.service';
import { Movimento } from '../models/Movimento';
import { AuthService } from '../services/auth.service';
import { User } from '../models/User';
import { MovimentiService } from '../services/movimenti.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-tabella-movimenti',
  templateUrl: './tabella-movimenti.component.html',
  styleUrls: ['./tabella-movimenti.component.css']
})
export class TabellaMovimentiComponent implements OnInit, OnDestroy {
  @Output()
  visualizza = new EventEmitter<boolean>();
  UtenteCorrente? : User = undefined
  idUtenteCorrente = 0;

  elencoMovimenti : Movimento[] = [];
  nuovoMovimento = 0;  
  idcliccato = 0;

  subs : Subscription[] = []

  constructor(private tabellaDB : TabellaMovimentiService, 
              private mov : MovimentiService, 
              private auth : AuthService,
              private router : Router) { }


  ngOnInit(): void {
    this.UtenteCorrente = this.auth.utenteLoggato;
    this.idUtenteCorrente = this.getIdUtente();
    this.getTabella(this.idUtenteCorrente)
    
  }

  getTabella(idUtenteCorrente : number){
    this.subs.push(this.tabellaDB.getTabella(idUtenteCorrente).subscribe({
      next : (tabellaDB : Movimento[]) => {
        this.elencoMovimenti = tabellaDB
      },
      error(err) {
        alert("Id errato");

      }
    }))
  }

  getIdUtente() {
    const idUtente = this.UtenteCorrente?.IdAzienda
    if(idUtente != null) 
      return idUtente
    else 
      return 0
  }

  setSaldoUtente(nuovoSaldo : number) {
    if(nuovoSaldo != null && this.UtenteCorrente != null) 
      this.UtenteCorrente.SaldoAzienda = nuovoSaldo;
  }

  aggiungiMovimento(valMovimento : number) {
    const nuovoMovimento : Movimento = {
      IdAzienda : this.idUtenteCorrente,
      ValoreMovimento : valMovimento
    }
    this.subs.push(this.mov.tentaAggiunta(nuovoMovimento).subscribe({
      next : (risposta : number) => {
        if(risposta) {
          this.getTabella(this.idUtenteCorrente)
          this.setSaldoUtente(risposta)
          this.nuovoMovimento = 0;

        } else 
          alert("Errore")
      },
      error(err) {

      }
    }))
  }

  eliminaMovimento(index : number, idMovimento? : number, ) {
    if(idMovimento != null) {
      this.subs.push(this.mov.tentaEliminaz(idMovimento).subscribe({
        next : (risposta : number) => {
          if(risposta) {
            this.getTabella(this.idUtenteCorrente)
            this.setSaldoUtente(risposta)

          } else 
            alert("Errore")
        }
      }))
    } else {
      alert("Id vuoto")
    }
  }

  logout(){
    this.visualizza.emit(false);
    this.router.navigateByUrl("financeapp")
    localStorage.removeItem("Utente")
    localStorage.removeItem("AccessToken")
    localStorage.removeItem("RefreshToken")
  }

  ngOnDestroy(): void {
    this.UtenteCorrente = undefined;
    this.elencoMovimenti = [];
    this.subs.forEach(element => element.unsubscribe());
  }

}
