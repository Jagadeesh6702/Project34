using System.Data;
using System.Data.SqlClient;

namespace SqlServerQueries
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program p = new();
            p.Data();
            p.DataAdapter1();
        }

        public void Data()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=STLINDADM002\SQLEXPRESS;Initial Catalog=Database123;Integrated Security=True"))
                {

                    using (StreamReader reader = new StreamReader(@"C:\Users\sandhata\Desktop\Text1.txt"))
                    {
                        connection.Open();
                        Console.WriteLine("connection opened");
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            string[] values = line.Split(',');
                            string query = "insert into employee values(" + values[0] + ",'" + values[1] + "')";
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandText = query;
                            // cmd.CommandType = CommandType.Text;
                            cmd.Connection = connection;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    connection.Close();
                    Console.WriteLine("connection close");
                }
            }
            catch (IndexOutOfRangeException ie)
            {
                Console.WriteLine(ie.Message);
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.Message);
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                Console.WriteLine("ok... done..");
            }

        }
        public void DataAdapter1()
        {
            string connectionString = @"Data Source=STLINDADM002\SQLEXPRESS;Initial Catalog=Database123;Integrated Security=True";
            string query = "SELECT * FROM Villas";
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(dt);
                connection.Close();
            }

            using (StreamWriter writer = new StreamWriter(@"C:\Users\sandhata\Desktop\text2.txt"))
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    writer.Write(dt.Columns[i]);
                    if (i < dt.Columns.Count - 1)
                    {
                        writer.Write(",");
                    }
                }
                writer.WriteLine();

                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        writer.Write(row[i].ToString());
                        if (i < dt.Columns.Count - 1)
                        {
                            writer.Write(",");
                        }
                    }
                    writer.WriteLine();
                }
            }

            Console.WriteLine("Data exported successfully to text1.txt");
            Console.ReadKey();
        }
    }
}