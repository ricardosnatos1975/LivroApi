import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { LivroFormularioComponent } from './Livro-formulario.component';

describe('LivroFormularioComponent', () => {
  let component: LivroFormularioComponent;
  let fixture: ComponentFixture<LivroFormularioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormsModule],
      declarations: [LivroFormularioComponent],
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LivroFormularioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render form elements', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('h2').textContent).toContain('Formulário de Livro');
    expect(compiled.querySelector('form')).toBeTruthy();
    expect(compiled.querySelector('label[for="nome"]').textContent).toContain('Nome:');
    expect(compiled.querySelector('textarea[name="biografia"]')).toBeTruthy();
    expect(compiled.querySelector('label[for="dataNascimento"]').textContent).toContain('Data de Nascimento:');
    expect(compiled.querySelector('button[type="submit"]').textContent).toContain('Salvar Livro');
  });

  it('should bind Livro properties to form controls', () => {
    const compiled = fixture.nativeElement;
    component.Livro = { titulo: 'Titulo Teste', dataPublicacao: '2000-01-01', titulo: 'Título Teste' };
    fixture.detectChanges();

    expect(compiled.querySelector('input[name="nome"]').value).toBe('Livro Teste');
    expect(compiled.querySelector('textarea[name="titulo"]').value).toBe('Titulo teste');
    expect(compiled.querySelector('input[name="dataPublicacao"]').value).toBe('2000-01-01');
  });

  it('should emit submit event on form submission', () => {
    spyOn(component.LivroSubmit, 'emit');
    const compiled = fixture.nativeElement;
    compiled.querySelector('form').dispatchEvent(new Event('submit'));
    expect(component.LivroSubmit.emit).toHaveBeenCalled();
  });
});
