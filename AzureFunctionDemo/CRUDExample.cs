using System;
using System.Data.SqlClient;

namespace CRUDExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(local);Initial Catalog=ExampleDB;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            // Create
            string insertQuery = "INSERT INTO Customers (Name, Email, Phone) VALUES (@Name, @Email, @Phone)";
            SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
            insertCommand.Parameters.AddWithValue("@Name", "John Doe");
            insertCommand.Parameters.AddWithValue("@Email", "johndoe@example.com");
            insertCommand.Parameters.AddWithValue("@Phone", "555-555-5555");

            connection.Open();
            int insertedRows = insertCommand.ExecuteNonQuery();
            Console.WriteLine("Inserted {0} rows.", insertedRows);
            connection.Close();

            // Read
            string selectQuery = "SELECT * FROM Customers";
            SqlCommand selectCommand = new SqlCommand(selectQuery, connection);

            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("{0}\t{1}\t{2}", reader["Name"], reader["Email"], reader["Phone"]);
            }

            reader.Close();
            connection.Close();

            // Update
            string updateQuery = "UPDATE Customers SET Phone = @Phone WHERE Email = @Email";
            SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
            updateCommand.Parameters.AddWithValue("@Phone", "555-555-1234");
            updateCommand.Parameters.AddWithValue("@Email", "johndoe@example.com");

            connection.Open();
            int updatedRows = updateCommand.ExecuteNonQuery();
            Console.WriteLine("Updated {0} rows.", updatedRows);
            connection.Close();

            // Delete
            string deleteQuery = "DELETE FROM Customers WHERE Name = @Name";
            SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
            deleteCommand.Parameters.AddWithValue("@Name", "John Doe");

            connection.Open();
            int deletedRows = deleteCommand.ExecuteNonQuery();
            Console.WriteLine("Deleted {0} rows.", deletedRows);
            connection.Close();
        }
    }
}