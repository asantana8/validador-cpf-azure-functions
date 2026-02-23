# 🚀 Azure Function: Validador de CPF (Serverless)

Este projeto apresenta um microsserviço escalável e de baixo custo para validação de CPFs, desenvolvido durante meus estudos para a certificação **Microsoft Azure Developer Associate (AZ-204)**. 

A solução utiliza a arquitetura **Serverless** com Azure Functions para garantir alta disponibilidade e cobrança baseada estritamente no uso.

## 🛠 Tecnologias e Ferramentas
* **Linguagem:** C# (.NET 8.0 - Isolated Worker Model)
* **Engine:** Azure Functions (Trigger HTTP)
* **IDE:** Visual Studio Code com Azure Functions Core Tools
* **Testes:** REST Client (VS Code) e PowerShell
* **Cloud:** Microsoft Azure (Plano Flex Consumption)

## 🏗 Arquitetura
O microsserviço foi desenhado seguindo princípios modernos de nuvem:
* **Endpoint:** Recebe requisições via método `POST`.
* **Escalabilidade:** Escala automaticamente de zero a milhares de instâncias conforme a demanda.
* **Resiliência:** Implementação de lógica assíncrona para melhor aproveitamento de recursos.

## 📁 Estrutura do Projeto
* `FnValidaCPF.cs`: Contém o gatilho HTTP e a lógica de validação.
* `testes.http`: Arquivo para testes rápidos de integração (Local e Produção).
* `host.json` & `local.settings.json`: Configurações de runtime e variáveis de ambiente.

## 🚀 Como Executar o Projeto

### Pré-requisitos
* [.NET SDK 8.0](https://dotnet.microsoft.com/download)
* [Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local)

### Localmente
1. Clone este repositório.
2. No terminal, execute:
   ```bash
   dotnet build
   func start
