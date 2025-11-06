import { Component, signal, inject } from '@angular/core';
import { CategoriaService } from '../categoria/categoria.service';
import { Categoria } from '../categoria/categoria.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})

  // Responsável por gerenciar a aplicação.
  // Implementa a lógica para buscar e armazenar as categorias.
export class App {
  protected readonly title = signal('Frontend');
  
  // Injeta o serviço que busca os dados.
  private readonly categoriaService = inject(CategoriaService);

  public categorias = signal<Categoria[]>([]);

  public buscarCategorias(): void {
    this.categoriaService.getCategorias()
      .subscribe({
        next: (dados) => {
          this.categorias.set(dados);
          console.log('Categorias carregadas!', dados);
        },
        error: (err) => {
          console.error('Erro ao buscar categorias:', err);
        }
      });
  }
}
