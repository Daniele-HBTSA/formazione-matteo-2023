import { Injectable } from '@angular/core';
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

  tentaLogin(utente : User) :  Observable<User>{
    const url = environment.url + "login";

    return this.http.post<User>(url, utente).pipe(
      tap(element => {
        if(element != null) {
          this.utenteLoggato = element
        }
        else ("Utente non trovato")
      })
    );
  }

  tentaRegistraz(nuovoUtente : User) :  Observable<User> {
    const url = environment.url + "registrati";

    return this.http.post<User>(url, nuovoUtente);
  }

  

  
}
