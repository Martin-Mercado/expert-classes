
public class DatabaseExpert
{
    
    public static string MainConnectionString { get
        {
            return ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;
        }
    }

    public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null,  [CallerMemberName] string callerName = null)
    {
        Debug.WriteLine($"ExecuteQuery called from: {callerName}");
        

        DataTable dataTable = new DataTable();
        using (SqlConnection connection = new SqlConnection(MainConnectionString))
        {
            connection.Open();

            Debug.WriteLine("Executing SQL query:");
            Debug.WriteLine($"ConnectionString: {MainConnectionString}");
            Debug.WriteLine($"Query: {query}");

            if (parameters != null && parameters.Length > 0)
            { 
                Debug.WriteLine("Parameters:");
                foreach (var parameter in parameters)
                {
                    Debug.WriteLine($"  {parameter.ParameterName} = {parameter.Value}");
                }
            }

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
        }
        return dataTable;
    }

    public static void ExecuteNonQuery(string query, SqlParameter[] parameters = null, [CallerMemberName] string callerName = null)
    {

        Debug.WriteLine($"ExecuteNonQuery called from: {callerName}");

        using (SqlConnection connection = new SqlConnection(MainConnectionString))
        {
            connection.Open();

            Debug.WriteLine("Executing SQL NONquery:");
            Debug.WriteLine($"Query: {query}");

            if (parameters != null && parameters.Length > 0)
            {
                Debug.WriteLine("Parameters:");
                foreach (var parameter in parameters)
                {
                    Debug.WriteLine($"  {parameter.ParameterName} = {parameter.Value}");
                }
            }

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                command.ExecuteNonQuery();
            }
        }
    }
}
