using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ab03_Sanchez_C
{
    internal class Program
    {
        // Cadena de conexión a la base de datos SQL Server
        private static string connectionString = "Server=.\\SQLEXPRESS;Database=Tecsup2023DB;Integrated Security=true;";

        static void Main(string[] args)
        {
            // Llamar a la función que obtiene los estudiantes como un DataTable
            DataTable studentsDT = GetStudentsUsingDataTable();
            DisplayStudentsDataTable(studentsDT);

            // Llamar a la función que obtiene los estudiantes como una lista de objetos
            List<Student> studentsList = GetStudentsUsingObjectList();
            DisplayStudentsObjectList(studentsList);

            // Espera una tecla para cerrar la aplicación
            Console.ReadLine();
        }

        // Función 4: Obtener estudiantes como un DataTable
        public static DataTable GetStudentsUsingDataTable()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Students"; // Consulta SQL
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    dataAdapter.Fill(dt); // Llena el DataTable con los resultados
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los datos: " + ex.Message);
            }
            return dt;
        }

        // Función 5: Obtener estudiantes como una lista de objetos
        public static List<Student> GetStudentsUsingObjectList()
        {
            List<Student> students = new List<Student>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Students"; // Consulta SQL
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // Crear un nuevo objeto Student con los datos leídos
                        students.Add(new Student
                        {
                            ProductId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetInt32(2)
                        });
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los datos: " + ex.Message);
            }
            return students;
        }

        // Mostrar los estudiantes usando DataTable
        public static void DisplayStudentsDataTable(DataTable dt)
        {
            Console.WriteLine("Estudiantes desde DataTable:");
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"ID: {row["productid"]}, Name: {row["name"]}, Price: {row["price"]}");
            }
        }

        // Mostrar los estudiantes usando lista de objetos
        public static void DisplayStudentsObjectList(List<Student> students)
        {
            Console.WriteLine("\nEstudiantes desde lista de objetos:");
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.ProductId}, Name: {student.Name}, Price: {student.Price}");
            }
        }
    }

    // Clase para representar un estudiante
    public class Student
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
