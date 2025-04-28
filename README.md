
# **Educa Online - Plataforma de controle do aluno**

[](https://github.com/jairobionez/gestao-contas-mba#gest%C3%A3o-conta--plataforma-de-controle-financeiro-pessoal)

## **1. Apresentação**

[](https://github.com/jairobionez/gestao-contas-mba#1-apresenta%C3%A7%C3%A3o)

Bem-vindo ao repositório do projeto  **Educa Online**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo  **Arquitetura, Modelagem e Qualidade de Software**. Desenvolver uma aplicação api que permita aos alunos gerenciar suas matricula e pagamentos através de uma API RESTful.

### **Autor**
-   **Jairo Bionez**

## **2. Proposta do Projeto**

[](https://github.com/jairobionez/gestao-contas-mba#2-proposta-do-projeto)

O projeto consiste em:

Desenvolver uma plataforma educacional online com múltiplos bounded contexts (BC), aplicando DDD, TDD, CQRS e padrões arquiteturais para gestão eficiente de conteúdos educacionais, alunos e processos financeiros.

## **3. Tecnologias Utilizadas**

[](https://github.com/jairobionez/gestao-contas-mba#3-tecnologias-utilizadas)

-   **Linguagem de Programação:**  C#
-   **Frameworks:**
    -   ASP.NET Core Web API
    -   Entity Framework Core
-  **Libs principais:**
    -  MediatR
    -  AutoMapper
-   **Banco de Dados:**  SQLite
-   **Autenticação e Autorização:**
    -   ASP.NET Core Identity
    -   JWT (JSON Web Token) para autenticação na API
-   **Documentação da API:**  Swagger

## **4. Estrutura do Projeto**

[](https://github.com/jairobionez/gestao-contas-mba#4-estrutura-do-projeto)

A estrutura do projeto é organizada da seguinte forma:
-   src/
    -   WebApis/EducaOnline.Api/ - API RESTful
    -   Services/ - Contextos delimitados para Aluno/Conteudo/Financeiro
    -   BuildingBlocks/ - Controle de eventos da aplicação
  
-   README.md - Arquivo de Documentação do Projeto
-   FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
-   .gitignore - Arquivo de Ignoração do Git

## **5. Funcionalidades Implementadas**

[](https://github.com/jairobionez/gestao-contas-mba#5-funcionalidades-implementadas)

-   **CRUD para Contas, Orçamentos e Transações:**  Permite criar, editar, visualizar e excluir contas, orçamentos e transações.
-   **Autenticação e Autorização:**  Controle de acesso e autorização baseada em claims.
-   **API RESTful:**  Exposição de endpoints para operações via API.
-   **Documentação da API:**  Documentação automática dos endpoints da API utilizando Swagger.

## **6. Como Executar o Projeto**

### **Pré-requisitos**

-   .NET SDK 8.0 ou superior
-   SQlite
-   Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
-   Git

### **Passos para Execução**

[](https://github.com/jairobionez/gestao-contas-mba#passos-para-execu%C3%A7%C3%A3o)

1.  **Clone o Repositório:**
    
    -   `git clone git@github.com:jairobionez/educa-online-mba.git`
    -   `cd educa-online-mba`
2.  **Configuração do Banco de Dados:**

4.  **Executar a API:**
    -   `cd src/EducaOnline.Api/`
    -   `dotnet run`
    -   Acesse a documentação da API em:  [http://localhost:5050/swagger](http://localhost:5050/swagger)

## **7. Instruções de Configuração**

-   **JWT para API:**  As chaves de configuração do JWT estão no  `appsettings.json`.
-   **Migrações do Banco de Dados:**  As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

[http://localhost:5050/swagger](http://localhost:5050/swagger)

## **9. Avaliação**

-   Este projeto é parte de um curso acadêmico e não aceita contribuições externas.
-   Para feedbacks ou dúvidas utilize o recurso de Issues
-   O arquivo  `FEEDBACK.md`  é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
