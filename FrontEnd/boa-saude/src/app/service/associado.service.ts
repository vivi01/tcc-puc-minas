import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpParams, HttpClient, HttpHeaders } from '@angular/common/http';
import { Associado } from "../models/associado";
import { Plano } from "../models/plano";


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

    getValorNovoPlano(dataNascimento: Date, plano: Plano, planoOdonto: boolean): Observable<number>{ 
        this.httpOptions = {
            headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
            // HttpParams: new HttpParams{}
          };
    
        return this.http.post<number>(this.url + "GetNovoValorPlano", plano, {            
            params: {
                dataNascimento: '12/12/2000',
                planoOdontologico: "true"
            }});
    }
}