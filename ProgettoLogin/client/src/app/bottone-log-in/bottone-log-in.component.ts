import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-bottone-log-in',
  templateUrl: './bottone-log-in.component.html',
  styleUrls: ['./bottone-log-in.component.css']
})
export class BottoneLogInComponent implements OnInit {
  @Input()
  UserName : string = "";
  @Input()
  UserPsw : string = "";

  @Output() 
  autenticato = new EventEmitter<boolean>();

  constructor(private auth : AuthService) { }

  ngOnInit(): void {
    
  }

  clickLogIn(){ // effettuare la chiamata 
     this.auth.logIn(this.UserName, this.UserPsw).subscribe({ //questo metodo riceve gli aggiornamenti dall'Observable
       /*
       All'interno di subscribe ci sono 2 handlers:
       next: obbligatorio, gestisce il tipo di risposta ottenuta da subscribe.
       error: opzionale, si può aggiungere e gestisce gli eventuali errori.
       */
       next : (response : boolean) => { //riceviamo la risposta da subscribe e la castiamo a boolean
         if(response) {
           //accede al sito
           this.autenticato.emit(true);
  
         } else { 
            this.autenticato.emit(false);
           alert("Il nome utente e/o la password sono errati!");
  
         }
       },
  
       error(err) { //in caso di errore, lo gestiamo
           alert("Errore: " + err);
       },
     })

     
    }
}
