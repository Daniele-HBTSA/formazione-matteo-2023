import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { FormDatiComponent } from './form-dati/form-dati.component';

const routes: Routes = [
  { path : "financeapp", component : AppComponent },
  { path : "financeapp1", component : FormDatiComponent },
  { path : "**", component : AppComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
