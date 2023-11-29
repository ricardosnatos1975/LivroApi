import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Autor } from './autor.model';

@Injectable({
  providedIn: 'root',
})
export class AutorService {
  private apiUrl = '...'; 

  constructor(private http: HttpClient) {}

  getAutores(): Observable<Autor[]> {
    return this.http.get<Autor[]>(`${this.apiUrl}/autores`);
  }

  getAutorById(id: number): Observable<Autor> {
    return this.http.get<Autor>(`${this.apiUrl}/autores/${id}`);
  }

  getAutoresPaginados(pagina: number, itensPorPagina: number): Observable<Autor[]> {
    return this.http.get<Autor[]>(`${this.apiUrl}/autores?page=${pagina}&itensPorPagina=${itensPorPagina}`);
  }

  criarAutor(autor: Autor): Observable<Autor> {
    return this.http.post<Autor>(`${this.apiUrl}/autores`, autor);
  }

  atualizarAutor(id: number, autor: Autor): Observable<Autor> {
    return this.http.put<Autor>(`${this.apiUrl}/autores/${id}`, autor);
  }

  excluirAutor(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/autores/${id}`);
  }
}
