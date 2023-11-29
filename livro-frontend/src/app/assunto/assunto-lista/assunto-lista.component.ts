import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { assuntoService } from '../services/assunto.service';
import { assunto } from '../models/assunto.model';

@Component({
  selector: 'app-assunto-lista',
  templateUrl: './assunto-lista.component.html',
  styleUrls: ['./assunto-lista.component.scss']
})
export class assuntoListaComponent implements OnInit {
  assuntoes: assunto[] = [];
  loading: boolean = true;

  constructor(private assuntoService: assuntoService, private router: Router) {}

  ngOnInit(): void {
    this.carregarassuntoes();
  }

  carregarassuntoes(): void {
    this.assuntoService.getassuntoes().subscribe(
      (data: assunto[]) => {
        this.assuntoes = data;
        this.loading = false;
      },
      error => {
        console.error('Erro ao carregar assuntoes:', error);
        this.loading = false;
      }
    );
  }

  verDetalhes(assuntoId: number): void {
    this.router.navigate(['/assuntos', assuntoId]);
  }

  editarassunto(assuntoId: number): void {
    this.router.navigate(['/assuntos', assuntoId, 'editar']);
  }

  excluirassunto(assuntoId: number): void {
    if (confirm('Tem certeza de que deseja excluir este assunto?')) {
      this.assuntoService.excluirassunto(assuntoId).subscribe(
        () => {
          this.carregarassuntoes();
        },
        error => {
          console.error('Erro ao excluir assunto:', error);
        }
      );
    }
  }
}
