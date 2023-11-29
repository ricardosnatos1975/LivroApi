// autor-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutorListaComponent } from './autor-lista/autor-lista.component';
import { AutorDetalhesComponent } from './autor-detalhes/autor-detalhes.component';
import { AutorFormularioComponent } from './autor-formulario/autor-formulario.component';

const routes: Routes = [
  { path: 'autores', component: AutorListaComponent },
  { path: 'autores/:id', component: AutorDetalhesComponent },
  { path: 'adicionar-autor', component: AutorFormularioComponent },
  { path: 'editar-autor/:id', component: AutorFormularioComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AutorRoutingModule {}
