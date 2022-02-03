import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { Associado } from '../models/associado';
import { AssociadosService } from '../service/associado.service';

@Injectable()
export class AssociadoResolve implements Resolve<Associado> {

   constructor(public associadoService: AssociadosService) { }

   resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot)
      : Observable<Associado> | Promise<Associado> | Associado {
      return this.associadoService.obterAssociado(290);
   }
}