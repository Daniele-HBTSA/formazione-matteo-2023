import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { LoggatiComponent } from './loggati/loggati.component';
import { RegistratiComponent } from './registrati/registrati.component';
import { HomeComponent } from './home/home.component';
import { WrapperComponent } from './wrapper/wrapper.component';
import { FormComponent } from './form/form.component';
import { BottoneLogInComponent } from './bottone-log-in/bottone-log-in.component';
import { BottoneRegisterComponent } from './bottone-register/bottone-register.component';

@NgModule({
  declarations: [
    AppComponent,
    LoggatiComponent,
    RegistratiComponent,
    HomeComponent,
    WrapperComponent,
    FormComponent,
    BottoneLogInComponent,
    BottoneRegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
