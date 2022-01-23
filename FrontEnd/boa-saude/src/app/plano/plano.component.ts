import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { Plano } from "../models/plano";
import  {FormBuilder, Validators} from '@angular/forms' ; 
import { PlanosService } from "../service/plano.service";

@Component({
    selector: 'app-plano',
    templateUrl: './plano.component.html',
    styleUrls: ['./plano.component.scss']
})
export class PlanoComponent implements OnInit {

    displayedColumns: string[] = ['descricao', 'tipoPlano', 'classificacaoPlano', 'valorBase', 'editar', 'detalhe', 'excluir'];
    planos: Plano[];

    constructor(private planosService: PlanosService) {}
    ngOnInit(){
        
    }

    obterPlanos(){
        this.planosService.obterPlanos()
            .subscribe(res => {
                this.planos = res;
            });
    }

}