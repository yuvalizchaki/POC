# Development

## Database setup

### Run:
```sh
docker-compose up -d
```

### Redis Commander Url:
```
https://localhost:8081
```

### pgAdmin Url:

```
https://localhost:5050
```

### pgAdmin Credentials:

```
Username:   admin@example.com
Password:   admin
```

Postgres Url:

### Host name for pgAdmin:

```
host.docker.internal
```

### Postgres Credentials

```
Database:   mydatabase
Username:   user
Password:   password
```

### .NET migrate database:
Run under `POC\backend\POC\POC>`:
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```
