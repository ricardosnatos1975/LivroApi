import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AutorService } from '../autor.service';

@Component({
  selector: 'app-autor-detalhes',
  templateUrl: './autor-detalhes.component.html',
  styleUrls: ['./autor-detalhes.component.scss']
})
export class AutorDetalhesComponent implements OnInit {
  autorId: number;
  autor: Autor;

  constructor(private route: ActivatedRoute, private autorService: AutorService) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.autorId = +params['id'];
      this.carregarDetalhesAutor();
    });
  }

  carregarDetalhesAutor(): void {
    this.autorService.getAutorById(this.autorId).subscribe(autor => {
      this.autor = autor;
    });
  }
}
