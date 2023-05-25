import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormDatiComponent } from './form-dati/form-dati.component';
import { BottoneloginComponent } from './form-dati/bottonelogin/bottonelogin.component';
import { BottoneregistratiComponent } from './form-dati/bottoneregistrati/bottoneregistrati.component';
import { HttpClientModule } from '@angular/common/http';
import { TabellaMovimentiComponent } from './tabella-movimenti/tabella-movimenti.component';

@NgModule({
  declarations: [
    AppComponent,
    FormDatiComponent,
    BottoneloginComponent,
    BottoneregistratiComponent,
    TabellaMovimentiComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
