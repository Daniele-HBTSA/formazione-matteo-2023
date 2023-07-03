import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorsService implements HttpInterceptor{

  constructor(private auth : AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let authHeader : string = "";
    const token = localStorage.getItem("AccessToken");

    if(token != null) {
      authHeader = "Bearer " + token;
    }

    if(this.auth.utenteLoggato) {
      req = req.clone({
        setHeaders : {Authorization : authHeader}
      });
    }

    return next.handle(req);
  }



}
