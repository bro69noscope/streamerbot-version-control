Listen, easy: we just mirror the streamer.bot layout like this, under a Scope
folder (Common / Ftp / Production) matching the vcdata instance the sub actions
belong to:

SubActions
├── Common
│   ├── ActionGroupName
│   │   ├── ActionName
│   │   │   ├── SubActionExecuteCodeName.cs
│   │   │   ├── SubActionExecuteCodeName.shadow.csproj
│   │   │   ├── SubActionExecuteCodeName.cs
│   │   │   └── SubActionExecuteCodeName.shadow.csproj
│   │   └── ActionName
│   │       ├── SubActionExecuteCodeName.cs
│   │       └── SubActionExecuteCodeName.shadow.csproj
│   └── ActionGroupName
│       └── ActionName
│           ├── SubActionExecuteCodeName.cs
│           └── SubActionExecuteCodeName.shadow.csproj
├── Ftp
│   └── ActionGroupName
│       └── ...
└── Production
    └── ActionGroupName
        └── ...

(The SubActionExecuteCodeName filename must match the C# sub-action's own name
in Streamer.bot: `Execute Code (SubActionExecuteCodeName)`)

Each SubActionExecuteCodeName.cs gets a SubActionExecuteCodeName.shadow.csproj
next to it so the LSP has one compilation per CPHInline.

The csproj is a single line, everything (framework, CPH shim, Streamer.bot ref,
the .cs to compile) comes from Directory.Build.props keyed on the project name.

To create the missing ones and add them to csharp.sln, run from common/csharp:
    `.\add-shadow-projects.ps1`

Extra references are opt-in so the shadow mirrors what the sub action really
references in Streamer.bot. set them in the .shadow.csproj when needed:
```
    <PropertyGroup>
      <UseBroStreamerTools>true</UseBroStreamerTools>
      <UseNewtonsoftJson>true</UseNewtonsoftJson>
    </PropertyGroup>
```
