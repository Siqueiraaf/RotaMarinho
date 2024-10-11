
# ROTA Marinho

## Visão Geral
O projeto RotaMarinho é uma aplicação ASP.NET Core voltada para o gerenciamento de embarcações. Este documento tem o objetivo de detalhar os passos de configuração, implementação de repositórios, mapeamento do banco de dados com Entity Framework Core, autenticação JWT e demais funcionalidades implementadas até o momento.

A documentação também serve como referência para futuras manutenções, evolução do código e possíveis migrações para novas tecnologias ou versões de frameworks.
## 1. Configuração Inicial
### Instalação de Dependências

**ASP.NET Core**: Para a criação da API.

**Entity Framework Core**: Para o mapeamento objeto-relacional (ORM) com SQL Server.

**Autenticação JWT**: Para autenticação e controle de acesso na aplicação.

No terminal, usamos os seguintes comandos para adicionar as bibliotecas necessárias:

```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Swashbuckle.AspNetCore

```

### Configuração de Conexão com o Banco de Dados
Adicionamos a string de conexão com o SQL Server no arquivo appsettings.json:
```
{
  "ConnectionStrings": {
    "ConnectionPadrao": "Server=localhost;Database=;"
  }
}
```

## 2. Entity Framework Core: DbContext e Migrations
#### 2.1. Criação do AppDbContext
O AppDbContext é responsável por gerenciar a comunicação entre a aplicação e o banco de dados. Ele herda de DbContext e define os mapeamentos das entidades.

#### 2.2. Migrations
O Entity Framework Core permite a criação de migrations para a evolução do esquema de banco de dados de forma incremental. Após definir o DbContext, geramos as migrations com os comandos abaixo:

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```
Gerou a tabela Embarcacoes no banco de dados com base na classe Embarcacao.

#### 2.3. Seed Data
Para garantir que alguns dados iniciais sejam inseridos no banco de dados, adicionamos um método ```OnModelCreating``` com dados de embarcações de exemplo.

## 3. Repositórios
#### 3.1. Criação da Interface IEmbarcacaoRepository
Criamos uma interface para abstrair as operações relacionadas às embarcações.

#### 3.2. Implementação do EmbarcacaoRepository
A implementação do repositório interage diretamente com o AppDbContext.

#### 3.3. Injeção de Dependências
Registramos o IEmbarcacaoRepository no Program.cs para ser injetado em controladores e serviços:

```
builder.Services.AddScoped<IEmbarcacaoRepository, EmbarcacaoRepository>();

```

## 4. Autenticação JWT
Para garantir a segurança da aplicação, implementamos autenticação baseada em JWT. Isso permite que apenas usuários autenticados acessem determinadas rotas da API.

Configuramos a autenticação no ```Program.cs```

## 5. Swagger para Documentação de API
Para facilitar o teste e a visualização da API, integramos o Swagger:

```
builder.Services.AddSwaggerGen();
app.UseSwagger();
app.UseSwaggerUI();
```
Isso disponibiliza a documentação da API na URL ```/swagger```, permitindo interações diretas com os endpoints.

## 6. Melhorias Futuras
#### Algumas melhorias e evoluções sugeridas incluem:

- **Validação de Requisições**: Implementar validação para garantir a integridade dos dados recebidos na API.

- **Cache**: Adicionar caching para melhorar o desempenho em consultas frequentes.

- **Logs**: Integrar um sistema de logs detalhados para monitorar a aplicação em produção.

- **Deploy**: Automatizar o processo de deploy em ambientes de produção com pipelines CI/CD.

- **Testes Unitários e de Integração**: Implementar uma suíte de testes com xUnit e Moq para garantir a qualidade do código.
## Estrutura de Diretórios
```
[RotaMarinho]
|───API
│   └───Controllers
├───Application
│   └───Services
├───Domain
│   ├───Entities
│   └───Repositories
├───DTOs
├───Infrastructure
│   └───Persistence
│       └───Repositories
├───Migrations
├───Models
├───obj
│   └───Debug
│       └───net8.0
│           ├───ref
│           ├───refint
│           └───staticwebassets
└───Properties
```

## 1. API
A pasta API é onde ficam os Controllers da aplicação. Os Controllers são responsáveis por lidar com as requisições HTTP que chegam à API e por direcioná-las para as devidas camadas de serviço ou repositório.

#### API/Controllers
Nesta pasta, estão os Controllers, que atuam como pontos de entrada da API. Cada controller lida com um determinado recurso, como **"EmbarcacaoController"**, que gerencia as operações relacionadas às embarcações. Esses controllers expõem endpoints que seguem o padrão REST.

## 2. Application
A pasta Application contém os Services, que encapsulam a lógica de negócios da aplicação. A camada de serviço se comunica diretamente com os repositórios e contém regras de negócio mais complexas, que não precisam estar no controller.

#### Application/Services
Os Services nesta pasta são responsáveis pela lógica de negócios. Por exemplo, o EmbarcacaoService poderia ser usado para manipular dados das embarcações antes de serem persistidos no banco ou antes de serem enviados de volta para o controller.

## 3. Domain
A pasta Domain contém as Entities e os Repositories.

#### Domain/Entities
As Entities representam os modelos de dados principais do domínio da aplicação. Por exemplo, Embarcacao seria uma entidade que representa uma embarcação e contém as propriedades como Nome, Descricao, etc. Essas entidades geralmente correspondem às tabelas no banco de dados.

#### Domain/Repositories
Os Repositories nesta pasta são interfaces que abstraem o acesso aos dados. Eles definem os contratos para operações de CRUD (Create, Read, Update, Delete). Por exemplo, o IEmbarcacaoRepository define as operações que podem ser realizadas com a entidade Embarcacao.

## 4. DTOs
A pasta **DTOs** _(Data Transfer Objects)_ contém objetos que são usados para transferir dados entre a API e o cliente. Os DTOs servem para simplificar as respostas e pedidos da API, sem expor diretamente as entidades do domínio.

Por exemplo, um EmbarcacaoDTO pode conter apenas os campos necessários para exibição no front-end ou para criação/atualização de uma embarcação, diferente da entidade Embarcacao, que pode ter mais campos ou relacionamentos complexos.

## 5. Infrastructure
A pasta Infrastructure contém a implementação da persistência de dados e outros aspectos que envolvem a infraestrutura do projeto.

### Infrastructure/Persistence
A Persistence contém a implementação do repositório concreto e o DbContext, que gerencia a comunicação com o banco de dados.

### Infrastructure/Persistence/Repositories
Aqui estão os repositórios concretos, como o EmbarcacaoRepository, que implementa os métodos definidos na interface IEmbarcacaoRepository. Eles usam o AppDbContext para interagir com o banco de dados, realizando operações de leitura, escrita, atualização e exclusão de dados.

## 6. Migrations
A pasta **Migrations** contém os arquivos gerados automaticamente pelo Entity Framework Core para controlar a evolução do banco de dados. Cada migração registra as alterações no esquema do banco de dados, como a criação de novas tabelas ou a alteração de colunas existentes.

Por exemplo, ao rodar ```dotnet ef migrations add InitialCreate```, um arquivo de migração é gerado para criar as tabelas necessárias no banco de dados.

## 7. Models
A pasta **Models** contém classes que representam modelos de dados auxiliares que não necessariamente são entidades do domínio ou que podem ser usadas para outras finalidades, como modelos para entrada e saída de dados.

Por exemplo,  vou usar um LoginModel que representa os dados enviados para realizar a autenticação do usuário, contendo propriedades como Username e Password.

## 8. obj
A pasta obj é gerada automaticamente pelo .NET durante o processo de build da aplicação. Ela contém arquivos temporários e intermediários utilizados pelo compilador.

### obj/Debug/net8.0
Esta pasta é específica para a configuração de compilação Debug para a plataforma .NET 8.0. Os arquivos aqui são temporários e ajudam no processo de compilação e execução da aplicação em modo de depuração.

ref: Contém arquivos de referência para a compilação.
refint: Arquivos internos de referência para o build.
staticwebassets: Arquivos estáticos da aplicação (CSS, JavaScript), caso aplicável, usados em conjunto com ASP.NET Core.
## 9. Properties
A pasta Properties contém configurações do projeto, como o arquivo *launchSettings.json*, que define como a aplicação será executada em ambientes de desenvolvimento. Este arquivo pode configurar o comportamento de portas, variáveis de ambiente, perfis de execução e outros detalhes do projeto.