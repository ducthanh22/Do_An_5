import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientClienttemplateComponent } from './client-clienttemplate/client-clienttemplate.component';
import { AdminAdmintemplateComponent } from './admin-admintemplate/admin-admintemplate.component';

const routes: Routes = [
  // {path:'client',component:ClientClienttemplateComponent},
  { path: 'admin', component: AdminAdmintemplateComponent },
  {
    path: '',
    data: {
      title: 'Trang',
    },
    children: [
      {
        path: '',
        component: ClientClienttemplateComponent,
        data: {
          title: 'Phiếu',
        },
        children: [
          {
            path: 'load',
            loadChildren: () =>
              import('../../modules/client/client.module').then(
                (x) => x.ClientModule
              ),
          },
        ],
      },
      
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TemplateRoutingModule { }
