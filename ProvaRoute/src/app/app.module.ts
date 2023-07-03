import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { Contenuto1Component } from './contenuto1/contenuto1.component';
import { Contenuto2Component } from './contenuto2/contenuto2.component';
import { NothumanComponent } from './nothuman/nothuman.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    Contenuto1Component,
    Contenuto2Component,
    NothumanComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
