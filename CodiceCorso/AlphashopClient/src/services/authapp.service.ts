import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthappService {

  constructor() { }

  autentica(userid : string, password : string) : boolean {
    var retVal = (userid === "Nicola" && password === "123") ? true : false;
    
    if(retVal) {
      sessionStorage.setItem("Utente", userid);
    }

    return retVal;
  }

  //loggedUser = () : string | null => (sessionStorage.getItem("Utente")) ? sessionStorage.getItem("Utente") : "";
  loggedUser() {
    let utenteSessione = sessionStorage.getItem("Utente");

    if(utenteSessione === "") 
      return null;
    else 
      return utenteSessione;
  }

  //isLogged = () : boolean => (sessionStorage.getItem("Utente")) ? true : false;
  isLogged() : boolean {
    let utenteSessione = sessionStorage.getItem("Utente");

    if(utenteSessione) 
      return true;
    else 
      return false;
  }

  clearUser() {
    sessionStorage.removeItem("Utente");
  }

  clearAll() {
    sessionStorage.clear();
  }

}
