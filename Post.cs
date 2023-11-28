namespace GuestBookSpace
{
    class Post
    {
        // Fält
        private string author = "";
        private string message = "";

        // Konstruktor
        public Post(string author, string message)
        {
            this.author = author;
            this.message = message;
        }
        // Egenskaper
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}