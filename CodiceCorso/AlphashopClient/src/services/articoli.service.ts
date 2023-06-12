import { IArticoli } from 'src/app/models/Articoli';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ArticoliService {

  articoli: IArticoli[]  = []

  constructor() { }

  getArticoli = () : IArticoli[] => this.articoli;

  getArticoliByCode = (codart: string) : IArticoli => {

    const index = this.articoli.findIndex(articoli => articoli.codArt === codart);
    return this.articoli[index];

  }
}
