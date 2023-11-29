import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { livroService } from '../livro.service';

@Component({
  selector: 'app-livro-detalhes',
  templateUrl: './livro-detalhes.component.html',
  styleUrls: ['./livro-detalhes.component.scss']
})
export class livroDetalhesComponent implements OnInit {
  livroId: number;
  livro: livro;

  constructor(private route: ActivatedRoute, private livroService: livroService) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.livroId = +params['id'];
      this.carregarDetalheslivro();
    });
  }

  carregarDetalheslivro(): void {
    this.livroService.getlivroById(this.livroId).subscribe(livro => {
      this.livro = livro;
    });
  }
}
