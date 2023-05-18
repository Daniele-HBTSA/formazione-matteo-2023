import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../model/User';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  constructor(private http: HttpClient) { }

  logIn(username : string, password : string) : Observable<boolean>{ //specifichiamo il tipo di ritorno come Observable di tipo booleano
    
    const user : User = {
      UserName : username,
      UserPsw : password
    }

    const url = "https://localhost:7189/login"

    return this.http.post<boolean>(url, user) //inviamo i dati al DB e ritorniamo un tipo boolean dopo la verifica

  }
}
