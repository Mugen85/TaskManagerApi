# 🧾 Fase successiva: Refactor entità, controller e test automatici

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
├── 📁 Controllers         --> TasksController, WeatherForecastController, AuthController
├── 📁 Data                --> TaskDbContext.cs
├── 📁 Dtos                --> TaskAllDto.cs
├── 📁 Entities            --> ToDoTask.cs
├── 📁 Mappings            --> UserProfile.cs
├── 📁 Migrations          --> Migrazioni EF Core
├── 📁 Settings            --> JwtSettings.cs
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
   builder.Services.AddEndpointsApiExplorer();
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

---

# 🧾 Fase successiva: Integrazione JWT (JSON Web Token)

## ✅ Obiettivo della fase

Implementare l'autenticazione tramite token JWT per:

* 🔐 Proteggere gli endpoint con `[Authorize]`
* 🔑 Generare token JWT validi via API `/api/auth/login`
* ✅ Validare token per ogni richiesta

---

## 🔧 Passaggi implementati

1. 📦 Aggiunto pacchetto NuGet:

   ```bash
   dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
   ```

2. 📄 Aggiunta sezione `Jwt` in `appsettings.json`:

```json
"Jwt": {
  "Key": "una-chiave-sicura-di-almeno-32-caratteri",
  "Issuer": "TaskManagerApi",
  "Audience": "TaskManagerApiClient",
  "ExpiresInMinutes": 60
}
```

3. 🧩 Creata classe `JwtSettings.cs` in `Settings/`

4. 🧰 Configurata autenticazione JWT in `Program.cs` con validazione sicura

5. 🧪 Gestito il caso di `null` su `Get<JwtSettings>()` con controllo esplicito:

```csharp
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()
    ?? throw new InvalidOperationException("Configurazione JWT mancante");
```

6. 👤 Creato `AuthController` con metodo `POST /api/auth/login`

   * Credenziali hardcoded: `admin` / `123456`
   * Restituzione token JWT in risposta

7. 🔒 Aggiunto `[Authorize]` agli endpoint del controller `TasksController`

8. ⚙️ Ordine corretto del middleware:

   ```csharp
   app.UseAuthentication();
   app.UseAuthorization();
   app.MapControllers();
   ```

9. 🧪 Verifica in Swagger:

   * Inserimento token JWT nell’`Authorize` senza `Bearer`
   * Test chiamate protette dopo autenticazione
   * Corretto errore `Bearer Bearer {token}` → solo token puro

---

## ✅ Risultato della fase

* ✔️ Token JWT generato e validato correttamente
* ✔️ Endpoint protetti funzionanti con `[Authorize]`
* ✔️ Swagger aggiornato per supportare autorizzazione Bearer
* ✔️ Tutto il sistema pronto per gestire utenti, ruoli e frontend Blazor
