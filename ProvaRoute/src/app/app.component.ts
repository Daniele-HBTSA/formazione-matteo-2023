import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NavigateService } from './service/navigate.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  showContent : boolean = false;
  human : boolean = false;

  constructor(private router : Router, private humanCheck : NavigateService){}

  switch() {
    this.showContent = !this.showContent;
    this.humanCheck.human = this.human;
    this.navigate();
  }

  navigate() {
    if(this.human) {
      this.router.navigateByUrl("contenuto1")
      this.human = false;
    }
    else {
      this.router.navigateByUrl("contenuto2")
    }
  }

  backHome(){
    this.showContent = false;
    this.human = false;
    this.router.navigateByUrl("benvenuto");
  }


}
