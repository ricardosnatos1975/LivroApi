import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { assuntoListaComponent } from './assunto-lista/assunto-lista.component';
import { assuntoDetalhesComponent } from './assunto-detalhes/assunto-detalhes.component';
import { assuntoFormularioComponent } from './assunto-formulario/assunto-formulario.component';

const routes: Routes = [
  { path: 'assuntos', component: assuntoListaComponent },
  { path: 'assuntos/:id', component: assuntoDetalhesComponent },
  { path: 'adicionar-assunto', component: assuntoFormularioComponent },
  { path: 'editar-assunto/:id', component: assuntoFormularioComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class assuntoRoutingModule {}
