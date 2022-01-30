import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssociadoComponent } from './associado/associado.component';

const routes: Routes = [
  {
    path: 'associado',
    component: AssociadoComponent,
    data: { title: 'Associado' }
  },
  { path: '',
    redirectTo: '/associado',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
