import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { assuntoFormularioComponent } from './assunto-formulario.component';

describe('assuntoFormularioComponent', () => {
  let component: assuntoFormularioComponent;
  let fixture: ComponentFixture<assuntoFormularioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormsModule],
      declarations: [assuntoFormularioComponent],
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(assuntoFormularioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render form elements', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('h2').textContent).toContain('Formulário de assunto');
    expect(compiled.querySelector('form')).toBeTruthy();
    expect(compiled.querySelector('label[for="idAssunto"]').textContent).toContain('Nome:');
    expect(compiled.querySelector('textarea[name="descricao"]')).toBeTruthy();
     expect(compiled.querySelector('button[type="submit"]').textContent).toContain('Salvar assunto');
  });

  it('should bind assunto properties to form controls', () => {
    const compiled = fixture.nativeElement;
    component.assunto = { idAssunto: 'assunto Teste', descricao: 'Biografia teste'};
    fixture.detectChanges();

    expect(compiled.querySelector('input[name="nome"]').value).toBe('assunto Teste');
    expect(compiled.querySelector('textarea[name="biografia"]').value).toBe('Biografia teste');
  });

  it('should emit submit event on form submission', () => {
    spyOn(component.assuntoSubmit, 'emit');
    const compiled = fixture.nativeElement;
    compiled.querySelector('form').dispatchEvent(new Event('submit'));
    expect(component.assuntoSubmit.emit).toHaveBeenCalled();
  });
});
