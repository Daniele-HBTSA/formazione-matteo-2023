import { Injectable } from '@angular/core';
import { HttpClient }  from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http : HttpClient) { }

  tentaLogin(utente : User) :  Observable<boolean>{
    const url = "https://localhost:7189/login";

    return this.http.post<boolean>(url, utente);

  }

  tentaRegistraz(nuovoUtente : User) :  Observable<boolean> {
    const url = "https://localhost:7189/registrati";

    return this.http.post<boolean>(url, nuovoUtente);
  }

  
}
