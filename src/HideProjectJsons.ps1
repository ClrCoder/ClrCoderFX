﻿Get-ChildItem -Recurse -Include project.json | Rename-Item -NewName { $_.Name.replace(".json",".json.bak") }
Get-ChildItem -Recurse -Include project.lock.json | Rename-Item -NewName { $_.Name.replace("lock.json","lock.json.bak") }