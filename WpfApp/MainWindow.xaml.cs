using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        // Cadena de conexión a la base de datos SQL Server
        private static string connectionString = "Server=.\\SQLEXPRESS;Database=Tecsup2023DB;Integrated Security=true;TrustServerCertificate=true;";


        public MainWindow()
        {
            InitializeComponent();
        }

        // Función que se llama al hacer clic en el botón de búsqueda
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = searchTextBox.Text;
            var students = GetStudentsUsingObjectList(searchTerm); // Obtener los estudiantes filtrados por nombre
            studentsDataGrid.ItemsSource = students; // Mostrar los estudiantes en el DataGrid
        }

        // Obtener estudiantes desde la base de datos como una lista de objetos (filtrados por nombre)
        public List<Student> GetStudentsUsingObjectList(string searchTerm)
        {
            List<Student> students = new List<Student>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Students WHERE name LIKE @searchTerm"; // Consulta con filtro
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%"); // Parámetro para la búsqueda

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
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
                MessageBox.Show("Error al obtener los datos: " + ex.Message);
            }
            return students;
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
