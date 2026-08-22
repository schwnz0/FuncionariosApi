---

```markdown
# 🏢 Funcionários API

API REST desenvolvida em **.NET Core** para o gerenciamento completo de funcionários. O projeto foi construído seguindo os princípios de **Clean Architecture** e boas práticas de desenvolvimento.

---

## 🛠️ Tecnologias Utilizadas

- **.NET 8.0 / 9.0** (C#)
- **ASP.NET Core Web API**
- **Entity Framework Core** (SQL Server e In-Memory Database)
- **JWT (JSON Web Token)** para autenticação e autorização
- **xUnit** para testes unitários
- **Swagger / OpenAPI** para documentação interativa

---

## 📐 Estrutura do Projeto (Clean Architecture)

A solução é composta por 5 projetos organizados de acordo com a separação de responsabilidades:

```text
FuncionariosApi/
│
├── 01-Presentation/        # Web API, Controllers (FuncionariosController, AuthController), Middlewares e Swagger
├── 02-Application/         # DTOs, Interfaces de Serviço e Implementações de Serviços
├── 03-Infrastructure/      # Contexto do Banco de Dados (AppDbContext) e Repositórios
├── 04-Domain/              # Entidades do Domínio e Interfaces do Repositório (Livre de dependências externas)
└── 05-Tests/               # Projeto de Testes Unitários utilizando xUnit e EF Core InMemory

```

---

## 🔐 Autenticação e Segurança (JWT)

A API utiliza autenticação via **Bearer Token JWT**:

* **Endpoints Públicos (`GET`):** Consulta de funcionários liberada sem autenticação.
* **Endpoints Protegidos (`POST`, `PUT`, `DELETE`):** Exigem cabeçalho de autenticação (`Authorization: Bearer <seu_token>`).

### Como Obter o Token:

Faça uma requisição `POST` para a rota `/api/auth/login`:

```json
{
  "usuario": "admin",
  "senha": "123456"
}

```

---

## 🚀 Como Executar o Projeto

### Pré-requisitos

* [.NET SDK](https://dotnet.microsoft.com/download) instalado.
* SQL Server configurado ou ajustado no `appsettings.json`.

### Passo a Passo

1. **Clonar o Repositório:**
```bash
git clone [https://github.com/schwnz0/FuncionariosApi.git](https://github.com/schwnz0/FuncionariosApi.git)
cd FuncionariosApi

```


2. **Configurar a Connection String:**
No arquivo `01-Presentation/appsettings.json`, ajuste a sua `DefaultConnection`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=FuncionariosDb;Trusted_Connection=True;TrustServerCertificate=True;"
}

```


3. **Executar a Migração / Atualizar Banco:**
```bash
dotnet ef database update --project 03-Infrastructure --startup-project 01-Presentation

```


4. **Rodar a API:**
```bash
dotnet run --project 01-Presentation

```


Acesse a documentação no navegador através do Swagger em: `https://localhost:XXXX/swagger`

---

## 🧪 Executando os Testes Unitários

O projeto de testes (`05-Tests`) valida as regras de negócio da camada de aplicação sem depender de um banco de dados real.

Para rodar os testes via terminal:

```bash
dotnet test

```

### Testes Implementados:

* `GetAllAsync_DeveRetornarFuncionariosCadastrados`: Valida a listagem de registros.
* `GetByIdAsync_IdInexistente_DeveLancarKeyNotFoundException`: Garante o tratamento de erro em pesquisas sem resultado.
* `CreateAsync_DeveSalvarERetornarFuncionario`: Garante a persistência e geração de ID ao criar um registro.

```

---

### Como salvar no seu projeto:
1. Vá na raiz da sua solução no Visual Studio (ou pasta principal do projeto).
2. Abra ou crie o arquivo chamado **`README.md`**.
3. Substitua o conteúdo pelo código acima e salve.
4. Faça o **commit** e o **push** para o GitHub:
   ```bash
   git add README.md
   git commit -m "docs: adiciona README detalhado com instrucoes e arquitetura"
   git push

```
