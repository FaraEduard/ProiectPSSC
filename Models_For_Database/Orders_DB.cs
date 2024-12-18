using System;

namespace ConsoleApp3.Models_For_Database
{
    public record Orders_DB
    (
        string ID,
        string NumeC_ustomer,
        DateTime Data,
        string Status,
        decimal Pret
    );
}
