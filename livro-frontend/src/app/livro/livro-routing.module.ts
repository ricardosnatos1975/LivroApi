import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { livroListaComponent } from './livro-lista/livro-lista.component';
import { livroDetalhesComponent } from './livro-detalhes/livro-detalhes.component';
import { livroFormularioComponent } from './livro-formulario/livro-formulario.component';

const routes: Routes = [
  { path: 'livros', component: livroListaComponent },
  { path: 'livros/:id', component: livroDetalhesComponent },
  { path: 'adicionar-livro', component: livroFormularioComponent },
  { path: 'editar-livro/:id', component: livroFormularioComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class livroRoutingModule {}
