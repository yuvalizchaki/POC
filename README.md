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


### CRM hard coded token expired after 1 year:
```
eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDUk0iLCJDb21wYW55SWQiOiIxIiwiZXhwIjoxNzUxNDkwNjg1LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM0NC8iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDM0NC8ifQ.L9fKPfV8TuYQhEaNGzqlm5wwku6G8jMykccl1mPcpec
```
## System Admin Credentials:
username: admin
password: admin

### To add admin to database run:
INSERT INTO public."Admins"("Username", "HashedPassword", "CompanyId")
VALUES ('admin', '�iv�A���M�߱g��s�K��o*�H�', '1');
