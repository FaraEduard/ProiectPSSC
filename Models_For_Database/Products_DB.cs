using System;

namespace ConsoleApp3.Models_For_Database
{
    public record Products_DB
    (
        int ID,
        string Nume,
        decimal Pret,
        int Cantitate,
        string Order_Id
    );
}
