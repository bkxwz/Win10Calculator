# Win10 Calculator

A Windows 10 style calculator built with C# and Windows Forms.

## Features

- Basic arithmetic: addition, subtraction, multiplication, division
- Advanced functions: percentage, square root, square, reciprocal
- Utility: negate, backspace, clear (C/CE)
- Dark theme matching Windows 10 calculator aesthetic

## Requirements

- .NET 8.0 SDK
- Windows OS (for running)

## Build

```bash
dotnet restore
dotnet build --configuration Release
```

## Run

```bash
dotnet run
```

## Publish

```bash
dotnet publish --configuration Release --runtime win-x64 --self-contained true
```

## License

MIT
