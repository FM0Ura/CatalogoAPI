import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { trigger, transition, style, animate, query, stagger } from '@angular/animations';

// Interface para tipar os dados da categoria
export interface Categoria {
  categoriaId: number;
  nome: string;
  imagemUrl: string;
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css',
  animations: [
    trigger('listAnimation', [
      transition('* => *', [ // Executa a cada mudança de estado
        query(':enter', [
          style({ opacity: 0, transform: 'translateY(-15px)' }),
          stagger('100ms', [
            animate('300ms ease-out', style({ opacity: 1, transform: 'translateY(0)' }))
          ])
        ], { optional: true })
      ])
    ])
  ]
})
export class AppComponent {
  title = 'Frontend';
  categorias = signal<Categoria[]>([]);

  constructor(private http: HttpClient) {}

  buscarCategorias() {
    // Limpa a lista para garantir que a animação de entrada seja acionada novamente
    this.categorias.set([]); 
    
    this.http.get<Categoria[]>('https://localhost:7273/api/categorias')
      .subscribe(data => {
        this.categorias.set(data);
      });
  }
}
