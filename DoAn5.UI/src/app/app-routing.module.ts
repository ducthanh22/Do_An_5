import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path:'',redirectTo:'load/Home',pathMatch:'full'},

  // {
  //   path: '',

  //   data: {
  //     title: 'Default',
  //   },
    
  //   children: [
  //     {
  //       path: '',
  //       loadChildren: () =>
  //         import('./Layout/template/template.module').then(
  //           (HomeComponent) => HomeComponent.TemplateModule
  //         ),
  //     },
  //   ],
  // },
  // {path:'**',redirectTo:'client'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
