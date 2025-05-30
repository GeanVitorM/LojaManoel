# Loja do Seu Manoel - Sistema de Empacotamento

Este projeto é uma API para calcular o empacotamento ideal de produtos em caixas para a loja de jogos online do Seu Manoel.

## Pré-requisitos

- Docker Desktop
- Docker Compose

## Como executar

1. Clone o repositório
2. Navegue até a pasta do projeto
3. Execute o comando:
```bash
docker-compose up --build
```

A API estará disponível em:
- HTTP: http://localhost:8080
- HTTPS: https://localhost:8081

O Swagger UI estará disponível em:
- http://localhost:8080/swagger
- https://localhost:8081/swagger

## Estrutura do Projeto

- `LojaManoel.API/`: Projeto principal da API
  - `Controllers/`: Controladores da API
  - `Models/`: Classes de modelo
  - `Services/`: Serviços de negócio

## Tecnologias Utilizadas

- .NET 7.0
- ASP.NET Core Web API
- SQL Server
- Docker
- Swagger/OpenAPI

## Exemplo de Uso

Para testar a API, você pode usar o Swagger UI ou fazer uma requisição POST para `/api/Empacotamento` com o seguinte formato de JSON:

```json
{
  "pedidos": [
    {
      "pedido_id": 1,
      "produtos": [
        {
          "produto_id": "PS5",
          "dimensoes": {
            "altura": 40,
            "largura": 10,
            "comprimento": 25
          }
        }
      ]
    }
  ]
}
```

A resposta será no formato:

```json
{
  "pedidos": [
    {
      "pedido_id": 1,
      "caixas": [
        {
          "caixa_id": "Caixa 1",
          "produtos": ["PS5"]
        }
      ]
    }
  ]
}
``` 