Run:
```ps1
puml-gen ..\POC\ .\POC\ -dir -excludePaths bin,obj,Properties
```

Modify `input.txt`

Run:
```ps1
python .\create_roots.py -i .\input.txt -o ./output -s .\POC\ -r .\plantuml-1.2024.3.jar
```