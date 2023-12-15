internal class Program
{
    private static void Main(string[] args)
    {
        Console.Title = "Uppdrag 4: Planera djurparken och Internet";
        // URL för webbplatsen som ska vill skrapas
        string url = "https://books.toscrape.com/catalogue/category/books/science_22/index.html";
        var links = BookInfoFetcher.GetLinks(url);
        List<BookInfo> bookInfoList = BookInfoFetcher.GetBookInfos(links);
        foreach (var book in bookInfoList)
        {
            Console.WriteLine($"Title: {book.Title}, Price: {book.Price}");
        }

    }
}
