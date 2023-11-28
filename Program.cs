using System;
using System.Runtime.CompilerServices;
using static System.Console;
using System.Threading;

namespace GuestBookSpace
{
    class Program
    {
        static void PrintPosts(Manager manager)
        {
            // Hämtar guestBook
            GuestBook guestBook = manager.GuestBook;

            // Kollar ifall guestbook har inlägg
            if (guestBook.Count == 0)
            {
                WriteLine("Det finns inga inlägg.");
            }
            else
            {
                // Skriver ut att inlägg till konsolen
                for (int i = 0; i < guestBook.Count; i++)
                {
                    Post post = guestBook[i];
                    WriteLine($"[{i}] {post.Author} - {post.Message}");
                }
            }
        }

        static void PrintMenu(Manager manager)
        {
            WriteLine("AXELS GÄSTBOK");
            WriteLine();
            WriteLine("1. Skriv nytt inlägg.");
            WriteLine("2. Ta bort inlägg.");
            WriteLine();
            WriteLine("X. Avsluta");
            WriteLine();
            PrintPosts(manager); // Skriver ut alla inlägg
            WriteLine("");
            WriteLine("Välj ett alternativ genom att skriva en karaktär + enter.");

        }

        static bool manageInput(string? input, Manager manager)
        {
            string feedback;

            switch (input)
            {
                case "1":
                    Clear();
                    WriteLine("SKRIV NYTT INLÄGG");
                    WriteLine();
                    WriteLine("Namn:");
                    string? author = ReadLine();
                    WriteLine("Meddelande:");
                    string? message = ReadLine();
                    feedback = manager.AddPost(author, message);
                    Clear();
                    WriteLine(feedback);
                    break;
                case "2":
                    Clear();
                    WriteLine("TA BORT INLÄGG");
                    WriteLine();
                    PrintPosts(manager);
                    WriteLine("");
                    WriteLine("Radera inlägg genom att skriva in dess index + enter.");
                    WriteLine("Index:");
                    string? index = ReadLine();
                    feedback = manager.DeletePost(index);
                    Clear();
                    WriteLine(feedback);
                    break;
                case "X":
                    Clear();
                    WriteLine("Programmet avslutas, hej då!");
                    return true;
                default:
                    Clear();
                    WriteLine("Felmeddelande: Karaktären motsvarar inte ett alternativ.");
                    break;
            }
            return false;
        }

        static void Main(string[] args)
        {
            // Instanser
            Manager manager = new Manager();

            // Skapar en do-while-loop som exekveras så länge succes == false
            bool success = false;
            do
            {
                // Rensar konsolen
                Clear();

                // Skriver ut menyn
                PrintMenu(manager);

                // Läser av input
                string? input = ReadLine();

                // Hanterar input
                success = manageInput(input, manager);

                // Ger möjlighet att läsa meddelandet genom en paus
                Thread.Sleep(2000);
            }
            while (success == false);
        }
    }
}