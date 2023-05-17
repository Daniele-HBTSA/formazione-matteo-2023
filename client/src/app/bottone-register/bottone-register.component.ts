import { Component, Input, OnInit } from '@angular/core';
import { RegistrationService } from '../service/registration.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-bottone-register',
  templateUrl: './bottone-register.component.html',
  styleUrls: ['./bottone-register.component.css']
})
export class BottoneRegisterComponent implements OnInit {
  @Input()
  UserName : string = ""
  @Input()
  UserPsw : string = ""


  constructor(private regi : RegistrationService, private router : Router) { }

  ngOnInit(): void {
  }

  clickRegistration() {
    this.regi.registration(this.UserName, this.UserPsw).subscribe({ 
      next : (response : boolean) => { //riceviamo la risposta da subscribe e la castiamo a boolean
        if(response) {
          //accede al sito
          alert("Utente registrato con successo.")
          this.router.navigateByUrl("");
  
        } else { 
          alert("Utente già presente, prova ad effettuare il LogIn")
          this.router.navigateByUrl("");
          
        }
      },
  
      error(err) { //in caso di errore, lo gestiamo
          alert("Errore");
      },
    })
  }

}
