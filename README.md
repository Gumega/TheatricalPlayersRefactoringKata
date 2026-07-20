# 🎭 Theatrical Players Refactoring Kata

Projeto de refatoração, evolução de design de software e testes para o clássico kata *Theatrical Players*. A aplicação foi evoluída de um código monolítico e acoplado para uma arquitetura orientada a domínio, extensível a novos gêneros e formatos de saída, acompanhada de uma suíte robusta de testes unitários.

---

## 🚀 O que foi feito?

- **Polimorfismo para Gêneros (Strategy Pattern):** Substituição de estruturas condicionais por classes especialistas (`IPlayCalculator`). Adição nativa do novo gênero **`history`**.
- **Extensibilidade de Formatos (Open/Closed Principle):** Separação estrita entre cálculo e apresentação (`IStatementFormatter`), permitindo a emissão de extratos em **Texto** e **XML**.
- **Geração Automática de XML:** Serialização nativa via `XmlSerializer` (UTF-8, indentação e DTOs mapeados), eliminando interpolação manual de strings.
- **Processamento Assíncrono em Background:** Enfileiramento via `System.Threading.Channels` consumido por um `BackgroundService` (`StatementProcessingWorker`), gravando o XML final em disco.
- **Persistência de Dados (EF Core):** Mapeamento do histórico de extratos e itens, controlando os estados do processamento (`Pending`, `Processing`, `Completed`, `Failed`).
- **Precisão Financeira:** Transição de `float` para `decimal` no cálculo de valores monetários.

---

## 🏛️ Arquitetura e Design Patterns

A solução adota princípios de **Clean / Onion Architecture** e **DDD simplificado**:

```text
┌──────────────────────────────────────────────────────────┐
│ [Infrastructure Layer]                                   │
│   ├── Formatters (TextStatementFormatter, XmlFormatter)  │
│   ├── Persistence (EF Core, Entities, AppDbContext)      │
│   └── Background Processing (Channels, Worker Service)   │
└────────────────────────────┬─────────────────────────────┘
                             │ (Implementa abstrações)
┌────────────────────────────▼─────────────────────────────┐
│ [Application Layer]                                      │
│   └── StatementCalculator (Caso de Uso)                  │
└────────────────────────────┬─────────────────────────────┘
                             │
┌────────────────────────────▼─────────────────────────────┐
│ [Domain Layer] (Sem dependências externas)               │
│   ├── Entities & Value Objects (Invoice, Play, etc.)     │
│   └── Services & Factory (IPlayCalculator, Factory)      │
└──────────────────────────────────────────────────────────┘
```

## Padrões Aplicados
- **Strategy & Factory**: Desacoplamento do cálculo e criação das estratégias dos gêneros (Tragedy, Comedy, History).
- **Facade**: Ponto de entrada unificado via StatementPrinter, mantendo a compatibilidade com o contrato legado.
- **SOLID**: Responsabilidade única por componente (SRP) e facilidade de extensão de novos recursos sem alteração do código existente (OCP).

## 🧪 Estratégia de Testes
- **Testes de Domínio**: Unidades rápidas e determinísticas com xUnit + FluentAssertions cobrindo regras fiscais e limites por gênero.
- **Testes de Formatação**: Validação da estrutura XML gerada contra contratos definidos.
- **Testes de Aprovação (Verify)**: Preservados na camada externa para garantir a não regressão da aplicação.

## 💻 Como Executar
Pré-requisitos
- .NET 10 SDK (ou superior)
