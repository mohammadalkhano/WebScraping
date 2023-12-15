internal class Program
{
    private static void Main(string[] args)
    {
        Console.Title = "Uppdrag 4: Planera djurparken och Internet";
        // URL för webbplatsen som ska vill skrapas
        string url = "https://books.toscrape.com/catalogue/category/books/science_22/index.html";
        // Get a list of book links from the specified URL.
        var links = BookInfoFetcher.GetLinks(url);
        // Get book information from the list of links.
        List<BookInfo> bookInfoList = BookInfoFetcher.GetBookInfos(links);
        // Display book information in the console.
        foreach (var book in bookInfoList)
        {
            Console.WriteLine($"Title: {book.Title}, Price: {book.Price}");
        }

    }
}
