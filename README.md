# CatalogoAPI

API de catálogo de produtos desenvolvida com ASP.NET Core, Entity Framework e MySQL.

## 📖 Sobre o Projeto

`CatalogoAPI` é uma API RESTful criada para gerenciar um catálogo de produtos e suas respectivas categorias. Ela oferece endpoints para realizar operações CRUD (Criar, Ler, Atualizar e Deletar) tanto para produtos quanto para categorias, além de funcionalidades como logging customizado e tratamento de exceções.

Este projeto foi desenvolvido como um exemplo prático de construção de APIs com a stack .NET, demonstrando o uso de boas práticas e ferramentas modernas.

## ✨ Funcionalidades

- **Gestão de Produtos**: CRUD completo para produtos.
- **Gestão de Categorias**: CRUD completo para categorias.
- **Relacionamento**: Cada produto está associado a uma categoria.
- **Documentação de API**: Interface do Swagger e Scalar para testar os endpoints.
- **Logging**: Filtro de log para registrar informações sobre as requisições.
- **Tratamento de Erros**: Middleware para captura e tratamento global de exceções.

## 🛠️ Tecnologias Utilizadas

- **[.NET 9](https://dotnet.microsoft.com/pt-br/download/dotnet/9.0)**
- **[ASP.NET Core](https://learn.microsoft.com/pt-br/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-9.0)**
- **[Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/)**
- **[MySQL](https://www.mysql.com/)**
- **[Swagger/OpenAPI](https://swagger.io/)**

## 🚀 Como Executar o Projeto

Siga os passos abaixo para configurar e executar o projeto em sua máquina.

### Pré-requisitos

- **[.NET 9 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/9.0)**
- **[MySQL Server](https://dev.mysql.com/downloads/mysql/)** instalado e em execução.
- Um editor de código de sua preferência (ex: **[Visual Studio Code](https://code.visualstudio.com/)** ou **[Visual Studio](https://visualstudio.microsoft.com/pt-br/)**)

### 1. Clone o Repositório

```bash
git clone https://github.com/FM0Ura/CatalogoAPI.git
cd CatalogoAPI
```

### 2. Configure a Conexão com o Banco de Dados

A API precisa da senha do seu banco de dados MySQL para se conectar. A forma recomendada para configurar isso em ambiente de desenvolvimento é usando "User Secrets" (segredos do usuário).

1.  Navegue até o diretório do projeto `CatalogoAPI`:

    ```bash
    cd CatalogoAPI
    ```

2.  Inicialize o gerenciador de segredos:

    ```bash
    dotnet user-secrets init
    ```

3.  Adicione a senha do seu usuário root do MySQL ao `secrets.json`:

    ```bash
    dotnet user-secrets set "MYSQL_root_PASS" "sua_senha_secreta"
    ```

    > **Atenção**: Substitua `sua_senha_secreta` pela senha do seu banco de dados MySQL.

### 3. Aplique as Migrations

Instale o Entity Framework CLI globalmente, caso ainda não tenha feito:
```bash
dotnet tool install --global dotnet-ef
```

As migrations do Entity Framework criarão o banco de dados `catalogoDB` e as tabelas necessárias.

```bash
dotnet ef database update
```

### 4. Execute a Aplicação

Agora, basta executar a API:

```bash
dotnet run
```

A aplicação estará disponível em `https://localhost:7273` (ou outra porta, verifique o console).

## 📡 Endpoints da API

Você pode acessar a documentação interativa do Swagger em `https://localhost:7273/swagger` ou a documentação do Scalar em `https://localhost:7273/scalar` para testar os endpoints.

### Categorias

-   `GET /Categorias`: Retorna todas as categorias.
-   `GET /Categorias/{id}`: Retorna uma categoria específica pelo seu ID.
-   `GET /Categorias/produtos`: Retorna todas as categorias com seus respectivos produtos.
-   `POST /Categorias`: Cria uma nova categoria.
-   `PUT /Categorias/{id}`: Atualiza uma categoria existente.
-   `DELETE /Categorias/{id}`: Deleta uma categoria.

### Produtos

-   `GET /Produtos`: Retorna todos os produtos.
-   `GET /Produtos/{id}`: Retorna um produto específico pelo seu ID.
-   `POST /Produtos`: Cria um novo produto.
-   `PUT /Produtos/{id}`: Atualiza um produto existente.
-   `DELETE /Produtos/{id}`: Deleta um produto.

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE.txt](LICENSE.txt) para mais detalhes.
