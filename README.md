
# Assistência Técnica API

Bem-vindo à documentação da API para gestão de uma assistência técnica. Esta API permite gerenciar clientes, equipamentos, ordens de serviço e diversas funcionalidades relacionadas ao fluxo de trabalho de uma assistência técnica.

## Funcionalidades

### 1. Cadastro de Clientes
- **Adicionar novo cliente**: Adiciona um novo cliente ao sistema.
- **Editar informações do cliente**: Atualiza as informações de um cliente existente.
- **Listar todos os clientes**: Retorna uma lista de todos os clientes cadastrados.

### 2. Cadastro de Equipamentos
- **Adicionar novo equipamento**: Registra um novo equipamento.
- **Editar informações do equipamento**: Atualiza os dados de um equipamento existente.
- **Listar todos os equipamentos**: Retorna uma lista de todos os equipamentos cadastrados.

### 3. Ordens de Serviço
- **Criar nova ordem de serviço**: Cria uma nova ordem de serviço vinculada a um cliente e equipamento.
- **Editar ordem de serviço**: Permite editar os detalhes de uma ordem de serviço.
- **Listar todas as ordens de serviço**: Retorna todas as ordens de serviço registradas.
- **Atualizar status da ordem de serviço**: Atualiza o status de uma ordem de serviço (ex: em andamento, concluída).

### 4. Histórico de Serviços
- **Visualizar histórico de serviços por cliente** (em desenvolvimento): Permite visualizar o histórico de serviços prestados a um cliente específico.
- **Visualizar histórico de serviços por equipamento** (em desenvolvimento): Exibe o histórico de serviços realizados em um determinado equipamento.

### 5. Notificações (em desenvolvimento)
- **Enviar notificações por e-mail**: Envia notificações sobre o andamento da ordem de serviço por e-mail.
- **Enviar notificações por SMS**: Notificações via SMS sobre status de ordens de serviço.

### 6. Relatórios (em desenvolvimento)
- **Gerar relatório de serviços realizados**: Gera um relatório detalhado dos serviços já realizados.
- **Gerar relatório de tempo médio de conserto**: Exibe o tempo médio de conserto de equipamentos.

### 7. Controle de Estoque (em desenvolvimento)
- **Adicionar peças e materiais**: Adiciona novas peças ou materiais ao estoque.
- **Atualizar quantidade de estoque**: Atualiza a quantidade de peças e materiais disponíveis.
- **Listar peças e materiais em estoque**: Lista todas as peças e materiais em estoque.

### 8. Funcionalidades Relacionadas a Usuários
- **Cadastro de Usuários**: Permite adicionar novos usuários ao sistema.
- **Obter informações do Usuário**: Consulta as informações detalhadas de um usuário.
- **Editar informações do usuário**: Permite editar os dados de um usuário.
- **Listar todos os usuários**: Lista todos os usuários registrados.
- **Autenticação e Autorização**: Login e logout, além do gerenciamento de permissões de acesso com base em papéis (roles), como Admin, Técnico e Cliente.
- **Recuperação de Senha** (em desenvolvimento): Permite a recuperação de senha de usuários.

## Entidades Principais
### Cliente
- `Id`: Identificador único do cliente.
- `Nome`: Nome completo do cliente.
- `Telefone`: Telefone de contato do cliente.
- `Email`: Endereço de e-mail do cliente.

### Endereço
- `Street`: Rua do cliente.
- `Number`: Número da residência.
- `Complement`: Complemento do endereço.
- `Neighborhood`: Bairro.
- `City`: Cidade.
- `State`: Estado.
- `ZipCode`: Código postal.

### Equipamento
- `Id`: Identificador único do equipamento.
- `Marca`: Marca do equipamento.
- `Modelo`: Modelo do equipamento.
- `Número de Série`: Número de série do equipamento.
- `Descrição do Problema`: Descrição do problema relatado.

### Ordem de Serviço
- `Id`: Identificador único da ordem de serviço.
- `ClienteId`: Identificador do cliente associado.
- `EquipamentoId`: Identificador do equipamento associado.
- `Status`: Status da ordem de serviço (em andamento, concluída, etc.).
- `Data de Criação`: Data em que a ordem de serviço foi criada.

## Como Executar o Projeto

### Requisitos
- **.NET 8**
- **Entity Framework Core** 8.0 ou superior
- Banco de dados: **My SQL** (configurável)

### Passos para Configuração
1. Clone o repositório.
2. Configure as strings de conexão no arquivo `appsettings.json`.
3. Execute os comandos de migração do Entity Framework Core para gerar o banco de dados:
   ```bash
   dotnet ef database update
   ```
4. Inicie o projeto:
   ```bash
   dotnet run
   ```

