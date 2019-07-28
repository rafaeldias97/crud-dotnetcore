# DOTNET CORE + MONGODB + DOCKER + SWAGGER + JWT Autentication + CRUD
API usando DotNet Core com MongoDb, JWT Autentication, Swagger e Docker.

## Instalando e Executando
Para dar inicio ao projeto, é necessário ter o [DotNet Core 2.1](https://dotnet.microsoft.com/download/dotnet-core/2.1) e [Docker](https://hub.docker.com/editions/community/docker-ce-desktop-windows) instalado

```git
git clone https://github.com/rafaeldias97/crud-dotnetcore.git
```
```bash
docker-compose up --build
```

## Rotas no Swagger

Com o projeto sendo executado no docker, busque pela URL
> http://localhost/swagger/index.html

Voce tera acesso ao swagger, onde permite realizar a documentação de APIS Rest
Algumas rotas são protegidas utilizando JWT, um exemplo de rota protegia é a rota GET /api/Pessoa
Voce ira receber o status 401 ao realizar a requisição

![Rotas](https://i.ibb.co/5rGz4L7/Get-Protegido.png)

Para realizar a autenticação, voce deve primeiro realizar o cadastro na aplicação

![Rotas](https://i.ibb.co/4SBBZtP/Cadastro-Swagger.png)

Com o cadastro efetuado, deve ser realizado a autenticação propriamente dita na rota de Auth

![Rotas](https://i.ibb.co/vdpKfK0/Autenticar-Swagger.png)

Copie o TOKEN gerado

![Rotas](https://i.ibb.co/94VP4r4/Autenticar-Token.png)

O token de acesso poderá ser utilizado no Header da requisição utilizando o "Bearer <TOKEN>"

![Rotas](https://i.ibb.co/3Wm7GdS/Bearer.png)

Voce já pode utilizar as rotas protegidas como GET /api/Pessoa
Recebendo o status 200

![Rotas](https://i.ibb.co/qNsnjVq/Get-Desprotegido.png)
