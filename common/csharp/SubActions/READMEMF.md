listen, easy: we just mirror the streamer.bot layout like this, under a Scope folder
(Common / Ftp / Production) matching the vcdata instance the sub actions belong to
SubActions
├── Common
│   ├── ActionGroupName
│   │   ├── ActionName
│   │   │   ├── SubActionExecuteCodeName.cs
│   │   │   ├── SubActionExecuteCodeName.shadow.csproj
│   │   │   ├── SubActionExecuteCodeName.cs
│   │   │   └── SubActionExecuteCodeName.shadow.csproj
│   │   └── ActionName
│   │       └── SubActionExecuteCodeName.cs
│   └── ActionGroupName
│       └── ActionName
│           └── SubActionExecuteCodeName.cs
├── Ftp
│   └── ActionGroupName
│       └── ...
└── Production
    └── ActionGroupName
        └── ...
