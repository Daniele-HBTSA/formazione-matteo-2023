import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../model/User';

@Injectable({
  providedIn: 'root'
})
export class GetTableService {

  constructor(private http : HttpClient) { }

  //metodo che richiede l'elenco degli utenti
  getTable() : Observable<User[]> {
    const url = "https://localhost:7189/homepage"
    
    return this.http.get<User[]>(url);
  }
}
