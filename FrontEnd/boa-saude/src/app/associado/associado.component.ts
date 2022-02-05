import { Component, OnDestroy, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from "@angular/router";
import { Subscription } from "rxjs";
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
export class AssociadoComponent implements OnInit, OnDestroy {

    associadoForm: FormGroup;
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
    associado: Associado;
    listPlanos: Plano[];
    planoId: number;
    planoDescricao: string;
    novoPlano: Plano;
    inscricao: Subscription;
    inscricaoPlano: Subscription;   
    codigoAssociado: number;

    constructor(private route: ActivatedRoute, private router: Router,
        private associadosService: AssociadosService,
        private planosService: PlanosService, private formBuilder: FormBuilder) { }

    ngOnDestroy(): void {
        this.inscricao.unsubscribe();
    }

    ngOnInit() {
        this.obterAssociado();
        this.associadoForm.get('planoId')?.setValue("");
        this.associadoForm.get('possuiPlanoOdontologico')?.setValue(false);
        this.desabilitaCampos();        
    }

    obterAssociado() {
        this.inscricao = this.route.data.subscribe(
            (info) => {
                console.log(info);
                this.associado = info.associados;
                this.codigoAssociado = this.associado["codigoAssociado"];
                this.associadoForm = this.formBuilder.group({
                    'associadoId': this.associado["id"],
                    'nomeAssociado': this.associado["nome"],
                    'dataNascimento': this.associado["dataNascimento"],
                    'cpfCnpj': this.associado["cpfCnpj"],
                    'descricao': this.associado["plano"]["descricao"],
                    'tipoPlano': this.associado["plano"]["tipoPlano"],
                    'valorAtual': this.associado["valorPlano"],
                    'planoId': null,
                    'plano': this.associado["plano"],
                    'possuiPlanoOdontologico': false,
                    'valorNovo': null,
                    'planoDescricao': null,
                    'codigoAssociado': this.associado["codigoAssociado"]
                });
                this.dataNascimento = this.associado["dataNascimento"];
                this.obterPlanos();
            }
        );
    }

    updatePlano() {
        var alterarPlano = new AlterarPlano();
        alterarPlano.codigoAssociado = this.codigoAssociado;
        alterarPlano.codigoNovoPlano = this.associadoForm.get('planoId')?.value;
        alterarPlano.planoOdontologico = this.associadoForm.get('possuiPlanoOdontologico')?.value;
        this.associadosService.alterarPlano(alterarPlano)
            .subscribe(res => {
                this.associado = res;
            });
            location.reload();
    }

    GetValorNovoPlano() {
        var idPlano = this.associadoForm.get('planoId')?.value;
        var planoOdonto = this.associadoForm.get('possuiPlanoOdontologico')?.value;
        console.log(idPlano);
        if (idPlano != "") {
            this.planosService.getPlano(idPlano)
                .subscribe(res => {
                    this.novoPlano = res;
                    this.associadosService.getValorNovoPlano(this.dataNascimento, this.novoPlano, planoOdonto)
                        .subscribe(res => {
                            this.associadoForm.controls['valorNovo'].setValue(res);
                        });
                });
        }
        else {
            this.associadoForm.controls['valorNovo'].setValue("");
        }
    }

    async obterPlanos() {
        await this.planosService.obterPlanos().then(data => {
            this.listPlanos = data;
        })
    }

    desabilitaCampos() {
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