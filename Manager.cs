using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks.Dataflow;
using static System.Console;

namespace GuestBookSpace
{
    class Manager
    {
        // Fält
        private GuestBook? guestBook;
        private string filePath = @"posts.json";
        private string? jsonString;
        // Egenskaper
        public Manager()
        {
            // Skapar en ny instans av objektet GuestBook
            guestBook = new GuestBook();
            
            // Kontrollerar att filen exisisterar
            if (File.Exists(filePath))
            {
                // Skapar en ny instans av FileInfo
                FileInfo fileInfo = new FileInfo(filePath);

                // Kontrollerar att filen inte är tom
                if (fileInfo.Length != 0)
                {
                    // Konverterar json-filen till en sträng och sparar den
                    jsonString = File.ReadAllText(filePath);

                    // Lagrar datan från json-filen som Post-instanser i guestBook
                    guestBook = JsonSerializer.Deserialize<GuestBook>(jsonString);
                }
            }
        }
        public GuestBook GuestBook
        {
            get
            {
                if (guestBook == null)
                {
                    guestBook = new GuestBook();
                }
                return guestBook;
            }
        }
        public string AddPost(string? author, string? message)
        {
            // Kontrollerar att det inte skickats tomma strängar/null-värden
            if (string.IsNullOrEmpty(author) || string.IsNullOrEmpty(message))
            {
                return "Felmeddelande: Otillräcklig information, både namn och meddelande måste fyllas i. Inget inlägg har sparats.";
            }
            else
            {
                // Skapar ett nytt inlägg
                Post post = new Post(author, message);

                // Lägger till inlägget i guestBook
                guestBook.Add(post);

                // Uppdaterar json-filen
                UpdateJSON();

                return "Inlägget är sparat!";
            }
        }
        public string DeletePost(string? index)
        {
            // Kontrollerar att det inte skickats tomma strängar/null-värden
            if (string.IsNullOrEmpty(index))
            {
                return "Felmeddelande: Otillräcklig information, inlägget från index måste fyllas i. Inget inlägg har raderats.";
            }
            else
            {
                int parsedIndex;
                // Kontrollerar att index går att konvertera till en integer
                if (int.TryParse(index, out parsedIndex))
                {
                    // Kontrollerar att i lägget finns
                    if (parsedIndex >= 0 && parsedIndex < guestBook.Count)
                    {
                        // Raderar inlägget från guestBook
                        guestBook.RemoveAt(parsedIndex);

                        //Uppdaterar json-filen
                        UpdateJSON();

                        return "Inlägget är raderat!";
                    }
                    else
                    {
                        return $"Det finns ingen inlägg med index {parsedIndex}.";
                    }
                }
                else
                {
                    return "Felmeddelande: Olämpligt värde, index kan endast innehåll siffror.";
                }
            }
        }
        private void UpdateJSON()
        {
            // Konverterar guestBook till en json-sträng
            jsonString = JsonSerializer.Serialize(guestBook);

            // Skriver över existerande fil med den nya json-strängen
            File.WriteAllText(filePath, jsonString);
        }
    }
}