export interface Autor {
    id: number;
    nome: string;
    sobrenome: string;
    nacionalidade: string;
    dataNascimento: Date;
    livrosPublicados: Livro[];
  }
  
  