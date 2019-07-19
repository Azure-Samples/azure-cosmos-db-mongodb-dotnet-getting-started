---
languages:
- csharp
page_type: sample
products:
- dotnet
- azure
- azure-cosmos-db
description: "How to use the Azure Cosmos DB for MongoDB API to store and access data from a .NET application."
---

# Developing a .NET app using Azure Cosmos DB for MongoDB API
Azure Cosmos DB is a fully managed globally distributed, multi-model database service, transparently replicating your data across any number of Azure regions. You can elastically scale throughput and storage, and take advantage of fast, single-digit-millisecond data access using the API of your choice backed by 99.999 SLA. This sample shows you how to use the Azure Cosmos DB for MongoDB API to store and access data from a .NET application.

## Running this sample

* Before you can run this sample, you must have the following prerequisites:

   * An active Azure account. If you don't have one, you can sign up for a [free account](https://azure.microsoft.com/free/). Alternatively, you can use the [Azure Cosmos DB Emulator](https://azure.microsoft.com/documentation/articles/documentdb-nosql-local-emulator) for this tutorial.
   * Visual Studio 2017 (download and use the free [Visual Studio 2017 Community Edition](https://www.visualstudio.com/downloads/))

* Then, clone this repository using `git clone git@github.com:azure-samples/azure-cosmosdb-for-mongodb-api-dotnet-getting-started.git`.

* Next, substitute the `username`, `host`, and `password` in *DAL / Dal.cs* with your Cosmos DB account's values. 

* Install the *MongoDB.Driver* library from Visual Studio's Nuget Manager.

* Click *CTRL + F5* to run your application.

## About the code
The code included in this sample is intended to get you quickly started with a .NET application that connects to Azure Cosmos DB for MongoDB API.

## More information

- [Azure Cosmos DB](https://docs.microsoft.com/azure/cosmos-db/introduction)
- [Azure Cosmos DB for MongoDB API](https://docs.microsoft.com/en-us/azure/cosmos-db/mongodb-introduction)
- [MongoDB .NET driver documentation](http://mongodb.github.io/mongo-csharp-driver/)
