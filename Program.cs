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

    private static readonly string _createStudentsCommand = File.ReadAllText("./Sql/Students/create_table.sql");
    private static readonly string _countStudentsCommand = File.ReadAllText("./Sql/Students/count.sql");
    private static readonly string _seedStudentsCommand = File.ReadAllText("./Sql/Students/seed.sql");
    private static readonly string _selectAllStudentsCommand = File.ReadAllText("./Sql/Students/select_all.sql");
    private static readonly string _createFunctionListAllStudentsNamedJohnCommand = File.ReadAllText("./Sql/Students/create_function_list_all_users_named_john.sql");
    private static readonly string _callFunctionListAllStudentsNamedJohnCommand = File.ReadAllText("./Sql/Students/call_list_all_students_named_john_function.sql");

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

          // Create server function which gets all students with name 'John'
          command.CommandText = _createFunctionListAllStudentsNamedJohnCommand;
          command.ExecuteNonQuery();

          // Select all students with name 'John' from database using stored function
          command.CommandText = _callFunctionListAllStudentsNamedJohnCommand;
          using (NpgsqlDataReader reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              students.Add(new Student
              {
                Id         = (long)   reader["id"],
                FirstName  = (string) reader["first_name"],
                LastName   = (string) reader["last_name"],
                Email      = (string) reader["email"]
              });
            }
          }
        }
      }

      students.ForEach(s => Console.WriteLine(s.ToJson()));
    }
  }
}
