import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Autor } from './autor.model';

@Component({
  selector: 'app-autor-formulario',
  templateUrl: './autor-formulario.component.html',
  styleUrls: ['./autor-formulario.component.css']
})
export class AutorFormularioComponent implements OnInit {
  @Input() autor: Autor = { nome: '', biografia: '', dataNascimento: '' };
  @Output() autorSubmit: EventEmitter<Autor> = new EventEmitter<Autor>();

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.autorSubmit.emit(this.autor);
    }
  }
}
