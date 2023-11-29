import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// Importando os componentes específicos de cada recurso
import { LivroListaComponent } from './livro/livro-lista/livro-lista.component';
import { LivroDetalhesComponent } from './livro/livro-detalhes/livro-detalhes.component';
import { LivroFormularioComponent } from './livro/livro-formulario/livro-formulario.component';

import { AssuntoListaComponent } from './assunto/assunto-lista/assunto-lista.component';
import { AssuntoDetalhesComponent } from './assunto/assunto-detalhes/assunto-detalhes.component';
import { AssuntoFormularioComponent } from './assunto/assunto-formulario/assunto-formulario.component';

import { AutorListaComponent } from './autor/autor-lista/autor-lista.component';
import { AutorDetalhesComponent } from './autor/autor-detalhes/autor-detalhes.component';
import { AutorFormularioComponent } from './autor/autor-formulario/autor-formulario.component';

@NgModule({
  declarations: [
    AppComponent,
    LivroListaComponent,
    LivroDetalhesComponent,
    LivroFormularioComponent,
    AssuntoListaComponent,
    AssuntoDetalhesComponent,
    AssuntoFormularioComponent,
    AutorListaComponent,
    AutorDetalhesComponent,
    AutorFormularioComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
