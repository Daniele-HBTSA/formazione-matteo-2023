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
    if(localStorage.getItem("AccessToken") == null){
      alert("Effettuare un nuovo login")
      this.logout();
    }

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
        } 
      },
      error(err) {
        alert("Errore: " + err)
      }
    }))
  } 

  eliminaMovimento(index : number, idMovimento? : number, ) {
    if(localStorage.getItem("AccessToken") == null){
      alert("Effettuare un nuovo login")
      this.logout();
    }

    if(idMovimento != null) {
      this.subs.push(this.mov.tentaEliminaz(idMovimento).subscribe({
        next : (risposta : number) => {
          if(risposta) {
            this.getTabella(this.idUtenteCorrente)
            this.setSaldoUtente(risposta)

          }
        }
      }))
    } else {
      alert("Id vuoto")
    }
  } 

  logout(){
    this.auth.liberaStorage();
    this.visualizza.emit(false);
    this.router.navigateByUrl("financeapp")
  }

  ngOnDestroy(): void {
    this.UtenteCorrente = undefined;
    this.elencoMovimenti = [];
    this.subs.forEach(element => element.unsubscribe());
  }
}
