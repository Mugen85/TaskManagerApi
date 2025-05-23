# 🚀 TaskManagerAPI

API RESTful moderna e professionale sviluppata in ASP.NET Core, con architettura modulare, test automatici, DTO, AutoMapper e futura interfaccia frontend in Blazor WebAssembly.

## 🎯 Obiettivo del progetto

Costruire un'applicazione reale e professionale che dimostri competenze avanzate nello sviluppo software con lo stack Microsoft, applicando i principi SOLID, una separazione chiara delle responsabilità e un'infrastruttura pronta per la crescita.

Il progetto è pensato per:

- 🏗️ Consolidare le basi architetturali da aspirante **architetto software**
- 🔧 Essere un punto di partenza per applicazioni più complesse e scalabili  
- 💼 Mostrare a potenziali aziende la **serietà e l'approccio tecnico** al mondo .NET

## 🛠️ Tecnologie e strumenti utilizzati

- **ASP.NET Core 8.0** — Framework principale per l'API REST
- **Entity Framework Core** — Accesso e gestione dei dati con DbContext  
- **AutoMapper** — Mapping automatico tra entità e DTO
- **xUnit** — Test automatici unitari su DbContext e logica
- **EF Core InMemory** — Test in isolamento senza database reale
- **Swagger/OpenAPI** — Documentazione dell'API (da integrare)
- **JWT** — Autenticazione con token (in fase di sviluppo)
- **Blazor WebAssembly** — Frontend (fase futura)

## 📂 Architettura del progetto

```
📦 TaskManagerApi
├── 📁 src                  --> Codice dell'applicazione principale
│   └── 📁 TaskManager.Api
├── 📁 tests                --> Test automatici
│   └── 📁 TaskManager.Tests
├── 📁 docs                 --> Documentazione tecnica e diagrammi
├── 📝 .gitignore           --> Esclusione file da Git
├── 📖 README.md            --> Introduzione generale al progetto
└── 📦 TaskManagerAPI.sln   --> File soluzione Visual Studio
```

## ✅ Stato attuale dello sviluppo

- ✔️ Struttura professionale con separazione `src/`, `tests/`, `docs/`
- ✔️ Creazione della soluzione e dei due progetti (API e test)
- ✔️ Implementazione iniziale dell'entità `Task` e `TaskAllDto`
- ✔️ Costruzione del `TaskDbContext` con EF Core
- ✔️ Test automatici separati con EF Core InMemory
- ✔️ Documentazione interna (`docs/riassunto-taskmanagerapi.md`)

## 🗺️ Roadmap di sviluppo

### 🔧 Backend

- [ ] Configurare `DbContext` in `Program.cs` con database reale (SQLite o MySQL)
- [ ] Aggiungere `TasksController` con routing REST
- [ ] Implementare metodi CRUD completi (GET, POST, PUT, DELETE)
- [ ] Aggiungere validazioni e middleware per gestione errori
- [ ] Integrare AutoMapper per mappare tra entità e DTO
- [ ] Proteggere le rotte con JWT (autenticazione e ruoli)
- [ ] Documentare le API con Swagger

### 🌐 Frontend

- [ ] Creare progetto Blazor WebAssembly
- [ ] Collegare Blazor all'API REST
- [ ] Creare interfaccia per visualizzare, creare e gestire task
- [ ] Aggiungere autenticazione JWT sul frontend
- [ ] Ottimizzare stile e accessibilità

### 🚀 Integrazione e deployment

- [ ] Preparare dockerizzazione (se necessaria)
- [ ] Aggiungere GitHub Actions per CI/CD
- [ ] Implementare logging e monitoraggio base
- [ ] Scrivere documentazione tecnica e funzionale finale

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

## 🧪 Esecuzione dei test

```bash
# Esegui tutti i test
dotnet test

# Esegui i test con coverage
dotnet test --collect:"XPlat Code Coverage"
```

## 📚 Documentazione

La documentazione tecnica dettagliata si trova nella cartella `docs/`:

- [Documentazione completa del progetto](docs/Prima-fase-creazione-struttura.md)
- Diagrammi dell'architettura (in sviluppo)
- Guide per sviluppatori (in sviluppo)

## 👨‍💻 Autore

**Mugen85**

🔗 Progetto pubblico per dimostrazione delle competenze full stack con focus backend C# / .NET

---

*Questo progetto è in attivo sviluppo e rappresenta un esempio di applicazione enterprise-ready con le migliori pratiche del mondo .NET*