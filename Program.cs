using System;
using System.IO;
using System.Collections.Generic;
using Npgsql;
using PostgresTest.Models;

namespace PostgresTest
{
  public static class Program
  {
    private const string _connectionString = "Host=localhost;Database=dotnet_postgres_test";
    private const string _studentsTableName = "students";
    private const string _getAllJohnsProcedureName = "all_johns";

    private static readonly string _createStudentsCommand          = File.ReadAllText("./Sql/create_students.sql");
    private static readonly string _countStudentsCommand           = File.ReadAllText("./Sql/count_students.sql");
    private static readonly string _seedStudentsCommand            = File.ReadAllText("./Sql/seed_students.sql");
    private static readonly string _selectAllStudentsCommand       = File.ReadAllText("./Sql/select_all_students.sql");

    public static void Main(string[] args)
    {
      List<Student> students = new List<Student>();

      using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
      {
        connection.Open();

        using (NpgsqlCommand command = new NpgsqlCommand())
        {
          command.Connection = connection;

          // Create students table if not exists
          command.CommandText = _createStudentsCommand;
          command.ExecuteNonQuery();

          // Check if the table is empty
          command.CommandText = _countStudentsCommand;
          bool tableIsEmpty = ((long) command.ExecuteScalar()) == 0;

          // Seed table if it is empty
          if (tableIsEmpty)
          {
            command.CommandText = _seedStudentsCommand;
            command.ExecuteNonQuery();
          }

          // Select all students from database
          command.CommandText = _selectAllStudentsCommand;
          using (NpgsqlDataReader reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              students.Add(new Student
              {
                Id    = (long)   reader["id"],
                Name  = (string) reader["name"],
                Email = (string) reader["email"]
              });
            }
          }
        }
      }

      students.ForEach(s => Console.WriteLine(s));
    }
  }
}
