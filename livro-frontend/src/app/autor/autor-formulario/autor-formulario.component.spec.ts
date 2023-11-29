import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { AutorFormularioComponent } from './autor-formulario.component';

describe('AutorFormularioComponent', () => {
  let component: AutorFormularioComponent;
  let fixture: ComponentFixture<AutorFormularioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormsModule],
      declarations: [AutorFormularioComponent],
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AutorFormularioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render form elements', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('h2').textContent).toContain('Formulário de Autor');
    expect(compiled.querySelector('form')).toBeTruthy();
    expect(compiled.querySelector('label[for="nome"]').textContent).toContain('Nome:');
    expect(compiled.querySelector('textarea[name="biografia"]')).toBeTruthy();
    expect(compiled.querySelector('label[for="dataNascimento"]').textContent).toContain('Data de Nascimento:');
    expect(compiled.querySelector('button[type="submit"]').textContent).toContain('Salvar Autor');
  });

  it('should bind autor properties to form controls', () => {
    const compiled = fixture.nativeElement;
    component.autor = { nome: 'Autor Teste', biografia: 'Biografia teste', dataNascimento: '2000-01-01' };
    fixture.detectChanges();

    expect(compiled.querySelector('input[name="nome"]').value).toBe('Autor Teste');
    expect(compiled.querySelector('textarea[name="biografia"]').value).toBe('Biografia teste');
    expect(compiled.querySelector('input[name="dataNascimento"]').value).toBe('2000-01-01');
  });

  it('should emit submit event on form submission', () => {
    spyOn(component.autorSubmit, 'emit');
    const compiled = fixture.nativeElement;
    compiled.querySelector('form').dispatchEvent(new Event('submit'));
    expect(component.autorSubmit.emit).toHaveBeenCalled();
  });
});
