import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AutorService } from '../services/autor.service';
import { Autor } from '../models/autor.model';

@Component({
  selector: 'app-autor-lista',
  templateUrl: './autor-lista.component.html',
  styleUrls: ['./autor-lista.component.scss']
})
export class AutorListaComponent implements OnInit {
  autores: Autor[] = [];
  loading: boolean = true;

  constructor(private autorService: AutorService, private router: Router) {}

  ngOnInit(): void {
    this.carregarAutores();
  }

  carregarAutores(): void {
    this.autorService.getAutores().subscribe(
      (data: Autor[]) => {
        this.autores = data;
        this.loading = false;
      },
      error => {
        console.error('Erro ao carregar autores:', error);
        this.loading = false;
      }
    );
  }

  verDetalhes(autorId: number): void {
    this.router.navigate(['/autores', autorId]);
  }

  editarAutor(autorId: number): void {
    this.router.navigate(['/autores', autorId, 'editar']);
  }

  excluirAutor(autorId: number): void {
    if (confirm('Tem certeza de que deseja excluir este autor?')) {
      this.autorService.excluirAutor(autorId).subscribe(
        () => {
          this.carregarAutores();
        },
        error => {
          console.error('Erro ao excluir autor:', error);
        }
      );
    }
  }
}
