import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  UserName : string = '';
  UserPsw : string = '';

  autenticato = false; 

  constructor(private router : Router) { }

  ngOnInit(): void {
  }
  
  onAuthenticate(isAuthenticated : boolean){
    this.autenticato = isAuthenticated;

    if (this.autenticato) {
      this.router.navigateByUrl("home");

      } else {
      alert("Inserisci i tuoi dati")

      }
  }
}

