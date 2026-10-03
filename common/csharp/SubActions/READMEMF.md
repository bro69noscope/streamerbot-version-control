Listen, easy: we just mirror the streamer.bot layout like this:

SubActions
├── Common                                           <- profile scope (Common | Ftp | Production)
│   ├── AppGroupName                                 <- app scope (Obs | Dota2 | General ...)
│   │   ├── ActionGroupName                          <- action scope (ManageUi | DoAnythingElse ...)
│   │   │   ├── ActionName                           <- name of the streamer.bot action
│   │   │   │   ├── SubActionExecuteCodeName.cs      <- name of the streamer.bot c# sub-action
│   │   │   │   ├── SubActionExecuteCodeName.shadow.csproj
│   │   │   │   ├── SubActionExecuteCodeName.cs
│   │   │   │   └── SubActionExecuteCodeName.shadow.csproj
│   │   │   └── ActionName
│   │   │       ├── SubActionExecuteCodeName.cs
│   │   │       └── SubActionExecuteCodeName.shadow.csproj
│   │   └── ActionGroupName
│   │       └── ActionName
│   │           ├── SubActionExecuteCodeName.cs
│   │           └── SubActionExecuteCodeName.shadow.csproj
│   └── AppGroupName
│       └── ActionGroupName
│           └── ActionName
│               ├── SubActionExecuteCodeName.cs
│               └── SubActionExecuteCodeName.shadow.csproj
├── Ftp
│   └── AppGroupName
│       └── ActionGroupName
│           └── ...
└── Production
    └── AppGroupName
        └── ActionGroupName
            └── ActionName
                ├── SubActionExecuteCodeName.cs
                └── SubActionExecuteCodeName.shadow.csproj

(The SubActionExecuteCodeName filename must match the C# sub-action's own name in Streamer.bot:
`Execute Code (SubActionExecuteCodeName)`)

Each SubActionExecuteCodeName.cs gets a SubActionExecuteCodeName.shadow.csproj next to it so the LSP
has one compilation per CPHInline.

The csproj is a single line, everything (framework, CPH shim, Streamer.bot ref, the .cs to compile)
comes from Directory.Build.props keyed on the project name.

To create the missing ones and add them to csharp.sln, run from common/csharp:
`.\add-shadow-projects.ps1`

Extra references are opt-in so the shadow mirrors what the sub action really references in
Streamer.bot. set them in the .shadow.csproj when needed:
```
    <PropertyGroup>
      <UseBroStreamerTools>true</UseBroStreamerTools>
      <UseNewtonsoftJson>true</UseNewtonsoftJson>
    </PropertyGroup>
```

In order for the LSP to properly diagnose the project, do not forget to run `dotnet sln restore
csharp.sln` after the csproj was built.
