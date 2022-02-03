import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { Plano } from '../models/plano';
import { PlanosService } from '../service/plano.service';

@Injectable()
export class PlanoResolve implements Resolve<Plano> {

   constructor(public planoService: PlanosService) { }

   resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot)
      : Observable<any> | Promise<any> | any {
      return this.planoService.obterPlanos();
   }
}