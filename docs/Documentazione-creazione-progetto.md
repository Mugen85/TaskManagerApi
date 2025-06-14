# 🧾 Documentazione progetto TaskManagerAPI

## ✅ Obiettivo

Sviluppare una **Web API RESTful completa** in ASP.NET Core per la gestione di task, con architettura professionale, test automatici, DTO, AutoMapper, autenticazione JWT e frontend con Blazor WebAssembly.

## 📁 Struttura del progetto

```
📦 TaskManagerApi
├── 📁 src                  --> codice dell'applicazione principale
│   └── 📁 TaskManager.Api
├── 📁 tests                --> test automatici
│   └── 📁 TaskManager.Tests
├── 📁 docs                 --> documentazione tecnica e diagrammi
├── .gitignore             --> esclusione file da Git
├── README.md              --> introduzione generale al progetto
└── TaskManagerAPI.sln     --> file soluzione Visual Studio
```

## 🔷 Fase 1: creazione della struttura professionale

- Creata soluzione Visual Studio `TaskManagerAPI`
- Creato progetto ASP.NET Core Web API `TaskManager.Api`
- Spostato in `src/`, modificato file `.sln` a mano
- Creata cartella `tests/` e `docs/`

## 🔷 Fase 2: progetto di test `TaskManager.Tests`

- Creato con xUnit
- Primo test con `[Fact]` che verifica una somma
- Test passato correttamente

## 🔷 Fase 3: costruzione API REST

- Creata entità `Task` con proprietà `Id`, `Title`, `Description`, `IsCompleted`
- Creato `TaskAllDto` per esporre solo alcuni campi
- Costruito `TaskDbContext` con `DbSet<Task>`
- Risolto conflitto con `System.Threading.Tasks.Task` usando nome completo o alias

## 🔷 Fase 4: test realistico con EF Core InMemory

### 📄 File: `ExampleTests.cs`

Contiene 3 test separati:

1. **`Aggiunge_Task_Correttamente`**
   - Crea un task, lo salva e verifica che esista nel contesto

2. **`Legge_Task_Salvato`** 
   - Legge un task salvato precedentemente e ne verifica il titolo

3. **`Verifica_IsCompleted_False_Di_Default`**
   - Controlla che il flag booleano sia `false` come previsto

Utilizza `DbContextOptions` e `UseInMemoryDatabase()` per simulare l'ambiente senza database reale.

## 🔷 Fase 5: collegamento al database reale con SQLite

- Installato pacchetto NuGet `Microsoft.EntityFrameworkCore.Sqlite`
- Aggiunta stringa di connessione in `appsettings.json`
- Registrato `TaskDbContext` in `Program.cs` tramite dependency injection
- Installato pacchetto `Microsoft.EntityFrameworkCore.Tools`
- Creata migrazione iniziale `InitialCreate` con il comando `Add-Migration`
- Applicata la migrazione con `Update-Database`
- Generato il file SQLite `taskmanager.db` con la tabella `Tasks`

## 🔜 Prossimi step

1. 🔧 **Configurare** `DbContext` in `Program.cs` con database reale (es: SQLite, MySQL) ✅ **FATTO**

2. 🛠️ **Aggiungere** `TasksController` con routing REST ✅ **FATTO**

3. 🌀 **Integrare AutoMapper** per conversione entità <-> DTO ✅ **FATTO**

4. ✅ **Scrivere metodi CRUD completi** (GET, POST, PUT, DELETE) ✅ **FATTO**

5. 🔐 **Aggiungere JWT** per autenticazione e protezione delle rotte ✅ **FATTO**

6. 📄 **Abilitare Swagger** per documentare le API ✅ **FATTO**

7. 🎨 **Creare frontend in Blazor WebAssembly** per interfaccia utente completa

