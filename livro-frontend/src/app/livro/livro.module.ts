import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LivroListaComponent } from './livro-lista/livro-lista.component';
import { LivroDetalhesComponent } from './livro-detalhes/livro-detalhes.component';
import { LivroFormularioComponent } from './livro-formulario/livro-formulario.component';

@NgModule({
  declarations: [
    LivroListaComponent,
    LivroDetalhesComponent,
    LivroFormularioComponent
  ],
  imports: [
    CommonModule
  ]
})
export class LivroModule { }
