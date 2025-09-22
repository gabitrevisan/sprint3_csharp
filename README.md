# 📚 Projeto CarteiraCerta by NexTech

Bem-vindo ao repositório da API **CarteiraCerta**, o backend desenvolvido pela equipe **NexTech** para transformar a curiosidade em conhecimento e abrir caminhos para novos investidores.

---

Por Eduardo Araujo (RM99758), Gabriela Trevisan (RM99500), Leonardo Bonini (RM551716) e Rafael Franck (RM550875)

---

## 🎯 O que é o Projeto CarteiraCerta?

O **CarteiraCerta** é uma **API RESTful** para uma aplicação de investimentos. A ideia, desenvolvida para o "Challenge FIAP/XP", é criar uma plataforma que guia investidores (principalmente os iniciantes), oferecendo recomendações personalizadas e facilitando o gerenciamento de seus ativos financeiros.

Esta API é a responsável por toda a lógica de negócio, manipulação de dados e comunicação com o banco de dados Oracle.

---

## ⚙️ O que o Projeto Faz? (Funcionalidades)

Atualmente, a API CarteiraCerta possui as seguintes funcionalidades implementadas:

1.  **Gerenciamento Completo de Ativos (CRUD):**
    * Permite criar, ler, atualizar e deletar ativos financeiros (como ações, fundos, etc.) no banco de dados. É a base para que o usuário possa construir e gerenciar sua carteira.

2.  **Exportação de Dados da Carteira (Manipulação de Arquivos):**
    * Oferece um endpoint que permite ao usuário exportar os detalhes completos de sua carteira de investimentos para um arquivo no formato `.json`, facilitando a portabilidade e análise dos dados.

---

## 💡 Como Funciona? (O Fluxo de Dados)

Quando você interage com a interface de testes (Swagger), acontece o seguinte processo:

1.  **Interface (Swagger UI):** Você preenche os dados de um novo ativo e clica em "Execute". O Swagger atua como um mensageiro, empacotando esses dados e enviando-os para o endereço correto da nossa API.

2.  **API Controller (A Cozinha):** A requisição chega ao `Controller` correspondente (ex: `AtivosController`). O Controller é o cérebro da operação: ele recebe os dados, entende o que precisa ser feito (neste caso, criar um novo ativo) e aciona a próxima camada.

3.  **DbContext (O Tradutor):** O Controller utiliza o `ApplicationDbContext` (a ponte com o banco de dados) para executar a ação. O `DbContext` traduz o comando da linguagem C# para um comando em linguagem SQL, que o banco de dados Oracle consegue entender.

4.  **Banco de Dados (O Cofre):** O Oracle Database recebe o comando SQL (ex: `INSERT INTO...`) e armazena permanentemente os dados na tabela correspondente (ex: `CarteiraCerta_Ativos`). O processo é o mesmo, mas no sentido inverso, para operações de leitura (GET).

---

## 🛠️ Arquitetura e Tecnologias

O projeto foi construído utilizando uma arquitetura em 3 camadas para garantir a separação de responsabilidades, escalabilidade e manutenção.

-   `CarteiraCerta.Model`: A camada que contém as classes que representam nossos dados (`Usuario`, `Ativo`, `Carteira`).
-   `CarteiraCerta.Data`: A camada de acesso a dados. É responsável por toda a comunicação com o banco de dados Oracle, utilizando o Entity Framework Core.
-   `CarteiraCerta.Api`: é a camada que expõe os endpoints RESTful que um cliente (como um site ou app mobile) irá consumir.

**Tecnologias Utilizadas:**
- .NET 8
- ASP.NET Core
- Entity Framework Core 8
- Oracle Database
- Swagger/OpenAPI

---

## 🚀 Como Executar e Testar o Projeto

Siga os passos abaixo para rodar e testar a API em sua máquina local.

### Pré-requisitos
* .NET 8 SDK instalado.
* Acesso a um banco de dados Oracle.

### 1. Preparação do Ambiente
1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/gabitrevisan/sprint3_csharp
    cd CarteiraCerta
    ```
2.  **Ajuste a Connection String:**
    * Abra o arquivo `CarteiraCerta.Api/appsettings.json`.
    * Modifique a `DefaultConnection` com suas credenciais do banco de dados Oracle.

3.  **Crie as Tabelas no Banco de Dados:**
    * Abra o terminal na pasta `CarteiraCerta/CarteiraCerta.Api`.
    * Execute o comando abaixo para que o Entity Framework crie as tabelas para você:
    ```bash
    dotnet ef database update --project ../CarteiraCerta.Data
    ```

### 2. Executando a API
1.  Ainda no terminal, na pasta `CarteiraCerta/CarteiraCerta.Api`, execute o comando:
    ```bash
    dotnet run
    ```
2.  O terminal indicará que a aplicação está rodando e mostrará a URL (ex.:`http://localhost:5011`).

### 3. Testando com o Swagger
1.  Abra seu navegador e acesse a URL da aplicação seguida de `/swagger` (ex.: **`http://localhost:5011/swagger`**)

2.  Você verá a interface do Swagger com todos os endpoints disponíveis.
3.  **Para testar a criação de um ativo:**
    * Clique no endpoint `POST /api/Ativos` para expandir.
    * Clique em **"Try it out"**.
    * Edite o `Request body` com os dados do ativo que deseja criar.
    * Clique em **"Execute"**.

Um código de resposta `201 Created` indica sucesso! Você pode usar os outros endpoints para listar, atualizar e deletar os dados que acabou de criar.

---

## 🔐 Segurança e CI/CD

Este projeto integra práticas de segurança diretamente no pipeline de desenvolvimento (CI/CD) utilizando o GitHub Actions, com o objetivo de identificar e mitigar vulnerabilidades de forma automatizada.

O pipeline de segurança é composto por três etapas principais:
1. **SAST (Static Application Security Testing):**
   - Ferramenta: GitHub CodeQL.
   - Gatilho: Executado a cada push ou pull request para a branch main.
   - Função: Analisa o código-fonte em busca de vulnerabilidades, como as falhas de controle de acesso encontradas no AtivosController.cs. Os resultados são exibidos na aba "Security" > "Code scanning".

2. **DAST (Dynamic Application Security Testing):**
   - Ferramenta: OWASP ZAP.
   - Gatilho: Executado logo após a conclusão bem-sucedida da análise SAST.
   - Função: Inicia a API em um ambiente de teste e realiza uma varredura dinâmica, "atacando" os endpoints para encontrar vulnerabilidades em tempo de execução. Os resultados são reportados automaticamente como "Issues" no repositório.

3. **SCA (Software Composition Analysis):**
   - Ferramenta: GitHub Dependabot.
   - Gatilho: Monitora o repositório continuamente.
   - Função: Verifica todas as dependências (pacotes NuGet) do projeto em busca de vulnerabilidades conhecidas (CVEs) e alerta sobre quaisquer riscos encontrados na aba "Security" > "Dependabot alerts".

Este pipeline unificado garante que a segurança seja uma parte contínua do ciclo de vida do desenvolvimento.

---

---

## 📝 Diagrama
![Diagrama de Arquitetura do Projeto](https://github.com/gabitrevisan/sprint3_csharp/raw/main/diagrama_sprint_csharp.png)
