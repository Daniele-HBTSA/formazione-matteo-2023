import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-bottoneregistrati',
  templateUrl: './bottoneregistrati.component.html',
  styleUrls: ['./bottoneregistrati.component.css']
})
export class BottoneregistratiComponent implements OnInit {

  @Output()
  onButtonClick = new EventEmitter();
  @Output()
  onFooterClick = new EventEmitter();

  constructor() { }

  ngOnInit(): void { }

  buttonClick() {
    this.onButtonClick.emit();
  }

  footerClick() {
    console.log("Hello there")
    this.onFooterClick.emit();
  }

}
