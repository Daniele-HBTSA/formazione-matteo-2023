import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormDatiComponent } from './form-dati/form-dati.component';
import { BottoneloginComponent } from './form-dati/bottonelogin/bottonelogin.component';
import { BottoneregistratiComponent } from './form-dati/bottoneregistrati/bottoneregistrati.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TabellaMovimentiComponent } from './tabella-movimenti/tabella-movimenti.component';
import { AuthInterceptorsService } from './services/auth-interceptors.service';

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
  providers: [
    {provide : HTTP_INTERCEPTORS, useClass : AuthInterceptorsService, multi : true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
