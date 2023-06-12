import { NgModule } from '@angular/core';
import { RouterModule, Routes, CanActivate } from '@angular/router';
import { Contenuto2Component } from './contenuto2/contenuto2.component';
import { Contenuto1Component } from './contenuto1/contenuto1.component';
import { NothumanComponent } from './nothuman/nothuman.component';
import { AppComponent } from './app.component';
import { NavigateService } from './service/navigate.service';
import { RouteGuardService } from './service/route-guard.service';

const routes: Routes = [
  { path : "benvenuto", component : AppComponent},
  { path : "contenuto1", component : Contenuto1Component, canActivate : [RouteGuardService]},
  { path : "contenuto2", component : Contenuto2Component },
  { path : "err", component : NothumanComponent },
  { path : "**", redirectTo : "err" },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
