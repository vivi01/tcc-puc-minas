import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpParams, HttpClient, HttpHeaders } from '@angular/common/http';
import { Associado } from "../models/associado";


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

    alterarPlano(): Observable<Associado>  {
        return this.http.get<Associado>(this.url + "AlterarPlano");
    }
}