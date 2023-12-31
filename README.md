# Online Shop App

An simple but powerfull online shop application.

**Table of contents:**

- [Prerequisites](#prerequisites)
- [Running the solution](#running-the-solution)
- [Build solution](#build-solution)
- [Database commands](#database-commands)

## Prerequisites

01. Install latest [NET Core 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)

02. Check if *dotnet* is installed by running next command:

```sh
dotnet --version
```

03. Restore tools used in development by running next command:

```sh
dotnet tool restore
```

04. Ensure database has latest schema applied

```sh
dotnet ef database update --project ./src/Infrastructure/ --startup-project ./src/Server/
```

## Running the solution

01. To run the application execute command:

```sh
dotnet watch run --project ./src/Server/
```

02. Open browser at URL <https://localhost:7193/> and start explore the app

## Build solution

01. [Check build runs on CI][CI-link] **--or--**

02. On the `root folder` run next command:

```sh
dotnet build
```

## Database commands

In order to properly use any of the **Entity Framework commands** we need to:

- specify `./src/Infrastructure` as the `--project` (since there we keep the database migrations)
- specify `./src/Server` as the `--startup-project` (since the Server contains the database connection string)
- and in case of working with `ef migrations add` command, specify `Persistence/Migrations` as the `--output-dir` (since there we keep all database migration files).

Handy commands:

- `add new migration`

```sh
dotnet ef migrations add "MigrationName" --project ./src/Infrastructure/ --startup-project ./src/Server/ --output-dir ./Persistence/Migrations/
```

- `update database to latest migration`

```sh
dotnet ef database update --project ./src/Infrastructure/ --startup-project ./src/Server/
```

- `update database to specific migration`

```sh
dotnet ef database update "MigrationName" --project ./src/Infrastructure/ --startup-project ./src/Server/
```

- `list migrations`

```sh
dotnet ef migrations list --project ./src/Infrastructure/ --startup-project ./src/Server/
```

- `drop database`

```sh
dotnet ef database drop --project ./src/Infrastructure/ --startup-project ./src/Server/
```

- `remove latest migration`

```sh
dotnet ef migrations remove --project ./src/Infrastructure/ --startup-project ./src/Server/
```
