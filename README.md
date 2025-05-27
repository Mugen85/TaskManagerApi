# 🚀 TaskManagerAPI

API RESTful moderna e professionale sviluppata in ASP.NET Core, con architettura modulare, test automatici, DTO, AutoMapper, autenticazione JWT, documentazione Swagger e futura interfaccia frontend in Blazor WebAssembly.

## 🌟 Obiettivo del progetto

Costruire un'applicazione reale e professionale che dimostri competenze avanzate nello sviluppo software con lo stack Microsoft, applicando i principi SOLID, una separazione chiara delle responsabilità e un'infrastruttura pronta per la crescita.

Il progetto è pensato per:

* 🏗️ Consolidare le basi architetturali da aspirante **architetto software**
* 🔧 Essere un punto di partenza per applicazioni più complesse e scalabili
* 💼 Mostrare a potenziali aziende la **serietà e l'approccio tecnico** al mondo .NET

## 🛠️ Tecnologie e strumenti utilizzati

* **ASP.NET Core 8.0** — Framework principale per l'API REST
* **Entity Framework Core** — Accesso e gestione dei dati con DbContext
* **AutoMapper** — Mapping automatico tra entità e DTO
* **xUnit** — Test automatici unitari su DbContext e logica
* **EF Core InMemory** — Test in isolamento senza database reale
* **Swagger/OpenAPI** — Documentazione dell'API
* **JWT** — Autenticazione con token
* **Blazor WebAssembly** — Frontend (fase futura)

## 📂 Architettura del progetto

```
📆 TaskManagerApi
📁 docs --> Documentazione tecnica e diagrammi
📁 src
 └📁 TaskManager.Api --> Codice dell'applicazione principale (API REST)
    📁 Controllers --> TasksController, WeatherForecastController, AuthController
    📁 Data --> TaskDbContext.cs
    📁 Dtos --> TaskAllDto.cs
    📁 Entities --> ToDoTask.cs
    📁 Mappings --> UserProfile.cs
    📁 Migrations --> Migrazioni EF Core
    📁 Settings --> JwtSettings.cs
    📄 Program.cs
    📄 appsettings.json
📁 tests
 └📁 TaskManager.Tests --> Test automatici del progetto API
    📄 TaskDbContextTests.cs
    📄 TasksControllerTests.cs
📌 .gitignore --> File di esclusione da Git
📖 README.md --> Introduzione generale al progetto
📆 TaskManagerAPI.sln --> Soluzione Visual Studio
```

## ✅ Stato attuale dello sviluppo

* ✔️ Rinomina dell'entità `Task` in `ToDoTask` per evitare conflitti
* ✔️ AutoMapper configurato correttamente con `UserProfile`
* ✔️ Implementazione completa CRUD nel controller `TasksController`
* ✔️ Test automatici su ogni endpoint con xUnit e InMemory
* ✔️ Integrazione Swagger per documentazione API (/swagger)
* ✔️ Integrazione JWT per autenticazione e protezione degli endpoint
* ✔️ Architettura modulare e testata

## 🗺️ Roadmap di sviluppo

### 🔧 Backend

* ✔️ Configurare `DbContext` con database reale (SQLite o altro)
* ✔️ Aggiunta `TasksController` con routing REST
* ✔️ Metodi CRUD completi (GET, POST, PUT, DELETE)
* ✔️ Integrare AutoMapper tra entità e DTO
* ✔️ Proteggere le rotte con JWT e attributo `[Authorize]`
* ✔️ Documentare le API con Swagger
* [ ] Aggiungere validazioni personalizzate e middleware di gestione errori

### 🌐 Frontend

* [ ] Creare progetto Blazor WebAssembly
* [ ] Collegare Blazor all'API REST
* [ ] Creare interfaccia per visualizzare, creare e gestire task
* [ ] Aggiungere autenticazione JWT sul frontend
* [ ] Ottimizzare stile e accessibilità

### 🚀 Integrazione e deployment

* [ ] Preparare dockerizzazione (se necessaria)
* [ ] Aggiungere GitHub Actions per CI/CD
* [ ] Implementare logging e monitoraggio base
* [ ] Scrivere documentazione tecnica e funzionale finale

## 📋 Come iniziare

```bash
# Clona il repository
git clone [repository-url]

# Naviga nella cartella del progetto
cd TaskManagerApi

# Ripristina i pacchetti NuGet
dotnet restore

# Esegui i test
dotnet test

# Avvia l'applicazione
dotnet run --project src/TaskManager.Api
```

## 🧩 Esecuzione dei test

```bash
# Esegui tutti i test
dotnet test

# Esegui i test con coverage
dotnet test --collect:"XPlat Code Coverage"
```

## 📚 Documentazione

La documentazione tecnica dettagliata si trova nella cartella `docs/`:

* [Documentazione completa del progetto](docs/Documentazione-creazione-progetto.md)
* Diagrammi dell'architettura (in sviluppo)
* Guide per sviluppatori (in sviluppo)

## 👨‍💼 Autore

**Mugen85**

🔗 Progetto pubblico per dimostrazione delle competenze full stack con focus backend C# / .NET

---

*Questo progetto è in attivo sviluppo e rappresenta un esempio di applicazione enterprise-ready con le migliori pratiche del mondo .NET*
