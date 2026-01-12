# Golden Raspberry Awards Api

Golden Raspberry Awards Api é uma API REST que possibilita a leitura da lista de indicados e vencedores da categoria **Pior Filme** do **Golden Raspberry Awards**.

---

## 📋 Pré-requisitos para rodar a API

Antes de começar, certifique-se de ter as seguintes ferramentas instaladas:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker Desktop](https://docs.docker.com/desktop/features/wsl/) (para execução com Docker)
- [Git](https://git-scm.com/downloads)

## 📥 Clonando o repositório

Após instalar todos os pré-requisitos, clone o repositório:

```bash
git clone https://github.com/diegoferreirax/golden-raspberry-awards-api.git
```

Entre na pasta src do projeto:

```bash
cd golden-raspberry-awards-api/src
```

## 🚀 Execução

O modo de execução para este projeto é com docker compose para deixar tudo mais centralizado.

Ainda na pasta src do projeto, execute o comando do docker compose:
```bash
docker compose -f docker-compose.yml up -d --force-recreate
```

Verifique se o container foi iniciado:

```bash
docker ps
```

---

## 🧪 Testando a aplicação

Após iniciar a aplicação, você pode testá-la das seguintes formas:

### 1. Acessar o Swagger

Abra o navegador e acesse:

- `http://localhost:5139/swagger`

### 2. Entrar na pasta do projeto de testes

```bash
cd ../tests
```

### 3. Executando testes de integração

Execute o build do projeto:
```bash
dotnet build
```

Rodar os testes:
```bash
dotnet test
```