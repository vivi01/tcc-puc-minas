import { Plano } from "./plano";

export class Associado {
    id: number;
    nome: string;
    dataNascimento: Date;
    codigoAssociado: number;
    cpf: number;
    plano: Plano;
}