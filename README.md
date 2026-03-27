# catalog-retail

Monorepo do sistema **Catalog Retail** — catálogo de produtos para varejo.

## Estrutura do projeto

```
catalog-retail/
├── application-service/   # Back-end: C# .NET 10 + PostgreSQL
└── application/           # Front-end: Node.js 24 + Vite + TypeScript + React
```

---

## application-service (Back-end)

API REST construída em **C# .NET 10** com banco de dados **PostgreSQL** e Entity Framework Core.

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL 16+](https://www.postgresql.org/download/)
- [EF Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet): `dotnet tool install --global dotnet-ef`

### Configuração

1. Copie e edite a connection string em `src/CatalogRetail.Api/appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=catalog_retail_dev;Username=postgres;Password=postgres"
     }
   }
   ```

2. Crie o banco de dados e execute as migrations:
   ```bash
   cd application-service
   dotnet ef migrations add InitialCreate --project src/CatalogRetail.Infrastructure --startup-project src/CatalogRetail.Api
   dotnet ef database update --project src/CatalogRetail.Infrastructure --startup-project src/CatalogRetail.Api
   ```

### Comandos

| Comando | Descrição |
|---------|-----------|
| `dotnet restore` | Restaura os pacotes NuGet |
| `dotnet build` | Compila a solução |
| `dotnet run --project src/CatalogRetail.Api` | Inicia a API em desenvolvimento |
| `dotnet watch --project src/CatalogRetail.Api` | Inicia a API com hot reload |
| `dotnet test` | Executa os testes |

Todos os comandos devem ser executados dentro da pasta `application-service/`.

A API ficará disponível em `http://localhost:5000`.  
A documentação OpenAPI estará em `http://localhost:5000/openapi/v1.json` (apenas em desenvolvimento).

### Endpoints disponíveis

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/products` | Lista todos os produtos |
| GET | `/api/products/active` | Lista produtos ativos |
| GET | `/api/products/{id}` | Busca produto por ID |
| GET | `/api/products/category/{categoryId}` | Lista produtos por categoria |
| POST | `/api/products` | Cria produto |
| PUT | `/api/products/{id}` | Atualiza produto |
| DELETE | `/api/products/{id}` | Remove produto |
| GET | `/api/categories` | Lista categorias |
| GET | `/api/categories/{id}` | Busca categoria por ID |
| POST | `/api/categories` | Cria categoria |
| PUT | `/api/categories/{id}` | Atualiza categoria |
| DELETE | `/api/categories/{id}` | Remove categoria |

### Migrations

```bash
# Criar nova migration
dotnet ef migrations add <NomeDaMigration> \
  --project src/CatalogRetail.Infrastructure \
  --startup-project src/CatalogRetail.Api

# Aplicar migrations pendentes
dotnet ef database update \
  --project src/CatalogRetail.Infrastructure \
  --startup-project src/CatalogRetail.Api

# Reverter última migration
dotnet ef database update <MigrationAnterior> \
  --project src/CatalogRetail.Infrastructure \
  --startup-project src/CatalogRetail.Api

# Remover última migration (não aplicada)
dotnet ef migrations remove \
  --project src/CatalogRetail.Infrastructure \
  --startup-project src/CatalogRetail.Api
```

### Arquitetura

```
application-service/
├── src/
│   ├── CatalogRetail.Api/            # Controllers, Program.cs, configuração
│   ├── CatalogRetail.Application/    # Serviços, DTOs, interfaces de aplicação
│   ├── CatalogRetail.Domain/         # Entidades, interfaces de domínio
│   └── CatalogRetail.Infrastructure/ # DbContext, repositórios, EF Core
└── CatalogRetail.sln
```

---

## application (Front-end)

Interface web construída em **React 19 + TypeScript** com **Vite** e **Node.js 24**.

### Pré-requisitos

- [Node.js 24+](https://nodejs.org/)
- npm 10+

### Configuração

1. Crie o arquivo `.env.local` baseado no exemplo:
   ```bash
   cp .env.example .env.local
   ```
2. Edite `.env.local` com a URL da API:
   ```env
   VITE_API_URL=http://localhost:5000/api
   ```

### Comandos

Todos os comandos devem ser executados dentro da pasta `application/`.

| Comando | Descrição |
|---------|-----------|
| `npm install` | Instala as dependências |
| `npm run dev` | Inicia o servidor de desenvolvimento (porta 5173) |
| `npm run build` | Gera o build de produção em `dist/` |
| `npm run preview` | Serve o build de produção localmente |
| `npm run lint` | Executa o ESLint |

A aplicação ficará disponível em `http://localhost:5173`.

### Estrutura

```
application/
├── public/           # Arquivos estáticos
├── src/
│   ├── assets/       # Imagens, ícones
│   ├── components/   # Componentes reutilizáveis
│   ├── pages/        # Páginas da aplicação
│   ├── services/     # Clientes HTTP (api.ts, productService.ts, categoryService.ts)
│   ├── types/        # Tipos e interfaces TypeScript
│   ├── App.tsx       # Componente raiz
│   └── main.tsx      # Entry point
├── .env.example      # Exemplo de variáveis de ambiente
└── vite.config.ts    # Configuração do Vite
```

---

## Rodando o projeto completo

1. Inicie o PostgreSQL e configure a connection string no back-end.
2. Execute as migrations do banco de dados.
3. Inicie o back-end:
   ```bash
   cd application-service
   dotnet run --project src/CatalogRetail.Api
   ```
4. Em outro terminal, inicie o front-end:
   ```bash
   cd application
   npm install
   npm run dev
   ```
5. Acesse `http://localhost:5173` no navegador.
