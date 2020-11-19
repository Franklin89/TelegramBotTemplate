# Telegram Bot Template [WORK IN PROGRESS]

This is a template project for a Telegram Bot using the the [Telegram.Bot](https://github.com/TelegramBots/telegram.bot) Nuget Package.

## Features

- ...

## Using the template

### Installation

From NuGet:

```
dotnet new -i TelegramBotTemplate
```

Locally built nupkg:

```
dotnet new -i TelegramBotTemplate.x.x.x.nupkg
```

Local folder:

```
dotnet new -i <PATH>
```

Where `<PATH>` is the path to the folder containing .template.config.

### Run

```
dotnet new telegram-bot -n YourCompany.Bot
```

Use the `-n` or `--name` parameter to change the name of the output created. This string is also used to substitute the namespace name in the .cs file for the project.

### Uninstall

```
dotnet new -u TelegramBotTemplate
```
