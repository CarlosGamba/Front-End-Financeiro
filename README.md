# 🚀 API - Financeiro

# 🚀 Front-end - Interface do Usuário

Aplicação web com consumo eficiente das rotas da API backend, garantindo uma navegação fluida e integração em tempo real com os dados do sistema.

## 🛠️ Tecnologias Utilizadas

- **Plataforma/Linguagem:** .NET 8 / C# 
- **Arquitetura:** MVC 

## 📌 Funcionalidades Principais

- [x] **Consumo de API:** Integração assíncrona com os endpoints da API backend para operações em tempo real.

## 🔧 Como Executar o front end Localmente

### Pré-requisitos
- [.NET SDK 8.0+](https://dotnet.microsoft.com/) (ou ambiente equivalente)
- Docker.

### Passo a Passo

1. Instalar o SDK 8.0

2. Criar e Rodar o Contêiner do front end 

 2.1 Clone o repositório: https://github.com/CarlosGamba/Front-End-Financeiro
 
 2.2 Criar a imagem docker build -t front-end-financeiro:v1 .

 2.3 Criar o container docker run -d -p 8082:8080 -e ASPNETCORE_ENVIRONMENT=Development --name front-end-container front-end-financeiro:v1 





