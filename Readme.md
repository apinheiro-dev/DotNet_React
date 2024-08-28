Criação WebAPI com .NET 6 e Front-end em React com Autenticação
Desenvolver uma aplicação web que permita gerenciar informações de estudantes, incluindo listagem, adição, atualização e exclusão de registros. A aplicação deve ser composta por uma WebAPI desenvolvida com .NET 6 e um front-end desenvolvido em React, incluindo uma tela de login.

Requisitos
Back-end (WebAPI)
Framework: .NET 6.

Entity Framework: Usar o EF Core com um banco de dados em memória.

Autenticação: Implementar autenticação básica (JWT).

Endpoints:

GET /api/students: Retorna todos os estudantes (autenticado).
GET /api/students/{id}: Retorna um estudante específico (autenticado).
POST /api/students: Cria um novo estudante (autenticado).
PUT /api/students/{id}: Atualiza um estudante existente (autenticado).
DELETE /api/students/{id}: Deleta um estudante (autenticado).
POST /api/auth/login: Autentica um usuário e retorna um token JWT.
Modelo de Dados:

Student
int Id (identificador único)
string Nome (nome do estudante)
int Idade (idade do estudante)
int Serie (série do estudante)
double NotaMedia (nota média do estudante)
string Endereco (endereço do estudante)
string NomePai (nome do pai do estudante)
string NomeMae (nome da mãe do estudante)
DateTime DataNascimento (data de nascimento do estudante)
User
int Id (identificador único)
string Username (nome de usuário)
string Password (senha)
Seed Data:

Popular a base de dados em memória com os dados do CSV fornecido e um usuário padrão.
Front-end (React)
Framework: React.

Componentes:

Login: Tela de login.
StudentList: Exibe a lista de estudantes.
StudentForm: Formulário para criar/atualizar estudantes.
Funcionalidades:

Login de usuário.
Listar todos os estudantes (após login).
Adicionar um novo estudante (após login).
Atualizar um estudante existente (após login).
Excluir um estudante (após login).
UI/UX:

Utilize uma biblioteca de componentes UI (por exemplo, Material-UI ou Bootstrap).
A interface deve ser responsiva e de fácil utilização.
Exiba mensagens de erro e sucesso para as operações de CRUD.



Documentação da API

Rotas disponíveis do Desafio .Net | React
http://localhost:7119/swagger/index.html

Execução
Para rodar os projetos, são necessários executar os comandos abaixo:

Back-end --> StudentsWebApi:

	dotnet Wacth Run

Front-end --> students-web-app:

	npm start



Arquitetura

Back-end: Padrão Repository

Front-end: Padrão Single Page Aplication (SPA)

	Desse modo a API fica responsável pelo acesso aos dados (Server Side), o front-end é renderizado no lado do cliente (Client Side) e não no servidor, para que o tráfego das informações seja otimizado nos dois sentidos (Cliente - Servidor e Servidor - Cliente).
	
