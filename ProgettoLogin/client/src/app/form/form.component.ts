import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

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

  onAuthenticate(rispostaBottone : boolean){
    this.autenticato = rispostaBottone;
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    if (this.autenticato) {
      alert("Login riuscito.")
      // this.router.navigateByUrl("home");
      return true;

      } else {
      alert("Inserisci i tuoi dati")
      return false;

      }
  }
}

