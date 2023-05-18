import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WrapperComponent } from './wrapper/wrapper.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path : '', component: WrapperComponent },
  { path : 'home', component : HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
