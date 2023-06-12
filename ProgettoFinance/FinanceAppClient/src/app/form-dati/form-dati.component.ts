import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { User } from '../models/User';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-form-dati',
  templateUrl: './form-dati.component.html',
  styleUrls: ['./form-dati.component.css']
})
export class FormDatiComponent implements OnInit, OnDestroy{

  accedi = true;
  registrati = false;
  @Output()
  rispostaServer = new EventEmitter<boolean>();

  IdAzienda? = 0;
  AccountAzienda = "";
  PswAzienda = "";
  NomeAzienda? = "";
  SaldoAzienda = 0;
    
  subs : Subscription[] = []; //Array di subscription a cui fare l'unsub

  constructor(private auth : AuthService) { }

  ngOnInit(): void {
    this.accedi = true;
    this.registrati = false;
  }

  switchFooter(){
    this.AccountAzienda = "";
    this.PswAzienda = "";
    this.NomeAzienda = "";
    this.SaldoAzienda = 0;
    this.accedi = !this.accedi;
    this.registrati = !this.registrati;
  }

  controlloDatiAccesso(){
    if(this.AccountAzienda == "" || this.PswAzienda == "") 
      return false
    else 
      return true
  }
  controlloDatiRegistr(){
    if(this.controlloDatiAccesso() || this.NomeAzienda == "")
      return false
    else 
      return true
  }

  richiediAccesso(){
    if(this.controlloDatiAccesso()) {
      const utente : User = {
        AccountAzienda : this.AccountAzienda,
        PswAzienda : this.PswAzienda
      }
  
      this.subs.push(this.auth.tentaLogin(utente).subscribe({
        next : (risposta : User) => {
          if(risposta != null){ 
            this.rispostaServer.emit(true);
          }
          else {
            alert("Il nome utente e/o la password sono errati!");
            this.rispostaServer.emit(false);
          }
        },
        error(err) {
          alert("Error: " + err);
  
        }
      }))
    } else {
      alert("Devi inserire i tuoi dati nei campi")
    }
  }

  richiediRegistraz() {
    if(this.controlloDatiRegistr()) {
      const nuovoUtente : User = {
        AccountAzienda : this.AccountAzienda,
        PswAzienda : this.PswAzienda,
        NomeAzienda : this.NomeAzienda,
        SaldoAzienda : this.SaldoAzienda
      }
  
      this.subs.push(this.auth.tentaRegistraz(nuovoUtente).subscribe({
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
      }))
    } else {
      alert("Devi inserire i tuoi dati nei campi")
    }
  }

  ngOnDestroy(): void {
    this.subs.forEach(element => element.unsubscribe());
  }
}
