import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';


import { AppComponent } from './app.component';
import { FormDatiComponent } from './form-dati/form-dati.component';
import { BottoneloginComponent } from './form-dati/bottonelogin/bottonelogin.component';
import { BottoneregistratiComponent } from './form-dati/bottoneregistrati/bottoneregistrati.component';
import { TabellaMovimentiComponent } from './tabella-movimenti/tabella-movimenti.component';
import { AuthInterceptorsService } from './services/auth-interceptors.service';
import { ErrorInterceptor } from './services/error-interceptor.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@NgModule({
  declarations: [
    AppComponent,
    FormDatiComponent,
    BottoneloginComponent,
    BottoneregistratiComponent,
    TabellaMovimentiComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatProgressSpinnerModule
  ],
  providers: [
    { provide : HTTP_INTERCEPTORS, useClass : AuthInterceptorsService, multi : true },
    { provide : HTTP_INTERCEPTORS, useClass : ErrorInterceptor, multi : true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
