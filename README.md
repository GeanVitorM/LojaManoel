# Loja do Seu Manoel - API de Embalagem

API para automatizar o processo de embalagem de pedidos da loja de jogos online do Seu Manoel.

## Pré-requisitos

- Docker
- Docker Compose

## Como executar

1. Clone o repositório
2. Execute o comando:
```bash
docker-compose up --build
```

3. A API estará disponível em: http://localhost:8080
4. Swagger UI: http://localhost:8080/swagger

## Executar Testes

Para executar os testes unitários:

```bash
dotnet test
```

## Endpoints

### Embalagem
- `POST /api/embalagem/processar` - Processa pedidos e retorna embalagem otimizada

### Pedidos
- `GET /api/pedidos` - Lista todos os pedidos
- `GET /api/pedidos/{id}` - Obtém pedido por ID
- `POST /api/pedidos` - Cria novo pedido
- `PUT /api/pedidos/{id}` - Atualiza pedido
- `DELETE /api/pedidos/{id}` - Remove pedido

### Caixas
- `GET /api/caixas` - Lista caixas disponíveis

## Caixas Disponíveis

- Caixa 1: 30 x 40 x 80 cm
- Caixa 2: 80 x 50 x 40 cm  
- Caixa 3: 50 x 80 x 60 cm

## Algoritmo de Embalagem

O sistema utiliza um algoritmo guloso que:
1. Ordena produtos por volume (maior primeiro)
2. Tenta encaixar cada produto na menor caixa possível
3. Agrupa produtos quando possível na mesma caixa
4. Retorna produtos que não cabem com observação

## Exemplo de uso da API

## Endpoints

### 1. `POST /api/Embalagem/processar`

Processa os pedidos e retorna as embalagens sugeridas para cada pedido.

**Exemplo de requisição:**

```json
{
  "pedidoId": 100,
  "pedido": {
    "pedidoId": 100,
    "criadoEm": "2025-05-30T13:10:07.554Z"
  },
  "produtos": [
    {
      "produtoId": "Controle PS4",
      "altura": 10,
      "largura": 40,
      "comprimento": 30,
      "pedidoId": 100
    }
  ],
  "criadoEm": "2025-05-30T13:10:07.554Z"
}
```
---
**Exemplo de resposta:**
```json
{
  "pedidos": [
    {
      "pedido_id": 100,
      "caixas": [
        {
          "caixa_id": "Caixa 1",
          "produtos": ["Controle PS4"],
          "observacao": null
        }
      ]
    }
  ]
}
```
---
### 2. `GET /api/pedidos`

Lista todos os pedidos cadastrados.

**Exemplo de resposta:**
```json
[
  {
    "pedidoId": 100,
    "criadoEm": "2025-05-30T13:10:07.554Z",
    "produtos": [
      {
        "produtoId": "Controle PS4",
        "altura": 10,
        "largura": 40,
        "comprimento": 30,
        "pedidoId": 100
      }
    ]
  }
]
```

---

### 3. `GET /api/pedidos/{id}`

Obtém um pedido específico pelo ID.

**Exemplo de resposta:**
```json
{
  "pedidoId": 100,
  "criadoEm": "2025-05-30T13:10:07.554Z",
  "produtos": [
    {
      "produtoId": "Controle PS4",
      "altura": 10,
      "largura": 40,
      "comprimento": 30,
      "pedidoId": 100
    }
  ]
}
```

---

### 4. `POST /api/pedidos`

Cria um novo pedido.

**Exemplo de requisição:**
```json
{
  "pedidoId": 101,
  "produtos": [
    {
      "produtoId": "Fifa 24",
      "altura": 2,
      "largura": 15,
      "comprimento": 20
    }
  ]
}
```

**Exemplo de resposta:**
```json
{
  "pedidoId": 101,
  "criadoEm": "2025-05-30T13:10:07.554Z",
  "produtos": [
    {
      "produtoId": "Fifa 24",
      "altura": 2,
      "largura": 15,
      "comprimento": 20
    }
  ]
}
```

---

### 5. `PUT /api/pedidos/{id}`

Atualiza um pedido existente.

**Exemplo de requisição:**
```json
{
  "pedidoId": 100,
  "produtos": [
    {
      "produtoId": "Headset",
      "altura": 15,
      "largura": 20,
      "comprimento": 25
    }
  ]
}
```

---

### 6. `DELETE /api/pedidos/{id}`

Remove um pedido pelo ID.

**Resposta:**  
- 204 No Content (sem corpo)

---

### 7. `GET /api/caixas`

Lista todas as caixas disponíveis.

**Exemplo de resposta:**
```json
[
  {
    "id": 1,
    "descricao": "Caixa 1",
    "altura": 30,
    "largura": 40,
    "comprimento": 80
  },
  {
    "id": 2,
    "descricao": "Caixa 2",
    "altura": 80,
    "largura": 50,
    "comprimento": 40
  },
  {
    "id": 3,
    "descricao": "Caixa 3",
    "altura": 50,
    "largura": 80,
    "comprimento": 60
  }
]
```

---
> Consulte o Swagger em [http://localhost:8080/swagger](http://localhost:8080/swagger) para mais exemplos e detalhes de cada endpoint.

## Tecnologias

- .NET 8
- Entity Framework Core
- SQL Server
- Docker
- xUnit (Testes)
- Moq (Mocks)
- Swagger/OpenAPI