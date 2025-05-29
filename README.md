# 🎓 Cursos Universitários API

API REST desenvolvida em .NET para o gerenciamento de cursos universitários, professores e disciplinas.

O sistema permite a criação, edição e relacionamento entre entidades acadêmicas, incluindo autenticação via Identity API e documentação automática com Swagger.

## 🚀 Funcionalidades

- ✅ CRUD completo para:
  - Cursos (`/Course`)
  - Disciplinas (`/Subject`)
  - Professores (`/Professor`)
- 🔁 Relacionamento N:N entre Professores e Cursos
- 🔐 Autenticação com Identity API
- 🧾 Documentação Swagger integrada
- ☁️ Pronto para publicação na Azure

## 🧱 Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/en-us/download)
- ASP.NET Core Minimal API
- Entity Framework Core (SQL Server)
- Identity API Endpoints
- Swagger (Swashbuckle)
- Azure App Service (opcional)

## 🔐 Autenticação

Rotas protegidas utilizam Identity API integrada:

- `POST /auth/login`
- `POST /auth/register`
- `POST /auth/logout`

> O uso de JWT é necessário para acessar rotas protegidas.

## 🌐 Publicação na Azure

O projeto pode ser publicado diretamente no Azure App Service. 

## ✍️ Autor

**Marcio Taier**  
[GitHub - @MTaier](https://github.com/MTaier)
