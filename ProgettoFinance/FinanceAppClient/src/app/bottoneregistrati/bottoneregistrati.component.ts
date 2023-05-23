import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-bottoneregistrati',
  templateUrl: './bottoneregistrati.component.html',
  styleUrls: ['./bottoneregistrati.component.css']
})
export class BottoneregistratiComponent implements OnInit {

  @Output()
  onFooterClick = new EventEmitter();

  constructor() { }

  ngOnInit(): void { }

  click() {
    this.onFooterClick.emit();
  }

}
