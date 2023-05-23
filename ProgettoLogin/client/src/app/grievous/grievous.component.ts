import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-grievous',
  templateUrl: './grievous.component.html',
  styleUrls: ['./grievous.component.css']
})
export class GrievousComponent implements OnInit, OnDestroy {

  @Output()
  messaggioEvent = new EventEmitter<string>();

  constructor() { }
  
  ngOnInit(): void {
    this.messaggioEvent.emit("Elemento creato");

  }

  ngOnDestroy(): void {
    this.messaggioEvent.emit("Elemento distrutto");

  }

}
