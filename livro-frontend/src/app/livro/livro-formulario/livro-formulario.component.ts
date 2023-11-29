import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { livro } from './livro.model';

@Component({
  selector: 'app-livro-formulario',
  templateUrl: './livro-formulario.component.html',
  styleUrls: ['./livro-formulario.component.css']
})
export class livroFormularioComponent implements OnInit {
  @Input() livro: livro = { titulo: '', dataPublicacao: '' };
  @Output() livroSubmit: EventEmitter<livro> = new EventEmitter<livro>();

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.livroSubmit.emit(this.livro);
    }
  }
}
