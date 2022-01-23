
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpParams, HttpClient } from '@angular/common/http';
import { Plano } from "../models/plano";


@Injectable({
    providedIn: 'root'
})
export class PlanosService {
    private url = "https://localhost:44302/api/v1/planos";
    constructor(private http: HttpClient) { }

    obterPlanos(): Observable<Plano[]>  {
        return this.http.get<Plano[]>(this.url);
    }

    obterPlanoId(id: number){
        return this.http.get<Plano>(this.url + id.toString());
    }

    adicionarUsuario(plano: Plano ){
        return this.http.post<Plano>(this.url, plano);
    }

    atualizarPlano(id: number, plano: Plano){
        plano.id = id;
        return this.http.put<Plano>(this.url + id, plano);
    }

    excluirPlano(id: number) {
        return this.http.delete<Plano>(this.url + id.toString());
    }
    
}