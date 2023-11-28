using System;
using System.Runtime.CompilerServices;
using static System.Console;

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

        static void Main(string[] args)
        {
            // Instanser
            Manager manager = new Manager();

            // Skriver ut menyn
            WriteLine("AXELS GÄSTBOK");
            WriteLine();
            WriteLine("1. Skriv nytt inlägg.");
            WriteLine("2. Ta bort inlägg.");
            WriteLine();
            WriteLine("X. Avsluta");
            WriteLine();
            PrintPosts(manager); // Skriver ut alla inlägg
            WriteLine("");

            bool success = false;
            while (success == false)
            {
                WriteLine("Välj ett alternativ genom att skriva en karaktär + enter.");

                // Läser av input
                string? input = ReadLine();
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
                        success = true;
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
                        success = true;
                        break;
                    case "X":
                        Clear();
                        WriteLine("Programmet avslutas, hej då!");
                        success = true;
                        break;
                    default:
                        WriteLine("Felmeddelande: Karaktären motsvarar inte ett alternativ.");
                        break;
                }
            }
        }
    }
}