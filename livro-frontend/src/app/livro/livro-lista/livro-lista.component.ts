import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LivroService } from '../services/Livro.service';
import { Livro } from '../models/Livro.model';

@Component({
  selector: 'app-Livro-lista',
  templateUrl: './Livro-lista.component.html',
  styleUrls: ['./Livro-lista.component.scss']
})
export class LivroListaComponent implements OnInit {
  Livroes: Livro[] = [];
  loading: boolean = true;

  constructor(private LivroService: LivroService, private router: Router) {}

  ngOnInit(): void {
    this.carregarLivroes();
  }

  carregarLivroes(): void {
    this.LivroService.getLivroes().subscribe(
      (data: Livro[]) => {
        this.Livros = data;
        this.loading = false;
      },
      error => {
        console.error('Erro ao carregar Livroes:', error);
        this.loading = false;
      }
    );
  }

  verDetalhes(LivroId: number): void {
    this.router.navigate(['/Livroes', LivroId]);
  }

  editarLivro(LivroId: number): void {
    this.router.navigate(['/Livroes', LivroId, 'editar']);
  }

  excluirLivro(LivroId: number): void {
    if (confirm('Tem certeza de que deseja excluir este Livro?')) {
      this.LivroService.excluirLivro(LivroId).subscribe(
        () => {
          this.carregarLivroes();
        },
        error => {
          console.error('Erro ao excluir Livro:', error);
        }
      );
    }
  }
}
