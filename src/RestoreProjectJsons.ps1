﻿Get-ChildItem -Recurse -Include project.json.bak | Rename-Item -NewName { $_.Name.replace(".json.bak",".json") }
Get-ChildItem -Recurse -Include project.lock.json.bak | Rename-Item -NewName { $_.Name.replace(".lock.json.bak",".lock.json") }