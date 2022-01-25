import { Component, OnInit } from "@angular/core";
import { AssociadosService } from "../service/associado.service";

@Component({
    selector: 'app-associado',
    templateUrl: './associado.component.html',
    styleUrls: ['./associado.component.scss']
})
export class AssociadoComponent implements OnInit {

    // associado: Associado[];
    associado: any;
    constructor(private associadosService: AssociadosService) {}

    ngOnInit(){
        this.obterAssociado();
    }

    obterAssociado(){
        this.associadosService.obterAssociado()
            .subscribe(res => {
                this.associado = res;
            });
    }

    alterarPlano(){
        this.associadosService.alterarPlano()
        .subscribe(res => {
            this.associado = res;
        });
    }
}