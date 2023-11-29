import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Assunto } from './Assunto.model';

@Injectable({
  providedIn: 'root',
})
export class AssuntoService {
  private apiUrl = '...'; 

  constructor(private http: HttpClient) {}

  getAssuntoes(): Observable<Assunto[]> {
    return this.http.get<Assunto[]>(`${this.apiUrl}/Assuntoes`);
  }

  getAssuntoById(id: number): Observable<Assunto> {
    return this.http.get<Assunto>(`${this.apiUrl}/Assuntoes/${id}`);
  }

  getAssuntoesPaginados(pagina: number, itensPorPagina: number): Observable<Assunto[]> {
    return this.http.get<Assunto[]>(`${this.apiUrl}/Assuntoes?page=${pagina}&itensPorPagina=${itensPorPagina}`);
  }

  criarAssunto(Assunto: Assunto): Observable<Assunto> {
    return this.http.post<Assunto>(`${this.apiUrl}/Assuntoes`, Assunto);
  }

  atualizarAssunto(id: number, Assunto: Assunto): Observable<Assunto> {
    return this.http.put<Assunto>(`${this.apiUrl}/Assuntoes/${id}`, Assunto);
  }

  excluirAssunto(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Assuntoes/${id}`);
  }
}
