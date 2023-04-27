using MauiFinal.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace MauiFinal
{
    public class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DBConnect()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "menu";
            uid = "root";
            password = "password";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);

        }



        public void InsertEmployee(Employee employee)
        {

            connection.Open();


            string sql = "INSERT INTO employee (Id, Name, Email) VALUES (@ID, @Name, @Email) ";

            
            MySqlCommand command = new MySqlCommand(sql, connection);


            command.Parameters.AddWithValue("@Id", employee.Id);
            command.Parameters.AddWithValue("@Name", employee.Name);
            command.Parameters.AddWithValue("@Email", employee.Email);



            command.ExecuteNonQuery();

            connection.Close();

        }

        public List<Employee> PopulateEmployee()
        {
            // Replace the connection details with your own MySQL server information
                 List<Employee> _employee = new List<Employee>();

                try
                {
                    connection.Open();

                   

                    string sql = "SELECT * FROM employee";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    
                    MySqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Email = reader.GetString("email")
                        };
                        _employee.Add(employee);

                    }
                    connection.Close();

                    
                }
                catch (MySqlException ex)
                {
                    // Handle any errors that occur during the database connection or query
                    Console.WriteLine("Error: " + ex.ToString());
                }


            

            return _employee;
        }



        public List<Dishes> PopulateDishes()
        {

            List<Dishes> _dishes = new List<Dishes>();
            // _foodDishes = _dbconnect.GetFoodDishes();
            // foodDishListView.ItemsSource = _foodDishes;


            try
            {
                connection.Open();

                string sql = "SELECT * from dish";

                MySqlCommand command = new MySqlCommand(sql, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Dishes dish = new Dishes
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name"),
                        Description = reader.GetString("Description"),
                        Price = reader.GetInt32("Price")
                    };


                    _dishes.Add(dish);

                }
                connection.Close();

            }
            catch(MySqlException ex) 
            {
                Console.WriteLine("Error: " + ex.ToString());
            }

            return _dishes;
        }
            





        public void UpdateDish(Dishes dish)
        {
            
            
                connection.Open();

                string sql = "UPDATE dish SET Name= @Name, Description= @Description, Price= @price WHERE Id=@Id";

                MySqlCommand command = new MySqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Id", dish.Id);
                command.Parameters.AddWithValue("@Name", dish.Name);
                command.Parameters.AddWithValue("@Description", dish.Description);
                command.Parameters.AddWithValue("@Price", dish.Price);

                if(dish.Price <= 0)
                {
                    throw new Exception("The price should be more that 0");
                }    

                command.ExecuteNonQuery();

                connection.Close();
            
                
            
        }

        public bool checkIdMatches(int idEntry)
        {

            List<Dishes> _dishesId = new List<Dishes>();

            connection.Open();

            string sql = "SELECT id from dish";

            MySqlCommand command = new MySqlCommand(sql, connection);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Dishes dish = new Dishes
                {
                    Id = reader.GetInt32("id"),
                };
                _dishesId.Add(dish);


            }
            connection.Close();

            foreach (Dishes dish in _dishesId)
            {
                if (dish.Id == idEntry)
                {
                    return true;
                }
            }
            return false;

        }

        public List<Events> populateEvents()
        {
            List<Events> _events = new List<Events>();



            try
            {
                connection.Open();
                string sql = "SELECT * from EVENT";

                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) 
                {
                    Events events = new Events
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name"),
                        Description = reader.GetString("Description"),
                        Capacity = reader.GetInt32("Capacity")
                    };
                    _events.Add(events);

                    
                }
                connection.Close() ;    
                
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error" + ex.ToString());
            }


            return _events;
        }



        public void DeleteEvent(int eventId)
        {

            try
            {
                connection.Open();

                string sql = "DELETE FROM event WHERE id = @id";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", eventId);
                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (MySqlException ex)
            {
                // Handle any errors that occur during the database connection or query
                Console.WriteLine("Error: " + ex.ToString());
            }
        }



    }
}
