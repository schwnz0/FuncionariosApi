# FuncionariosApi

API REST para gerenciamento de funcionários desenvolvida com **ASP.NET Core 8** e organizada utilizando os princípios da **Clean Architecture**.

O projeto foi desenvolvido como parte da avaliação prática do **Módulo 10 — REST APIs com ASP.NET Core**, utilizando Entity Framework Core, SQL Server, Swagger e separação de responsabilidades entre as camadas da aplicação.

## 🏗️ Arquitetura

O projeto é dividido em cinco projetos, seguindo a estrutura de Clean Architecture exigida na atividade:

```text
FuncionariosApi
│
├── 01-Presentation
│   ├── Controllers
│   └── Program.cs
│
├── 02-Application
│   ├── DTOs
│   ├── Interfaces
│   └── Services
│
├── 03-Infrastructure
│   ├── Data
│   ├── Repositories
│   └── Migrations
│
├── 04-Domain
│   ├── Entities
│   └── Interfaces
│
├── 05-Tests
│
└── FuncionariosApi.slnx
```

### Dependências entre as camadas

```text
04-Domain
    ↑
02-Application
    ↑
01-Presentation

04-Domain
    ↑
03-Infrastructure

02-Application
    ↑
05-Tests
```

A camada **Domain** contém as regras e contratos centrais da aplicação, enquanto **Application** concentra DTOs e serviços, **Infrastructure** é responsável pelo acesso aos dados e **Presentation** disponibiliza a API REST.

---

## 🚀 Tecnologias utilizadas

* **C#**
* **.NET 8**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **SQL Server**
* **Swagger / OpenAPI**
* **xUnit**
* **Clean Architecture**
* **Dependency Injection**

---

## 👨‍💼 Funcionalidades

A API permite realizar o CRUD completo de funcionários:

| Método   | Endpoint                 | Descrição                    |
| -------- | ------------------------ | ---------------------------- |
| `GET`    | `/api/funcionarios`      | Lista todos os funcionários  |
| `GET`    | `/api/funcionarios/{id}` | Busca um funcionário pelo ID |
| `POST`   | `/api/funcionarios`      | Cadastra um novo funcionário |
| `PUT`    | `/api/funcionarios/{id}` | Atualiza um funcionário      |
| `DELETE` | `/api/funcionarios/{id}` | Remove um funcionário        |

### Funcionário

A entidade `Funcionario` possui as seguintes propriedades:

```text
Id
Nome
Cargo
Salario
Departamento
Ativo
```

O campo `Ativo` possui valor padrão `true`.

A entidade também utiliza Data Annotations para validação dos dados, incluindo campos obrigatórios e validação do salário.

---

## 📦 DTOs

A aplicação utiliza DTOs para separar os dados de entrada e saída da API.

### FuncionarioInputDto

Utilizado para criação e atualização:

```text
Nome
Cargo
Salario
Departamento
```

### FuncionarioOutputDto

Utilizado nas respostas da API:

```text
Id
Nome
Cargo
Salario
Departamento
Ativo
```

---

## 🗄️ Banco de dados

O projeto utiliza **Entity Framework Core** com **SQL Server**.

A conexão com o banco é configurada através da propriedade `DefaultConnection` no arquivo:

```text
01-Presentation/appsettings.json
```

Exemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "SUA_CONNECTION_STRING"
  }
}
```

> Substitua a connection string de acordo com a configuração do SQL Server utilizada no ambiente local.

O banco utilizado pelo projeto é:

```text
FuncionariosDB
```

As migrations do Entity Framework Core estão disponíveis no projeto `03-Infrastructure`.

---

## 🔧 Configuração e execução

### 1. Pré-requisitos

Antes de executar o projeto, certifique-se de possuir:

* .NET 8 SDK
* SQL Server ou SQL Server LocalDB
* Visual Studio 2022 ou Visual Studio Code

### 2. Clone o repositório

```bash
git clone https://github.com/schwnz0/FuncionariosApi.git
```

Entre na pasta:

```bash
cd FuncionariosApi
```

### 3. Configure o banco

Verifique a `DefaultConnection` em:

```text
01-Presentation/appsettings.json
```

Configure-a de acordo com seu ambiente SQL Server.

### 4. Execute as migrations

Na raiz da solução, execute:

```bash
dotnet ef database update --project 03-Infrastructure --startup-project 01-Presentation
```

Caso o comando `dotnet ef` não esteja disponível, instale a ferramenta:

```bash
dotnet tool install --global dotnet-ef
```

### 5. Execute a API

```bash
dotnet run --project 01-Presentation
```

Após iniciar a aplicação, utilize a URL apresentada pelo terminal para acessar a API e o Swagger.

---

## 📖 Swagger

O projeto possui documentação da API através do **Swagger / OpenAPI**.

A interface permite visualizar e testar os endpoints disponíveis, incluindo:

* parâmetros;
* modelos de requisição;
* modelos de resposta;
* códigos HTTP possíveis;
* documentação dos endpoints.

---

## 📡 Exemplos de requisições

### Criar funcionário

`POST /api/funcionarios`

```json
{
  "nome": "João da Silva",
  "cargo": "Desenvolvedor",
  "salario": 4500,
  "departamento": "Tecnologia"
}
```

### Buscar todos

`GET /api/funcionarios`

Resposta:

```json
[
  {
    "id": 1,
    "nome": "João da Silva",
    "cargo": "Desenvolvedor",
    "salario": 4500,
    "departamento": "Tecnologia",
    "ativo": true
  }
]
```

### Atualizar funcionário

`PUT /api/funcionarios/1`

```json
{
  "nome": "João da Silva",
  "cargo": "Desenvolvedor Full-Stack",
  "salario": 5500,
  "departamento": "Tecnologia"
}
```

### Excluir funcionário

`DELETE /api/funcionarios/1`

Retorna:

```text
204 No Content
```

quando o funcionário é removido com sucesso.

---

## 📋 Códigos HTTP

A API utiliza códigos HTTP apropriados para representar o resultado das operações:

| Código | Significado                                 |
| -----: | ------------------------------------------- |
|  `200` | Operação realizada com sucesso              |
|  `201` | Recurso criado com sucesso                  |
|  `204` | Operação realizada sem conteúdo de resposta |
|  `400` | Requisição inválida                         |
|  `404` | Funcionário não encontrado                  |

Os endpoints também possuem `ProducesResponseType` para documentar os possíveis códigos de resposta no Swagger.

---

## 🧪 Testes

O projeto possui um projeto separado para testes:

```text
05-Tests
```

Os testes utilizam **xUnit** e podem ser executados através de:

```bash
dotnet test
```

---

## 📁 Estrutura dos principais componentes

### Domain

Contém as entidades e interfaces que representam o núcleo da aplicação.

```text
04-Domain
├── Entities
│   └── Funcionario.cs
└── Interfaces
    └── IFuncionarioRepository.cs
```

### Application

Contém os DTOs, contratos dos serviços e implementação das regras de aplicação.

```text
02-Application
├── DTOs
│   ├── FuncionarioInputDto.cs
│   └── FuncionarioOutputDto.cs
├── Interfaces
│   └── IFuncionarioService.cs
└── Services
    └── FuncionarioService.cs
```

### Infrastructure

Responsável pela persistência dos dados utilizando Entity Framework Core.

```text
03-Infrastructure
├── Data
│   └── AppDbContext.cs
├── Repositories
│   └── FuncionarioRepository.cs
└── Migrations
```

### Presentation

Responsável pela exposição dos endpoints HTTP.

```text
01-Presentation
├── Controllers
│   └── FuncionariosController.cs
└── Program.cs
```

---

## 🎯 Objetivo do projeto

O projeto tem como objetivo demonstrar a construção de uma **REST API com ASP.NET Core**, aplicando conceitos de:

* Clean Architecture;
* separação de responsabilidades;
* DTOs;
* Repository Pattern;
* Service Layer;
* Entity Framework Core;
* Dependency Injection;
* migrations;
* documentação OpenAPI/Swagger;
* códigos de status HTTP;
* validação de dados.

---

## 👤 Autor

**Everson Oliveira**

GitHub: [@schwnz0](https://github.com/schwnz0)

LinkedIn: [Everson Oliveira](https://www.linkedin.com/in/everson-oliveira-dev11/)

---

## 📄 Licença

Projeto desenvolvido para fins acadêmicos.
