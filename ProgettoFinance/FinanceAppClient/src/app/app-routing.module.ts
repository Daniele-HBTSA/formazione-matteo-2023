import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TabellaMovimentiComponent } from './tabella-movimenti/tabella-movimenti.component';
import { AppComponent } from './app.component';

const routes: Routes = [
  {path : "" , component : AppComponent},
  {path : "tabella/", component : TabellaMovimentiComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }