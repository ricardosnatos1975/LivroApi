import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { assunto } from './assunto.model';

@Component({
  selector: 'app-assunto-formulario',
  templateUrl: './assunto-formulario.component.html',
  styleUrls: ['./assunto-formulario.component.css']
})
export class assuntoFormularioComponent implements OnInit {
  @Input() assunto: assunto = { idAssunto: '', descricao: '' };
  @Output() assuntoSubmit: EventEmitter<assunto> = new EventEmitter<assunto>();

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.assuntoSubmit.emit(this.assunto);
    }
  }
}
