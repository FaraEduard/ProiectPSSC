using ShoppingCartApp;
using Azure.Messaging.ServiceBus;
using Microsoft.Data.SqlClient;

namespace ProiectPSSC
{
    class Program
    {
        static void Main(string[] args)
        {
            //Rulare Workflow 1 - Plasare comanda
            // Inițiem și apelăm aplicația PlasareComanda
            //var plasareComanda = new PlasareComanda();
            //plasareComanda.Start();



        }


        //Teste pentru conexiunea la Service Bus
        static async Task Main()
        {
            string connectionString = "<Azure Service Bus Connection String>";
            string queueName = "<Queue Name>";  // Poți utiliza un topic sau o queue, în funcție de cum ai configurat Service Bus-ul

            // Crează un client ServiceBus
            var client = new ServiceBusClient(connectionString);
            var receiver = client.CreateReceiver(queueName);

            Console.WriteLine("Aștept mesaje...");

            // Ascultă mesajele din queue
            await foreach (ServiceBusReceivedMessage message in receiver.ReceiveMessagesAsync())
            {
                string body = message.Body.ToString();
                Console.WriteLine($"Mesaj primit: {body}");

                // Poți adăuga logică aici pentru a prelucra mesajele (de exemplu, să interacționezi cu baza de date)
            }
        }

        async Task GetDataFromDatabase()
        {
            string connectionString = "<Azure SQL Database Connection String>";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                
                string query = "SELECT * FROM YourTable";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Console.WriteLine($"Data: {reader[0]}");
                }
            }
        }
    }
}

