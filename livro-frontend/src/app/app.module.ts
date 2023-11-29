import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from 'app-routing.module';
import { LivroModule } from './livro/livro.module';
import { AutorModule } from './autor/autor.module';
import { AssuntoModule } from './assunto/assunto.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LivroModule,
    AutorModule,
    AssuntoModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
