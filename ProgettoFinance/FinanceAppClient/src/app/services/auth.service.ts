import { Injectable } from '@angular/core';
import { HttpClient }  from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/User';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http : HttpClient) { }

  tentaLogin(utente : User) :  Observable<boolean>{
    const url = environment.url + "login";

    return this.http.post<boolean>(url, utente);

  }

  tentaRegistraz(nuovoUtente : User) :  Observable<boolean> {
    const url = environment.url + "registrati";

    return this.http.post<boolean>(url, nuovoUtente);
  }

  
}
