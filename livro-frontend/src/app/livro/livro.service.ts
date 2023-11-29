import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { livro } from './livro.model';

@Injectable({
  providedIn: 'root',
})
export class livroService {
  private apiUrl = '...'; 

  constructor(private http: HttpClient) {}

  getlivroes(): Observable<livro[]> {
    return this.http.get<livro[]>(`${this.apiUrl}/livros`);
  }

  getlivroById(id: number): Observable<livro> {
    return this.http.get<livro>(`${this.apiUrl}/livros/${id}`);
  }

  getlivroesPaginados(pagina: number, itensPorPagina: number): Observable<livro[]> {
    return this.http.get<livro[]>(`${this.apiUrl}/livros?page=${pagina}&itensPorPagina=${itensPorPagina}`);
  }

  criarlivro(livro: livro): Observable<livro> {
    return this.http.post<livro>(`${this.apiUrl}/livros`, livro);
  }

  atualizarlivro(id: number, livro: livro): Observable<livro> {
    return this.http.put<livro>(`${this.apiUrl}/livros/${id}`, livro);
  }

  excluirlivro(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/livros/${id}`);
  }
}
