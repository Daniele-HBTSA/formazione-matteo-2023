import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-prova-on-changes',
  templateUrl: './prova-on-changes.component.html',
  styleUrls: ['./prova-on-changes.component.css']
})
export class ProvaOnChangesComponent implements OnInit, OnChanges {

  visualizza = false;

  @Input()
  inputFiglio : string = ""
  messaggio = ""

  constructor() { }
  
  ngOnInit(): void {

  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log("Sto ricevendo i dati dal padre:")
    console.log(changes["inputFiglio"].currentValue)

  }

  switchVisualizza() {
    if(this.visualizza) 
      this.visualizza = false;

    else 
      this.visualizza = true;
    
  }




}
