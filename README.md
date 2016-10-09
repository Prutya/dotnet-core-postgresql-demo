# .NET Core Npgsql demo
## Prerequisites
You should have [.NET Core](https://www.microsoft.com/net/core) and [PostgresSQL](https://www.postgresql.org) installed.
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
