import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Plano } from "../models/plano";

@Injectable({
    providedIn: 'root'
})
export class PlanosService {

    private url = "https:localhost:44302/api/v1/Plano/"
    constructor(private http: HttpClient) { }
    httpOptions = {
        headers: new HttpHeaders({'Content-Type':'application/json; charset=utf-8'})
      }

    async obterPlanos(): Promise<Plano[]>  {
            const listaPlanos: Plano[] = [];
            const dadosApi = await this.http.get<Plano[]>(this.url + "GetTodosPlanos").toPromise().then(dataApi => {
                dataApi.forEach(plano => {
                        listaPlanos.push(plano);
                });
            })
            return listaPlanos;
        }

    getPlano(id: number): Observable<Plano>  {
        console.log("GetPlano")
        return this.http.get<Plano>(this.url + "Get/"+ id.toString());
    }
}