import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-bottonelogin',
  templateUrl: './bottonelogin.component.html',
  styleUrls: ['./bottonelogin.component.css']
})
export class BottoneloginComponent implements OnInit {

  @Output() 
  onFooterClick = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  click() {
      this.onFooterClick.emit();
  }
   
}
