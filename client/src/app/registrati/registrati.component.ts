import { Component, OnInit } from '@angular/core';
import { RegistrationService } from '../service/registration.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registrati',
  templateUrl: './registrati.component.html',
  styleUrls: ['./registrati.component.css']
})
export class RegistratiComponent {
  UserName : string = '';
  UserPsw : string = '';

  constructor(private regi : RegistrationService, private router : Router) { }

  clickRegistration() {
    this.regi.registration(this.UserName, this.UserPsw).subscribe({ 
      next : (response : boolean) => { //riceviamo la risposta da subscribe e la castiamo a boolean
        if(response) {
          //accede al sito
          alert("Utente registrato con successo.")
          this.router.navigateByUrl("");
  
        } else { 
          alert("Utente già esistente, effettua il login.")
          this.router.navigateByUrl("");
          
        }
      },
  
      error(err) { //in caso di errore, lo gestiamo
          alert("Errore");
      },
    })
  }

}
