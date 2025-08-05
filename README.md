# Projeto de Exemplo - Microsserviço com .NET 8, MongoDB e Kafka

Este projeto é um microsserviço criado com .NET 8, estruturado utilizando DDD (Domain-Driven Design), com suporte a MongoDB, Apache Kafka, testes unitários com xUnit e arquitetura baseada em CQRS com Dispatcher.

## ✨ Tecnologias e Padrões Utilizados

- **.NET 8**
- **MongoDB** via Docker
- **Apache Kafka** via Docker
- **CQRS (Command Query Responsibility Segregation)**
- **Dispatcher Pattern**
- **Injeção de Dependência (Microsoft DI)**
- **xUnit** para testes unitários
- **Clean Architecture** + **SOLID**
- **Entity Framework In-Memory** para testes


## Pré-requisitos
Docker e Docker Compose instalados
.NET 8 SDK instalado

## Subindo MongoDB e Kafka com Docker Compose

Necessário abrir prompt de comando onde está localizado o arquivo docker-compose.yml

executar os seguintes comandos:

docker-compose up -d   -> executa o arquivo docker-compose.yml.

docker ps -> verifica os containers que estão sendo executados.

## A API estará disponível em:

http://localhost:5207 (ou porta configurada no launchSettings.json)

## Visualizacao MongoDB

Necessário instalação de uma IDE, ex MongoDBCompass. 
Após instalação, só connectar com a connectionstring localizada no AppSettings do projeto.
