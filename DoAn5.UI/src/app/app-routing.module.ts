import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path:'',redirectTo:'client/Home',pathMatch:'full'},
  { path: 'ResetPassword', redirectTo: 'ResetPassword', pathMatch: 'prefix' },
 
  // {path:'**',redirectTo:'client/Home',pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
