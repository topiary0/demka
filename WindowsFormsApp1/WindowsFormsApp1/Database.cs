using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public static class Database
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["WindowsFormsApp1.Properties.Settings.user10ConnectionString"].ConnectionString;

        public static DataTable Query(string sql, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public static int Execute(string sql, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
}
