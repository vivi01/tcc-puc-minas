import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssociadoComponent } from './associado/associado.component';
import { AssociadoResolve } from './resolvers/associadoResolve';
import { PlanoResolve } from './resolvers/planoResolve';

const routes: Routes = [
  {
    path: 'associado',
    component: AssociadoComponent,
    resolve: {associados: AssociadoResolve, planos: PlanoResolve}
  },  
  { path: '',
    redirectTo: '/associado',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
 // providers: [AssociadoResolve,PlanoResolve]
})
export class AppRoutingModule { }
