import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-bottonelogin',
  templateUrl: '../bottonelogin/bottonelogin.component.html',
  styleUrls: ['./bottonelogin.component.css']
})
export class BottoneloginComponent implements OnInit {

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
    console.log("hello there")
    this.onFooterClick.emit();
  }
   
}
