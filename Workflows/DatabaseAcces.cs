using System;
using Microsoft.Data.SqlClient;
using ConsoleApp3.Models_For_Database;
using ProiectPSSC;
using Examples.Domain.Models;
using Examples.Domain.Operations;

namespace ShoppingCartApp
{
    public class DatabaseAccess
    {
        private static readonly string connectionString = @"Server=serverpssc.database.windows.net;Database=PSSC_DB;User Id=serverpssc;Password=PassPass!;";

        public static List<Prices_DB> GetPrices()
        {
            List<Prices_DB> Prices_DB = new List<Prices_DB>();
            string query = "SELECT Id, Nume, Pret FROM Prices";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Conexiune reușită!");

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string nume = reader.GetString(1);
                                decimal pret = reader.GetDecimal(2);

                                Prices_DB.Add(new Prices_DB(id,nume,pret));

                            }
                        }
                    }

                    return Prices_DB;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Eroare: " + ex.Message);
                }
                return Prices_DB;
            }
        }

        public static List<Orders_DB> GetOrders()
        {
            List<Orders_DB> Orders_DB = new List<Orders_DB>();
            string query = "SELECT Id, Nume_Customer, Data, Status, Pret FROM Orders";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Conexiune reușită!");

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string id = reader.GetString(0);
                                string numeCustomer = reader.GetString(1);
                                DateTime data = reader.GetDateTime(2);
                                string status = reader.GetString(3);
                                decimal pret = reader.GetDecimal(4);

                                Orders_DB.Add(new Orders_DB(id, numeCustomer, data, status, pret));
                            }
                        }
                    }

                    return Orders_DB;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Eroare: " + ex.Message);
                }
                return Orders_DB;
            }
        }

        public static List<Products_DB> GetProducts()
        {
            List<Products_DB> Products_DB = new List<Products_DB>();
            string query = "SELECT Id, Nume, Pret, Cantitate, Order_Id FROM Produse";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Conexiune reușită!");

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string nume = reader.GetString(1);
                                decimal pret = reader.GetDecimal(2);
                                int cantitate = reader.GetInt32(3);
                                string orderId = reader.GetString(4);

                                Products_DB.Add(new Products_DB(id, nume, pret, cantitate, orderId));
                            }
                        }
                    }

                    return Products_DB;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Eroare: " + ex.Message);
                }
                return Products_DB;
            }
        }



         // Metodă pentru inserare în tabela Orders
        public static void InsertOrder(string Id, string Nume_Customer, DateTime Data, string Status, decimal Pret)
        {
            string insertQuery = "INSERT INTO Orders (Id, Nume_Customer, Data, Status, Pret) VALUES (@Id, @Nume_Customer, @Data, @Status, @Pret)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Adăugare parametri pentru prevenirea SQL Injection
                        command.Parameters.AddWithValue("@Id", Id);
                        command.Parameters.AddWithValue("@Nume_Customer", Nume_Customer);
                        command.Parameters.AddWithValue("@Data", Data);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@Pret", Pret);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Inserare realizată cu succes in Orders!");
                        }
                        else
                        {
                            Console.WriteLine("Inserarea a eșuat.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Eroare la inserare: " + ex.Message);
                }
            }
        }

        public static void RemoveOrder(string Id)
        {
            string deleteQuery = "DELETE FROM Orders WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        // Adăugare parametru pentru prevenirea SQL Injection
                        command.Parameters.AddWithValue("@Id", Id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Order cu ID-ul '{Id}' a fost șters cu succes!");
                        }
                        else
                        {
                            Console.WriteLine($"Order cu ID-ul '{Id}' nu a fost găsit.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Eroare la ștergere: " + ex.Message);
                }
            }
        }

        public static void UpdateOrderStatusToCanceled(string Id)
        {
            string updateQuery = "UPDATE Orders SET Status = @Status WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        // Adăugare parametri pentru prevenirea SQL Injection
                        command.Parameters.AddWithValue("@Status", "Canceled");
                        command.Parameters.AddWithValue("@Id", Id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Statusul comenzii cu ID-ul '{Id}' a fost actualizat la 'Canceled'!");
                        }
                        else
                        {
                            Console.WriteLine($"Comanda cu ID-ul '{Id}' nu a fost găsită.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Eroare la actualizarea statusului: " + ex.Message);
                }
            }
        }



         // Metodă pentru inserare în tabela Products
        public static void InsertProduct(int Id, string Nume, decimal Pret, int Cantitate, string Order_Id)
        {
            string insertQuery = "INSERT INTO Produse (Id, Nume, Pret, Cantitate, Order_Id) VALUES (@Id, @Nume, @Pret, @Cantitate, @Order_Id)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Adăugare parametri pentru siguranță
                        command.Parameters.AddWithValue("@Id", Id);
                        command.Parameters.AddWithValue("@Nume", Nume);
                        command.Parameters.AddWithValue("@Pret", Pret);
                        command.Parameters.AddWithValue("@Cantitate", Cantitate);
                        command.Parameters.AddWithValue("@Order_Id", Order_Id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Inserare în Produse realizată cu succes!");
                        }
                        else
                        {
                            Console.WriteLine("Inserarea în Produse a eșuat.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Eroare la inserarea în Produse: " + ex.Message);
                }
            }
        }

        public static void RemoveProductsByOrderId(string orderId)
        {
            string deleteQuery = "DELETE FROM Products WHERE Order_Id = @Order_Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        // Adăugare parametru pentru prevenirea SQL Injection
                        command.Parameters.AddWithValue("@Order_Id", orderId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Toate produsele asociate cu Order_Id-ul '{orderId}' au fost șterse cu succes!");
                        }
                        else
                        {
                            Console.WriteLine($"Nu au fost găsite produse asociate cu Order_Id-ul '{orderId}'.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Eroare la ștergerea produselor: " + ex.Message);
                }
            }
        }

        public static void RemoveProductByNameAndOrderId(string productName, string orderId)
        {
            string deleteQuery = "DELETE FROM Produse WHERE Nume = @Nume AND Order_Id = @Order_Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        // Adăugare parametri pentru prevenirea SQL Injection
                        command.Parameters.AddWithValue("@Nume", productName);
                        command.Parameters.AddWithValue("@Order_Id", orderId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Produsul '{productName}' asociat cu Order_Id-ul '{orderId}' a fost șters cu succes!");
                        }
                        else
                        {
                            Console.WriteLine($"Produsul '{productName}' cu Order_Id-ul '{orderId}' nu a fost găsit.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Eroare la ștergerea produsului: " + ex.Message);
                }
            }
        }

        public static void CancelOrderIfPriceIsZero(string orderId)
        {
            string selectQuery = "SELECT Pret FROM Orders WHERE Id = @Order_Id";
            string updateQuery = "UPDATE Orders SET Status = @Status WHERE Id = @Order_Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    decimal pret = 0;

                    // Verificare preț
                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@Order_Id", orderId);

                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                pret = reader.GetDecimal(0);
                            }
                        }
                    }

                    // Dacă prețul este 0, actualizează statusul la "Canceled"
                    if (pret == 0)
                    {
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@Status", "Canceled");
                            updateCommand.Parameters.AddWithValue("@Order_Id", orderId);

                            int rowsAffected = updateCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine($"Comanda cu Order_Id '{orderId}' a fost anulată deoarece prețul este 0.");
                            }
                            else
                            {
                                Console.WriteLine($"Nu s-a putut actualiza statusul comenzii cu Order_Id '{orderId}'.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Prețul comenzii cu Order_Id '{orderId}' nu este 0. Statusul nu a fost modificat.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Eroare: " + ex.Message);
                }
            }
        }


        public static void AddProductInOrder(string orderId)
        {
            Console.Write("Numele produsului: ");
            string name = Console.ReadLine() ?? "N/A";

            // Cautam Id si pret dupa numele produsului.
            try
            {
                int i=0;
                foreach(var element in Program.Prices_DB)
                {
                    if(element.Nume == name)
                    {
                        string priceInput = element.Pret.ToString();
                        int Id = element.Id;

                        Console.Write("Cantitatea produsului dorit: ");
                        string stockInput = Console.ReadLine() ?? "N/A";
                        int stockk = Convert.ToInt32(stockInput);

                        var unvalidatedProduct = new Product.UnvalidatedProduct(name, priceInput, stockInput);
                        unvalidatedProduct.Id = Id;

                        var validationService = new ProductValidationService();
                        var validatedProduct = validationService.ValidateProduct(unvalidatedProduct);

                        Console.WriteLine($"- {unvalidatedProduct.Id} | {unvalidatedProduct.Name} | Pret: {unvalidatedProduct.Price:0.##} | Cantitate: {unvalidatedProduct.Stock}");

                        string insertQuery = "INSERT INTO Produse (Id, Nume, Pret, Cantitate, Order_Id) VALUES (@ID, @Nume, @Pret, @Cantitate, @Order_Id)";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();

                                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                                {
                                    // Adăugare parametri pentru prevenirea SQL Injection
                                    command.Parameters.AddWithValue("@Id", Id);
                                    command.Parameters.AddWithValue("@Nume", name);
                                    command.Parameters.AddWithValue("@Pret", element.Pret);
                                    command.Parameters.AddWithValue("@Cantitate", stockk);
                                    command.Parameters.AddWithValue("@Order_Id", orderId);

                                    int rowsAffected = command.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        Console.WriteLine($"Produsul '{unvalidatedProduct.Name}' a fost adăugat cu succes!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Adăugarea produsului a eșuat.");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Eroare la adăugarea produsului: " + ex.Message);
                            }
                        }

                        i=10;
                        break;
                        }
                    }
                    if(i != 10)
                    throw new Exception("Produsul nu exista.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Eroare la adaugarea produsului: {ex.Message}");
                }

        }


        public static void UpdateOrderTotal(string orderId)
        {
            string selectQuery = "SELECT Cantitate, Pret FROM Produse WHERE Order_Id = @Order_Id";
            string updateQuery = "UPDATE Orders SET Pret = @Total WHERE Id = @Order_Id";

            decimal total = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Preluare produse asociate Order_Id
                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@Order_Id", orderId);

                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int cantitate = reader.GetInt32(0);
                                decimal pret = reader.GetDecimal(1);

                                // Calcularea valorii produsului și adăugarea la total
                                total += cantitate * pret;
                            }
                        }
                    }

                    // Actualizare valoare totală în tabelul Orders
                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@Total", total);
                        updateCommand.Parameters.AddWithValue("@Order_Id", orderId);

                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Valoarea totală a comenzii cu Order_Id '{orderId}' a fost actualizată la {total:C}!");
                        }
                        else
                        {
                            Console.WriteLine($"Nu s-a putut actualiza valoarea totală a comenzii cu Order_Id '{orderId}'.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Eroare: " + ex.Message);
                }
            }
        }


    }
}
