# 🚀 API - Financeiro

Esta API foi projetada para atuar como o backend , garantindo a comunicação segura, escalável e padronizada de dados.

## 🛠️ Tecnologias Utilizadas

- **Plataforma/Linguagem:** .NET 8 / C# 
- **Arquitetura:** Clean Architecture 
- **Banco de Dados:** SQL Server / Entity Framework Core
- **Documentação:** Swagger / OpenAPI

## 📌 Funcionalidades Principais

- [x] **Autenticação e Autorização:** Controle de acesso via Tokens.
- [x] **Tratamento Global de Exceções:** Formatação padronizada para erros de API.

## 🔧 Como Executar a API Localmente

### Pré-requisitos
- [.NET SDK 8.0+](https://dotnet.microsoft.com/) (ou ambiente equivalente)
- Banco de Dados SQL SERVER rodando via Docker.

### Passo a Passo

1. Instalar o SDK 8.0

2. Criar e Rodar o Contêiner com SQL SERVER

 2.1 Abra o seu terminal (Prompt de Comando, PowerShell ou Terminal do VS Code) e execute o comando abaixo:

 2.2 docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SuaSenhaForte123!" -e SSQL_PID=Express" -p 1433:1433 --name sql_express_2022 -d mcr.microsoft.com/mssql/server:2022-latest

3. Criar e Rodar o Contêiner da API

 3.1 Clone o repositório: https://github.com/CarlosGamba/api-financeiro
 
 3.2 Criar a imagem docker build -t financeiro-api:v1 .

 3.3 Criar o container docker run -d -p 8081:8080 -e ASPNETCORE_ENVIRONMENT=Development --name controle-financeiro-container financeiro-api:v1





