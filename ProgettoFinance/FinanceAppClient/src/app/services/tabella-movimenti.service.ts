import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Movimento } from '../models/Movimento';

@Injectable({
  providedIn: 'root'
})
export class TabellaMovimentiService {

  constructor(private http : HttpClient) { }

  getTabella(idAzienda : number) : Observable<Movimento[]>{
    const url = environment.url + "mostramovimenti/" + idAzienda;

    return this.http.get<Movimento[]>(url);
   
  }
  

}
