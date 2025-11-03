# 🗓 AgendamentosApi

Aplicativo de gerenciamento de agendamentos para estúdio de beleza, permitindo que clientes agendem serviços e administradores gerenciem todos os agendamentos.

Dividido em backend ASP.NET Core e frontend Angular, com autenticação, CRUD de agendamentos e validação de horários.

## 🚀 Tecnologias

### Backend:

- ASP.NET Core 9;

- Entity Framework Core;

- AutoMapper;

- Identity (usuários e permissões);

- PostgreSQL;

- JWT (autenticação).

## 🏗 Estrutura do Projeto

### Backend (AgendamentosApi)

O backend foi estruturado em **repository pattern**, tendo assim a camada de repository (onde as consultas ao banco de dados são feitas), camada de services (onde ficam todas as regras de negócio da aplicação) e finalmente a controller (endpoints que se comunicam com o frontend).

```
AgendamentosApi/
│
├─ Data             # Contexto do banco de dados e migrações
├─ Models/          # Entidades (Agendamento, Usuario)
├─ DTOs/            # Objetos de transferência (AgendamentoDTO, UsuarioDTO, etc.)
├─ Profiles         # Configurações dos DTOs
├─ Repositories/    # Acesso ao banco (AgendamentoRepository)
├─ Services/        # Regras de negócio (AgendamentoService)
├─ Controllers/     # Endpoints REST
└─ Exceptions/      # Exceções customizadas
```

## ✨ Funcionalidades

### Backend

✅ CRUD de agendamentos

✅ Validação de horário (não permite duplicidade e nem agendamentos com intervalo menor que 1 hora)

✅ Autenticação com JWT

✅ Controle de permissões (admin vs user)

## 🔐 Autenticação e Permissões

- **Usuário cliente:** Pode ver, criar, editar e deletar apenas seus agendamentos;
- **Usuário administrador**: Pode ver, editar e deletar todos os agendamentos.

## 📌 Regras de Negócio

- Agendamentos devem ser futuros;

- Horário duplicado não permitido;

- Admin vê todos os agendamentos, usuário só os seus;

## 🛠 Como Rodar o Projeto

### Backend

Tenha a versão do .NET 9 instalada na sua máquina e dentro do repositório AgendamentosApi rode os seguintes comandos:

```
dotnet ef database update   # Cria banco e aplica migrations
dotnet run                  # Roda backend
```
**Obs:** Estou utilizando o secrets do .NET para guardar a chave de criptografia do token JWT e guardar a string de conexão com o banco de dados.
