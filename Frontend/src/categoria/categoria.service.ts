import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Categoria } from '../categoria/categoria.model';

// Responsável por buscar os dados da API.
@Injectable({
  providedIn: 'root'
})
export class CategoriaService {
  private readonly apiUrl = 'https://localhost:7018/Categorias';

  constructor(private http: HttpClient) { }

  /**
   * Busca todas as categorias na API.
   * Retorna um Observable, que é um fluxo de dados que a aplicação "escuta".
   */
  public getCategorias(): Observable<Categoria[]> {
    return this.http.get<Categoria[]>(this.apiUrl);
  }
}
