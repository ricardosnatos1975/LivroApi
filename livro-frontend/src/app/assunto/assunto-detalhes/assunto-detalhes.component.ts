import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { assuntoService } from '../assunto.service';

@Component({
  selector: 'app-assunto-detalhes',
  templateUrl: './assunto-detalhes.component.html',
  styleUrls: ['./assunto-detalhes.component.scss']
})
export class assuntoDetalhesComponent implements OnInit {
  assuntoId: number;
  assunto: assunto;

  constructor(private route: ActivatedRoute, private assuntoService: assuntoService) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.assuntoId = +params['id'];
      this.carregarDetalhesassunto();
    });
  }

  carregarDetalhesassunto(): void {
    this.assuntoService.getassuntoById(this.assuntoId).subscribe(assunto => {
      this.assunto = assunto;
    });
  }
}
