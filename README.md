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

*(English version below)*

### Documentazione API Interattiva (Swagger/OpenAPI)
L'API include una documentazione interattiva completa e la possibilità di testare gli endpoint direttamente dal browser, grazie all'integrazione con Swagger (OpenAPI).

**Per accedere alla UI di Swagger:**

1.  **Avvia l'applicazione** seguendo le istruzioni nella sezione "Come iniziare" (tipicamente con il comando `dotnet run --project src/TaskManager.Api`).
2.  Una volta che l'applicazione è in esecuzione, il terminale mostrerà gli URL su cui è in ascolto. Prendi nota dell'**URL base** (ad esempio, `https://localhost:7236` o `http://localhost:5181` – la porta specifica potrebbe variare a seconda della tua configurazione locale).
3.  Apri il tuo browser web e aggiungi `/swagger/index.html` all'URL base annotato.
    * **Esempio:** Se l'output del terminale indica che l'app è in ascolto su `https://localhost:7236`, allora l'URL completo per Swagger sarà:
        `https://localhost:7236/swagger/index.html`

Da questa pagina Swagger, potrai visualizzare tutti gli endpoint disponibili, i loro parametri, i tipi di risposta attesi e potrai anche inviare richieste di prova direttamente dall'interfaccia.

### Documentazione Aggiuntiva del Progetto
La documentazione tecnica più dettagliata, i diagrammi dell'architettura e le guide specifiche per gli sviluppatori si trovano all'interno della cartella `docs/` di questa repository:

* [Documentazione completa del progetto](docs/Documentazione-creazione-progetto.md)
* Diagrammi dell'architettura (in sviluppo)
* Guide per sviluppatori (in sviluppo)

---
---

## 📚 Documentation (English Version)

### Interactive API Documentation (Swagger/OpenAPI)
The API includes comprehensive interactive documentation and the ability to test endpoints directly from the browser, thanks to its integration with Swagger (OpenAPI).

**To access the Swagger UI:**

1.  **Start the application** by following the instructions in the "Come iniziare" (Getting Started) section (typically with the command `dotnet run --project src/TaskManager.Api`).
2.  Once the application is running, the terminal will display the URLs it is listening on. Take note of the **base URL** (e.g., `https://localhost:7236` or `http://localhost:5181` – the specific port may vary depending on your local configuration).
3.  Open your web browser and append `/swagger/index.html` to the noted base URL.
    * **Example:** If the terminal output indicates the app is listening on `https://localhost:7236`, then the full URL for Swagger will be:
        `https://localhost:7236/swagger/index.html`

From this Swagger page, you can view all available endpoints, their parameters, expected response types, and you can also send test requests directly from the interface.

### Additional Project Documentation
More detailed technical documentation, architectural diagrams, and specific developer guides are located within the `docs/` folder of this repository:

* [Complete project documentation (Italian)](docs/Documentazione-creazione-progetto.md)
* Architecture diagrams (under development)
* Developer guides (under development)

## 👨‍💼 Autore

**Mugen85**

🔗 Progetto pubblico per dimostrazione delle competenze full stack con focus backend C# / .NET

---

*Questo progetto è in attivo sviluppo e rappresenta un esempio di applicazione enterprise-ready con le migliori pratiche del mondo .NET*
