import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-loggati',
  templateUrl: './loggati.component.html',
  styleUrls: ['./loggati.component.css']
})
export class LoggatiComponent {
  UserName : string = '';
  UserPsw : string = '';

  constructor(private auth : AuthService, private router : Router) {} //Dependency Injection

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
          this.router.navigateByUrl("home");
          
  
        } else { 
          alert("Utente non trovato.")
  
        }
      },
  
      error(err) { //in caso di errore, lo gestiamo
          console.log(err);
          alert("Errore: " + err);
      },
    })
    }

}
