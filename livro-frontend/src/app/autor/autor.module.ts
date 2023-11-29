import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AutorListaComponent } from './autor-lista/autor-lista.component';
import { AutorDetalhesComponent } from './autor-detalhes/autor-detalhes.component';
import { AutorFormularioComponent } from './autor-formulario/autor-formulario.component';
import { AutorRoutingModule } from './autor-routing.module';

@NgModule({
  declarations: [
    AutorListaComponent,
    AutorDetalhesComponent,
    AutorFormularioComponent,
  ],
  imports: [CommonModule, AutorRoutingModule],
})
export class AutorModule {}
