using HtmlAgilityPack;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        // URL för webbplatsen som ska vill skrapas
        string url = "https://books.toscrape.com/catalogue/category/books/science_22/index.html";
        var links = BooksToScrape.GetBooks(url);
        var bookInfoList = new List<BookInfo>();
        foreach (var link in links)
        {
            Console.WriteLine($"Link: {link}");
        }

    }
}
public class BooksToScrape
{
    private static List<string> bookLinks { get; set; } = new List<string>();
    private static HtmlDocument GetHtmlDocument(string url)
    {
        // Skapa en ny HtmlWeb-instans
        HtmlWeb web = new HtmlWeb();

        try
        {
            // Ladda ner HTML från webbplatsen
            HtmlDocument htmlDoc = web.Load(url);
            return htmlDoc;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel inträffade: {ex.Message}");
            return null;
        }
    }
    public static List<string> GetBooks(string url)
    {
        var htmlDoc = GetHtmlDocument(url);
        // Extrahera information från HTML-dokumentet
        // Använd XPath eller andra metoder för att lokalisera och extrahera önskad information
        HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//h3/a");

        var baseUri = new Uri(url);
        if (nodes != null)
        {
            foreach (HtmlNode node in nodes)
            {
                var link = node.Attributes["href"].Value;
                link = new Uri(baseUri, link).AbsolutePath;
                bookLinks.Add(link);
            }
        }
        else
        {
            Console.WriteLine("Ingen information hittad på sidan.");
        }
        return bookLinks;
    }
    public static List<BookInfo> GetBookInfos()
    {
        throw new NotImplementedException();
    }
}
public class BookInfo
{
    public string? Title { get; set; }
    public string? Price { get; set; }
    public string? Description { get; set; }
}