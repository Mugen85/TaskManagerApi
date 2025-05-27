# 🧾 Refactor entità, controller e test automatici

## ✅ Obiettivo della fase

Eseguire un refactor strutturale del progetto per:

* 🛠 Eliminare conflitti tra la classe `Task` e il tipo `System.Threading.Tasks.Task`
* 🌐 Completare la logica del controller REST
* 🧪 Scrivere test automatici per ogni operazione CRUD
* 🧱 Mantenere la separazione chiara tra entità, DTO e logica applicativa

---

## 🔄 Rinomina dell'entità `Task` in `ToDoTask`

* 🔁 La classe `Task` è stata rinominata in `ToDoTask`
* 📄 Il file `Task.cs` è stato rinominato in `ToDoTask.cs`
* ❌ Evitato conflitto con `System.Threading.Tasks.Task` (usato nei metodi `async`)
* 📌 Tutti i riferimenti aggiornati:

  * `DbContext`: `DbSet<ToDoTask> Tasks { get; set; }`
  * AutoMapper: `CreateMap<ToDoTask, TaskAllDto>().ReverseMap();`
  * Controller e test: utilizzo del nuovo nome `ToDoTask`

---

## 🧠 AutoMapper

* 📁 Creato file `UserProfile.cs` nella cartella `Mappings`
* ⚙️ Mappatura tra entità e DTO:

```csharp
CreateMap<ToDoTask, TaskAllDto>().ReverseMap();
```

---

## 🧪 Test automatici del controller

* 📄 Creata classe `TasksControllerTests.cs`
* 🔍 Ogni azione del controller è testata con un metodo `Fact` indipendente
* 🧪 Utilizzato EF Core InMemory per creare un `DbContext` isolato per ogni test
* 🧱 Struttura dei test suddivisa in:

  * `// Arrange`
  * `// Act`
  * `// Assert`
* ✏️ Nomi dei metodi descrittivi e focalizzati su un solo comportamento

### 🔬 Esempi di test

* `GetAllReturnsEmptyListWhenNoTasksExist`
* `CreateAddsTaskAndReturnsCreatedDto`
* `GetByIdReturnsTaskWhenExists`
* `UpdateChangesValuesCorrectly`
* `DeleteRemovesTaskFromDb`

> I metodi usano `System.Threading.Tasks.Task` come tipo di ritorno per evitare ambiguità

---

## 📦 Ristrutturazione delle cartelle completata

### `src/TaskManager.Api`

```
📦 TaskManager.Api
├── 📁 Controllers         --> TasksController, WeatherForecastController
├── 📁 Data                --> TaskDbContext.cs
├── 📁 Dtos                --> TaskAllDto.cs
├── 📁 Entities            --> ToDoTask.cs
├── 📁 Mappings            --> UserProfile.cs
├── 📁 Migrations          --> Migrazioni EF Core
├── 📄 Program.cs
├── 📄 appsettings.json
```

### `tests/TaskManager.Tests`

```
📦 TaskManager.Tests
├── 📄 TaskDbContextTests.cs
├── 📄 TasksControllerTests.cs
```

---

## ✅ Risultato della fase

* ✔️ Rinomina dell'entità completata senza conflitti
* ✔️ Logica CRUD completata nel controller
* ✔️ Test automatici funzionanti per ogni endpoint
* ✔️ AutoMapper correttamente integrato
* ✔️ Architettura solida, modulare, facilmente estendibile

---

# 🧾 Fase successiva: Integrazione di Swagger / OpenAPI

## ✅ Obiettivo della fase

Integrare Swagger (OpenAPI) per generare documentazione automatica dell'API e permettere test da interfaccia web.

* 📄 Documentare in modo trasparente ogni endpoint REST
* 💡 Permettere ai futuri sviluppatori (o frontend) di capire parametri, risposte, modelli
* ⚙️ Offrire un'interfaccia di test interattiva via browser (`/swagger`)

---

## 🔧 Passaggi implementati

1. 📦 Aggiunto pacchetto NuGet:

   ```bash
   dotnet add package Swashbuckle.AspNetCore
   ```

2. 🧩 Registrato Swagger in `Program.cs`:

   ```csharp
   builder.Services.AddSwaggerGen();
   ```

3. 🧰 Attivato Swagger e SwaggerUI:

   ```csharp
   var app = builder.Build();

   if (app.Environment.IsDevelopment())
   {
       app.UseSwagger();
       app.UseSwaggerUI();
   }
   ```

4. 🧪 Verificato accesso alla documentazione su:

   ```
   https://localhost:{porta}/swagger
   ```

5. 📝 Verificata la presenza automatica di ogni endpoint del `TasksController`

---

## ✅ Risultato della fase

* ✔️ Swagger installato e configurato correttamente
* ✔️ Documentazione automatica generata per tutti gli endpoint pubblici
* ✔️ Accesso da browser funzionante (`/swagger`)
* ✔️ Pronto per estensioni future (versioning, descrizioni, security)
