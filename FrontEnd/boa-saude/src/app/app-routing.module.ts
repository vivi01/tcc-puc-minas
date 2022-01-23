import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PlanoComponent } from './plano/plano.component';

const routes: Routes = [
  {
    path: 'plano',
    component: PlanoComponent,
    data: { title: 'Lista de planos' }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
