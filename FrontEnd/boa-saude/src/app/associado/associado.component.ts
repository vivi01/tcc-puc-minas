import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { AlterarPlano } from "../models/alterarPlano";
import { Associado } from "../models/associado";
import { Plano } from "../models/plano";
import { AssociadosService } from "../service/associado.service";
import { PlanosService } from "../service/plano.service";

@Component({
    selector: 'app-associado',
    templateUrl: './associado.component.html',
    styleUrls: ['./associado.component.scss']
})
export class AssociadoComponent implements OnInit {

    associadoForm:FormGroup;
    associadoId: number;
    nomeAssociado: String = '';
    dataNascimento: Date;
    cpfCnpj: string;
    descricao: string = '';
    tipoPlano: number;
    valorAtual: number;
    plano: Plano;
    possuiPlanoOdontologico: boolean;
    valorNovo: number;
    associado: any;
    listPlanos: Plano[]; 
    planoId: number;
    planoDescricao: string;
    novoPlano: Plano;
    constructor(private associadosService: AssociadosService, private planosService: PlanosService, private formBuilder: FormBuilder) {}

    ngOnInit(){
        this.associadoForm = this.formBuilder.group({
            'associadoId':null,
            'nomeAssociado':null,
            'dataNascimento':null,
            'cpfCnpj':null,
            'descricao':null,
            'tipoPlano':null,
            'valorAtual':null,
            'planoId':null,
            'plano':null,
            'possuiPlanoOdontologico':null,
            'valorNovo':null,
            'planoDescricao': null
        });
        this.associadoForm.get('planoId')?.setValue("");
        this.associadoForm.get('possuiPlanoOdontologico')?.setValue(false);
        this.obterAssociado();
        this.desabilitaCampos();
        this.obterPlanos();
    }

    obterAssociado(){
        this.associadosService.obterAssociado(290)
            .subscribe(data => {
                this.associadoId = data.id,
                this.nomeAssociado = data.nome,
                this.dataNascimento = data.dataNascimento,
                this.cpfCnpj = data.cpfCnpj,
                this.descricao = data.plano.descricao,
                this.tipoPlano = data.plano.tipoPlano,
                this.valorAtual = data.plano.valorBase,
                this.plano = data.plano,
                this.possuiPlanoOdontologico = false,
                this.valorNovo = 250
            });
    }

    updatePlano(){
        debugger;
        var alterarPlano = new AlterarPlano();
        alterarPlano.codigoAssociado =  this.associadoId;
        alterarPlano.codigoNovoPlano = this.associadoForm.get('planoId')?.value;
        alterarPlano.planoOdontologico = this.associadoForm.get('possuiPlanoOdontologico')?.value;
        // this.associadosService.alterarPlano()
        // .subscribe(res => {
        //     this.associado = res;
        // });
    }

    GetValorNovoPlano(){     
        var idPlano = this.associadoForm.get('planoId')?.value;
        if (idPlano != "")
        {
            this.planosService.getPlano(idPlano)
            .subscribe(res => {
                this.novoPlano = res;
            });
            console.log(this.novoPlano);
            var planoOdonto = this.associadoForm.get('possuiPlanoOdontologico')?.value;

            this.associadosService.getValorNovoPlano(this.dataNascimento, this.novoPlano, planoOdonto) 
            .subscribe(res => {
                this.associadoForm.controls['valorNovo'].setValue(res);          
            });
        }
        else
        {
            this.associadoForm.controls['valorNovo'].setValue("");
        }
        
    }      

    obterPlanos(){
        this.planosService.obterPlanos()
            .subscribe(res => {
                this.listPlanos = res;
            });
    }

    desabilitaCampos(){
        this.associadoForm.get('nomeAssociado')?.disable();
        this.associadoForm.get('dataNascimento')?.disable();
        this.associadoForm.get('dataNascimento')?.disable();
        this.associadoForm.get('cpfCnpj')?.disable();
        this.associadoForm.get('descricao')?.disable();
        this.associadoForm.get('tipoPlano')?.disable();
        this.associadoForm.get('valorAtual')?.disable();
        this.associadoForm.get('valorNovo')?.disable();
    }
}