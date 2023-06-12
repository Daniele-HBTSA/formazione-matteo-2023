import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Movimento } from '../models/Movimento';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MovimentiService {

  constructor(private http : HttpClient) { }

  tentaAggiunta(nuovoMovimento : Movimento) : Observable<number>{
    const url = environment.url + "aggiungi"

    return this.http.post<number>(url, nuovoMovimento)
  }

  tentaEliminaz(idMovimento : number) : Observable<number>{
    const url = environment.url + "rimuovi/" + idMovimento

    return this.http.delete<number>(url)
  }

}
