<<<<<<< HEAD
# Minha API - Desenvolvimento de Sistemas

API REST desenvolvida em C# com ASP.NET Core como atividade prática da disciplina de Desenvolvimento de Sistemas, do III Módulo do Curso Técnico em Desenvolvimento de Sistemas.

O projeto tem como objetivo aplicar, na prática, os conceitos de desenvolvimento de API's REST, utilizando métodos HTTP para realizar operações sobre os dados da aplicação.

## Status do Projeto:

🚧 Em Desenvolvimento

A API está sendo construída progressivamente durante as aulas da disciplina.

## 📚 Sobre o Projeto

Durante o desenvolvimento da atividade, estão sendo trabalhados conceitos como:
- Criação de uma API REST
- Organização do projeto em diferentes camadas
- Criação de Models
- Criação de Controllers
- Utilização de métodos HTTP
- Comunicação entre as diferentes partes da aplicação
- Testes das requisições da API

## 🛠 Tecnologias Utilizadas

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Git](https://img.shields.io/badge/Git-F05032?style=for-the-badge&logo=git&logoColor=white)
![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)
![Visual Studio Code](https://img.shields.io/badge/Visual%20Studio%20Code-007ACC?style=for-the-badge&logo=visual-studio-code&logoColor=white)

### 📁 Estrutura do Projeto

Atualmente, o projeto está organizado da seguinte forma:
```
MinhaApi/
├── Controllers/
│   ├── ProdutoController.cs
│   └── TipoController.cs
│
├── Models/
│   ├── Produto.cs
│   └── Tipo.cs
│
├── Repositories/
│   ├── IProdutoRepository.cs
│   └── ProdutoRepository.cs
│
├── Services/
│   ├── IProdutoService.cs
│   └── ProdutoService.cs
│
├── Program.cs
├── MinhaApi.csproj
└── appsettings.json
```
### Controllers

Os Controllers são responsáveis por receber as requisições HTTP e definir quais ações devem ser executadas.

Atualmente, o projeto possui:
- ProdutoController
- TipoController

### Models

Os Models representam os dados utilizados pela aplicação.

O Model Produto possui atualmente:

- Id
- Nome
- Preco
- Estoque
- Ativo
- Tipo

O Model Tipo possui atualmente:

- Id
- Nome

## ▶️ Como executar o projeto
### Pré-requisitos

Para executar o projeto, é necessário ter instalado:

.NET SDK;
Git;
Uma IDE ou editor de código compatível com C#, como Visual Studio ou Visual Studio Code.

### 📖 Swagger

O projeto utiliza Swagger para facilitar a visualização e o teste dos endpoints da API durante o desenvolvimento.

Com a aplicação em execução, o Swagger pode ser acessado pelo endereço disponibilizado pelo projeto.

### 🔄 Desenvolvimento com Git e GitHub

O código-fonte do projeto é versionado utilizando Git e armazenado no GitHub.

Durante o desenvolvimento são utilizados, entre outros, os seguintes comandos:

git status
git add .
git commit -m "mensagem do commit"
git push
git pull

O GitHub é utilizado como repositório remoto para armazenar e sincronizar as versões do projeto.

### 🚧 Próximos passos

O projeto continuará sendo desenvolvido conforme o conteúdo apresentado nas aulas da disciplina.

Novas funcionalidades e melhorias serão adicionadas ao README conforme forem implementadas no projeto.

### 🎓 Contexto acadêmico

Curso: Técnico em Desenvolvimento de Sistemas
Módulo: III Módulo
Disciplina: Desenvolvimento de Sistemas
Instituição: SENAI

👨‍💻 Autor

Thiago Silva

Projeto desenvolvido para fins acadêmicos.
=======
# III Módulo do Curso Técnico em Desenvolvimento de Sistemas
Disciplina Desenvolvimento de Sistemas
>>>>>>> 8734f2800096dd820fb80d63284ea20c84bb6caf
