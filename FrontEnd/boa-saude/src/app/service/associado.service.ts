import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpParams, HttpClient } from '@angular/common/http';
import { Associado } from "../models/associado";


@Injectable({
    providedIn: 'root'
})
export class AssociadosService {

    private url = "https:localhost:44339/api/v1/associados/"
    constructor(private http: HttpClient) { }

    obterAssociado(): Observable<Associado[]>  {
        return this.http.get<Associado[]>(this.url + "GetAssociado");
    }

    alterarPlano(): Observable<Associado[]>  {
        return this.http.get<Associado[]>(this.url + "AlterarPlano");
    }
}