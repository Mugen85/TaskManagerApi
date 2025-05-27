# 🧾 Fase successiva a creazione: refactor entità, controller e test automatici

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
