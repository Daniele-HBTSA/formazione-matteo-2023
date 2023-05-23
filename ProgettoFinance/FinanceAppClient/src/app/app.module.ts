import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormDatiComponent } from './form-dati/form-dati.component';
import { BottoneloginComponent } from './bottonelogin/bottonelogin.component';
import { BottoneregistratiComponent } from './bottoneregistrati/bottoneregistrati.component';

@NgModule({
  declarations: [
    AppComponent,
    FormDatiComponent,
    BottoneloginComponent,
    BottoneregistratiComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
