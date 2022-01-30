import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpParams, HttpClient } from '@angular/common/http';
import { Associado } from "../models/associado";


@Injectable({
    providedIn: 'root'
})
export class AssociadosService {

    private url = "https:localhost:44302/api/v1/Associado/"
    constructor(private http: HttpClient) { }

    obterAssociado(codigoAssociado: number): Observable<Associado>  {
        debugger;
        // var teste = this.http.get<Associado>(this.url + "GetAssociado/" + codigoAssociado.toString());
        return this.http.get<Associado>(this.url + "GetAssociado/" + codigoAssociado.toString());
    }

    alterarPlano(): Observable<Associado[]>  {
        return this.http.get<Associado[]>(this.url + "AlterarPlano");
    }
}