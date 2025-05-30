```markdown
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

## Exemplo de Uso

Use o Swagger UI para testar a API ou envie requisições para o endpoint de embalagem com o JSON de exemplo fornecido.

## Tecnologias

- .NET 8
- Entity Framework Core
- SQL Server
- Docker
- xUnit (Testes)
- Moq (Mocks)
- Swagger/OpenAPI
```