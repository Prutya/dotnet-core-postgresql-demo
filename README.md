# .NET Core Npgsql demo
## Prerequisites
You should have [.NET Core](https://www.microsoft.com/net/core) and [PostgreSQL](https://www.postgresql.org) installed.
## Create database
```
$ createdb dotnet_postgres_test
```
or
```
$ psql
```
```sql
CREATE DATABASE dotnet_postgres_test;
```
## Launch demo
```
$ dotnet restore
$ dotnet run
```
## Expected Result
```json
{"FirstName":"John","LastName":"Doe","Email":"john.doe@example.com","Id":1}
{"FirstName":"John","LastName":"Shepard","Email":"john.shepard@example.com","Id":2}
{"FirstName":"John","LastName":"Snow","Email":"john.snow@example.com","Id":3}
```
