using Microsoft.Data.Sqlite;

try
{
    using var connection = new SqliteConnection("Data Source=Games.Db");
    connection.Open();

    Console.WriteLine("SQLite connection successful!");
}
catch (Exception ex)
{
    Console.WriteLine("SQLite failed:");
    Console.WriteLine(ex.ToString());
}