import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpParams, HttpClient, HttpHeaders } from '@angular/common/http';
import { Associado } from "../models/associado";
import { Plano } from "../models/plano";
import { AlterarPlano } from "../models/alterarPlano";


@Injectable({
    providedIn: 'root'
})
export class AssociadosService {

    private url = "https:localhost:44302/api/v1/Associado/"
    constructor(private http: HttpClient) { }
    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
      }

    obterAssociado(codigoAssociado: number): Observable<Associado>  {
        return this.http.get<Associado>(this.url + "GetAssociado/" + codigoAssociado.toString(), this.httpOptions);
    }

    alterarPlano(alterarPlano: AlterarPlano): Observable<Associado>  {
        return this.http.post<Associado>(this.url + "AlterarPlano", alterarPlano);
    }

    getValorNovoPlano(dataNascimento: Date, plano: Plano, planoOdonto: boolean): Observable<number>{ 
        this.httpOptions = {
            headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
          };
    
        return this.http.post<number>(this.url + "GetNovoValorPlano", plano, {            
            params: {
                dataNascimento: dataNascimento.toString(),
                planoOdontologico: planoOdonto.toString()
            }});
    }
}