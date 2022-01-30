import { Component, OnInit } from "@angular/core";
import { Associado } from "../models/associado";
import { AssociadosService } from "../service/associado.service";

@Component({
    selector: 'app-associado',
    templateUrl: './associado.component.html',
    styleUrls: ['./associado.component.scss']
})
export class AssociadoComponent implements OnInit {

    usuarioId: number;
    // usuarioForm: FormGroup;
    nomeAssociado: String = '';
    dataNascimento: Date;
    cpf: number;
    // telefone: String = '';
    // sexo: String = '';
    // dataNascimento: String = '';

    // associado: Associado[];
    associado: any;
    constructor(private associadosService: AssociadosService) {}

    ngOnInit(){
        this.obterAssociado();
    }

    obterAssociado(){
        debugger;
        // var teste = this.associadosService.obterAssociado(290).pipe(
        //     map(data => data['name'])
        // );
        

        this.associadosService.obterAssociado(290)
            .subscribe(data => {
                this.nomeAssociado = data.nome,
                this.dataNascimento = data.dataNascimento,
                this.cpf = data.cpf
            });
    }

    alterarPlano(){
        this.associadosService.alterarPlano()
        .subscribe(res => {
            this.associado = res;
        });
    }
}