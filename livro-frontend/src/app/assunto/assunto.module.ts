import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AssuntoListaComponent } from './assunto-lista/assunto-lista.component';
import { AssuntoDetalhesComponent } from './assunto-detalhes/assunto-detalhes.component';
import { AssuntoFormularioComponent } from './assunto-formulario/assunto-formulario.component';

@NgModule({
  declarations: [
    AssuntoListaComponent,
    AssuntoDetalhesComponent,
    AssuntoFormularioComponent
  ],
  imports: [
    CommonModule
  ]
})
export class AssuntoModule { }
