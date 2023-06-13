import { Injectable, OnDestroy } from '@angular/core';
import { HttpClient }  from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { User } from '../models/User';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  utenteLoggato? : User = undefined

  constructor(private http : HttpClient) { }

  /**
   * Dopo una risposta positiva dal server, assegno l'utente loggato ad una variabile che potrò richiamare
   */
  tentaLogin(utente : User) :  Observable<User>{
    const url = environment.url + "login";

    return this.http.post<User>(url, utente).pipe(
      tap(element => {
        if(element.tokenPersonale == undefined) {
          throw new Error("Token non ricevuto");
          
        } else if (element != null) {
          this.utenteLoggato = element
          localStorage.setItem("Utente", element.AccountAzienda)
          localStorage.setItem("JWToken", "Bearer " + element.tokenPersonale)

        } else {
          throw new Error("Utente non trovato")

        } 
      })
    );
  }

  tentaRegistraz(nuovoUtente : User) :  Observable<User> {
    const url = environment.url + "registrati";

    return this.http.post<User>(url, nuovoUtente);
  } 
}
