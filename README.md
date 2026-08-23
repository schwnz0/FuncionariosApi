# 🏢 Funcionários API

API REST desenvolvida com **ASP.NET Core 8** para o gerenciamento de funcionários. O projeto foi desenvolvido seguindo os princípios de **Clean Architecture**, separação de responsabilidades e boas práticas de desenvolvimento de APIs REST.

O projeto conta com **CRUD completo**, persistência com Entity Framework Core, documentação interativa através do Swagger, autenticação e autorização utilizando JWT e testes unitários.

---

## 🛠️ Tecnologias Utilizadas

* **.NET 8 / C#**
* **ASP.NET Core Web API**
* **Entity Framework Core 8**
* **SQL Server**
* **EF Core InMemory** para testes
* **JWT (JSON Web Token)** para autenticação e autorização
* **xUnit** para testes unitários
* **Swagger / OpenAPI** para documentação da API
* **Dependency Injection**
* **Repository Pattern**
* **Clean Architecture**

---

## 📐 Estrutura do Projeto

A solução é composta por 5 projetos, organizados de acordo com a separação de responsabilidades da Clean Architecture:

```text
FuncionariosApi/
│
├── 01-Presentation/
│   ├── Controllers/
│   │   ├── FuncionariosController.cs
│   │   └── AuthController.cs
│   └── Program.cs
│
├── 02-Application/
│   ├── DTOs/
│   ├── Interfaces/
│   └── Services/
│
├── 03-Infrastructure/
│   ├── Data/
│   ├── Repositories/
│   └── Migrations/
│
├── 04-Domain/
│   ├── Entities/
│   └── Interfaces/
│
└── 05-Tests/
    └── Testes unitários
```

### Responsabilidade das camadas

| Projeto               | Responsabilidade                                                 |
| --------------------- | ---------------------------------------------------------------- |
| **01-Presentation**   | API REST, Controllers, autenticação, autorização e Swagger       |
| **02-Application**    | DTOs, interfaces e serviços da aplicação                         |
| **03-Infrastructure** | Entity Framework Core, banco de dados, migrations e repositories |
| **04-Domain**         | Entidades e contratos centrais da aplicação                      |
| **05-Tests**          | Testes unitários utilizando xUnit e EF Core InMemory             |

---

## 👨‍💼 Funcionalidades

A API possui operações completas de gerenciamento de funcionários:

| Método   | Endpoint                 | Descrição                   | Autenticação |
| -------- | ------------------------ | --------------------------- | ------------ |
| `GET`    | `/api/funcionarios`      | Lista todos os funcionários | 🟢 Pública   |
| `GET`    | `/api/funcionarios/{id}` | Busca um funcionário por ID | 🟢 Pública   |
| `POST`   | `/api/funcionarios`      | Cadastra um funcionário     | 🔒 JWT       |
| `PUT`    | `/api/funcionarios/{id}` | Atualiza um funcionário     | 🔒 JWT       |
| `DELETE` | `/api/funcionarios/{id}` | Remove um funcionário       | 🔒 JWT       |
| `POST`   | `/api/auth/login`        | Realiza autenticação        | 🟢 Pública   |

---

## 👤 Entidade Funcionário

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

A entidade também utiliza validações para garantir a consistência dos dados recebidos pela API.

---

## 📦 DTOs

A aplicação utiliza **Data Transfer Objects (DTOs)** para separar os modelos utilizados nas requisições e respostas.

### FuncionarioInputDto

Utilizado para entrada de dados:

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

# 🔐 Autenticação e Segurança

A API utiliza **JWT (JSON Web Token)** para autenticação e autorização.

Os endpoints de consulta permanecem públicos, enquanto as operações de criação, alteração e exclusão exigem um token válido.

### Endpoints públicos

```text
GET /api/funcionarios
GET /api/funcionarios/{id}
POST /api/auth/login
```

### Endpoints protegidos

```text
POST   /api/funcionarios
PUT    /api/funcionarios/{id}
DELETE /api/funcionarios/{id}
```

---

## 🔑 Login

Para obter um token JWT, envie uma requisição para:

```http
POST /api/auth/login
```

Com o seguinte corpo:

```json
{
  "usuario": "admin",
  "senha": "123456"
}
```

Com as credenciais corretas, a API retorna um token JWT.

> **Observação:** as credenciais acima são apenas um exemplo para demonstração. Utilize as credenciais configuradas no ambiente da aplicação.

O token deve ser enviado nas requisições protegidas através do cabeçalho:

```http
Authorization: Bearer SEU_TOKEN
```

### Utilizando o Swagger

1. Execute a API.
2. Acesse o Swagger.
3. Execute `POST /api/auth/login`.
4. Copie o token retornado.
5. Clique em **Authorize**.
6. Informe:

```text
Bearer SEU_TOKEN
```

7. Execute os endpoints protegidos.

---

# 🔒 Configuração da chave JWT

A chave secreta utilizada para assinar os tokens JWT **não é armazenada no `appsettings.json` nem versionada no GitHub**.

Durante o desenvolvimento, o projeto utiliza **.NET User Secrets** para armazenar a chave localmente.

### Configurar User Secrets

Entre no diretório do projeto Presentation:

```bash
cd 01-Presentation
```

Caso o User Secrets ainda não esteja inicializado:

```bash
dotnet user-secrets init
```

Configure a chave JWT:

```bash
dotnet user-secrets set "JwtSettings:SecretKey" "SUA-CHAVE-SECRETA"
```

Para verificar as configurações cadastradas:

```bash
dotnet user-secrets list
```

A configuração esperada é:

```text
JwtSettings:SecretKey = SUA-CHAVE-SECRETA
```

A chave real é armazenada localmente pelo .NET User Secrets e não deve ser adicionada ao repositório.

> ⚠️ **Nunca publique uma chave JWT real no GitHub.**

---

# 🗄️ Banco de Dados

A aplicação utiliza **Entity Framework Core** para acesso ao banco de dados e **SQL Server** como banco principal.

A connection string é configurada através do arquivo:

```text
01-Presentation/appsettings.json
```

Exemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=FuncionariosDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> A connection string deve ser ajustada de acordo com a configuração do SQL Server utilizada no ambiente local.

As configurações não sensíveis da aplicação permanecem no `appsettings.json`, enquanto informações sensíveis, como a chave JWT, são armazenadas separadamente através do **.NET User Secrets**.

---

# 🚀 Como Executar o Projeto

## Pré-requisitos

Antes de executar o projeto, certifique-se de possuir:

* [.NET 8 SDK](https://dotnet.microsoft.com/download)
* SQL Server ou SQL Server LocalDB
* Visual Studio 2022 ou Visual Studio Code

---

## 1. Clonar o repositório

```bash
git clone https://github.com/schwnz0/FuncionariosApi.git
cd FuncionariosApi
```

---

## 2. Configurar o banco de dados

Abra:

```text
01-Presentation/appsettings.json
```

e configure a propriedade:

```json
"ConnectionStrings": {
  "DefaultConnection": "SUA_CONNECTION_STRING"
}
```

---

## 3. Configurar a chave JWT

Entre no projeto Presentation:

```bash
cd 01-Presentation
```

Configure o User Secret:

```bash
dotnet user-secrets set "JwtSettings:SecretKey" "SUA-CHAVE-SECRETA"
```

Verifique se a chave foi cadastrada:

```bash
dotnet user-secrets list
```

Depois volte para a raiz do projeto:

```bash
cd ..
```

---

## 4. Aplicar as migrations

Execute:

```bash
dotnet ef database update --project 03-Infrastructure --startup-project 01-Presentation
```

Esse comando aplica as migrations existentes e cria ou atualiza o banco de dados.

---

## 5. Executar a API

```bash
dotnet run --project 01-Presentation
```

Após iniciar a aplicação, acesse a URL apresentada no terminal.

O Swagger estará disponível em:

```text
https://localhost:XXXX/swagger
```

---

# 📖 Swagger / OpenAPI

O projeto possui documentação interativa através do **Swagger / OpenAPI**.

A documentação apresenta:

* Endpoints disponíveis;
* Parâmetros;
* Modelos de requisição;
* Modelos de resposta;
* Códigos HTTP;
* Documentação XML;
* Autenticação Bearer JWT.

O título da documentação é:

> **Funcionários API**

---

# 📡 Exemplos de Requisições

## Criar funcionário

```http
POST /api/funcionarios
Authorization: Bearer SEU_TOKEN
Content-Type: application/json
```

```json
{
  "nome": "João da Silva",
  "cargo": "Desenvolvedor",
  "salario": 4500,
  "departamento": "Tecnologia"
}
```

Resposta:

```http
201 Created
```

---

## Listar funcionários

```http
GET /api/funcionarios
```

Exemplo de resposta:

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

---

## Buscar funcionário por ID

```http
GET /api/funcionarios/1
```

---

## Atualizar funcionário

```http
PUT /api/funcionarios/1
Authorization: Bearer SEU_TOKEN
Content-Type: application/json
```

```json
{
  "nome": "João da Silva",
  "cargo": "Desenvolvedor Full-Stack",
  "salario": 5500,
  "departamento": "Tecnologia"
}
```

Resposta:

```http
204 No Content
```

---

## Excluir funcionário

```http
DELETE /api/funcionarios/1
Authorization: Bearer SEU_TOKEN
```

Resposta:

```http
204 No Content
```

---

# 📋 Códigos HTTP

A API utiliza códigos HTTP para representar o resultado das operações:

| Código | Significado                                 |
| -----: | ------------------------------------------- |
|  `200` | Requisição realizada com sucesso            |
|  `201` | Recurso criado com sucesso                  |
|  `204` | Operação realizada sem conteúdo de resposta |
|  `400` | Requisição inválida                         |
|  `401` | Não autenticado / token ausente ou inválido |
|  `404` | Funcionário não encontrado                  |

Os endpoints também utilizam `ProducesResponseType` para documentar os possíveis códigos de resposta no Swagger.

---

# 🧪 Testes Unitários

O projeto possui um projeto dedicado aos testes:

```text
05-Tests
```

Os testes utilizam:

* **xUnit**
* **Entity Framework Core InMemory**

O banco InMemory permite executar os testes sem depender de uma instância real do SQL Server.

Para executar todos os testes:

```bash
dotnet test
```

### Testes implementados

* `GetAllAsync_DeveRetornarFuncionariosCadastrados`

  * Valida a listagem de funcionários cadastrados.

* `GetByIdAsync_IdInexistente_DeveLancarKeyNotFoundException`

  * Valida o tratamento de um funcionário inexistente.

* `CreateAsync_DeveSalvarERetornarFuncionario`

  * Valida a criação e persistência de um novo funcionário.

---

# 🧱 Padrões e Conceitos Aplicados

O projeto aplica diversos conceitos importantes de desenvolvimento de software:

* **Clean Architecture**
* **Repository Pattern**
* **Service Layer**
* **DTO Pattern**
* **Dependency Injection**
* **Inversion of Control**
* **Entity Framework Core**
* **RESTful API**
* **JWT Authentication**
* **Unit Testing**
* **OpenAPI / Swagger**
* **HTTP Status Codes**
* **XML Documentation**

---

# 🎯 Objetivo Acadêmico

Este projeto foi desenvolvido como parte de uma avaliação prática de **REST APIs com ASP.NET Core**, tendo como objetivo aplicar conceitos de desenvolvimento de APIs, arquitetura de software, persistência de dados, autenticação, testes e documentação.

A implementação contempla:

* Estrutura em Clean Architecture;
* CRUD completo de funcionários;
* Entity Framework Core;
* SQL Server;
* Migrations;
* DTOs;
* Repository e Service;
* Dependency Injection;
* Swagger / OpenAPI;
* Documentação XML;
* JWT;
* Testes unitários.

---

# 👨‍💻 Autor

**Everson Oliveira**

🔗 GitHub: https://github.com/schwnz0

🔗 LinkedIn: https://www.linkedin.com/in/everson-oliveira-dev11/

---

## 📄 Licença

Projeto desenvolvido para fins acadêmicos.
