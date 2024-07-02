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

## System Admin Credentials:
username: admin
password: admin

### To add admin to database run:
INSERT INTO public."Admins"("Username", "HashedPassword", "CompanyId")
VALUES ('admin', '�iv�A���M�߱g��s�K��o*�H�', '1');
