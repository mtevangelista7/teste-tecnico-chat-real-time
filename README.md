# chat em tempo real (discord)

Este é um projeto de estudo baseado no [teste técnico](https://gist.github.com/DanielHe4rt/4012e5bf9c612d9cee9efa654eb32f6d) de DanielHe4rt, implementado utilizando .NET 8 com Blazor WebAssembly hosted.

# Demo

https://github.com/mtevangelista7/teste-tecnico-chat-real-time/assets/123425476/a001bcba-832a-4ddd-b3be-5577004a93d7


## Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- [Blazor WebAssembly](https://blazor.net/)
- [SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr)
- [MudBlazor](https://mudblazor.com/)
- [Refit](https://github.com/reactiveui/refit)
- [Mapster](https://github.com/MapsterMapper/Mapster)
- [JWT](https://jwt.io/)
- [Entity Framework](https://docs.microsoft.com/en-us/ef/)
- [SQLite](https://www.sqlite.org/index.html)

## Configuração e Execução

### Pré-requisitos

- .NET 8 SDK

### Passos para Executar

1. Clone o repositório:

    ```bash
    git clone https://github.com/mtevangelista7/teste-tecnico-chat-real-time
    cd teste-tecnico-chat-real-time
    ```

2. Configure a string de conexão do SQLite no arquivo `appsettings.json` no projeto do servidor:

    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Data Source=chat.db"
    }
    ```

3. Restaure as dependências e execute as migrações do Entity Framework:

    ```bash
    dotnet restore
    dotnet ef database update -p TesteTecnicoDiscord.Infra
    ```

4. Execute o projeto:

    ```bash
    dotnet run --project TesteTecnicoDiscord.Infra
    ```

5. Navegue até `https://localhost:7290` no seu navegador para acessar a aplicação.
