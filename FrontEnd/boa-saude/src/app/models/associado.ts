import { Plano } from "./plano";

export class Associado {
    id: number;
    nome: string;
    dataNascimento: Date;
    codigoAssociado: number;
    cpfCnpj: string;
    plano: Plano;
    possuiPlanoOdontologico: boolean;
}