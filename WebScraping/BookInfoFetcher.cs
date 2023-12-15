using HtmlAgilityPack;
using System.Text.RegularExpressions;

public class BookInfoFetcher
{
    // List to store book links
    private static List<string> bookLinks { get; set; } = new List<string>();
    /// <summary>
    /// Retrieves the HTML document from the specified link.
    /// </summary>
    /// <param name="url">The URL of the web page to fetch.</param>
    /// <returns>An HtmlDocument object representing the parsed HTML content.</returns>
    private static HtmlDocument GetHtmlDocument(string url)
    {
        // HtmlWeb instance for fetching HTML.
        HtmlWeb web = new HtmlWeb();

        try
        {
            // Download HTML from the website.
            HtmlDocument htmlDoc = web.Load(url);
            return htmlDoc;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel inträffade: {ex.Message}");
            // Return an empty HtmlDocument in case of an error.
            return new HtmlDocument();
        }
    }
    /// <summary>
    /// Retrieves a list of book links from the specified URL.
    /// </summary>
    /// <param name="url">The URL of the web page containing book links.</param>
    /// <returns>A list of book links extracted from the web page.</returns>
    public static List<string> GetLinks(string url)
    {
        // Get the HTML document for the specified URL.
        var htmlDoc = GetHtmlDocument(url);
        // Extract book links from the HTML document.
        // Use XPath or other methods to locate and extract the desired information.
        HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//h3/a");
        // Get the base URI for resolving relative links.
        var baseUri = new Uri(url);
        // Check if book links are found in the HTML document.
        if (nodes != null)
        {
            // Iterate through each node representing a book link.
            foreach (HtmlNode node in nodes)
            {
                // Extract the "href" attribute value, which is the book link.
                var link = node.Attributes["href"].Value;
                // Combine with the base URI to get the absolute book link.
                link = new Uri(baseUri, link).AbsoluteUri;
                // Add the absolute book link to the list.
                bookLinks.Add(link);
            }
        }
        else
        {
            Console.WriteLine("Ingen information hittad på sidan.");
        }
        return bookLinks;
    }
    /// <summary>
    /// Gets a list of BookInfo objects from a list of book links.
    /// </summary>
    /// <param name="links">List of book links to fetch information from.</param>
    /// <returns>List of BookInfo objects containing title and price information.</returns>
    public static List<BookInfo> GetBookInfos(List<string> links)
    {
        // Create a list to store BookInfo objects.
        var bookInfos = new List<BookInfo>();
        // Iterate through each book link.
        foreach (var link in links)
        {
            // Get the HTML document for the current book link.
            var doc = GetHtmlDocument(link);
            // Create a new BookInfo object.
            var book = new BookInfo();
            // Extract the title from the HTML document.
            book.Title = doc.DocumentNode.SelectSingleNode("//h1").InnerText;
            // Define the XPath for the price element.
            var pricePath = "//*[@class=\"col-sm-6 product_main\"]/*[@class=\"price_color\"]";
            // Extract the price as a string from the HTML document.
            var priceAsString = doc.DocumentNode.SelectSingleNode(pricePath).InnerText;
            // Convert the price string to a double using the ConvertPrice method.
            book.Price = ConvertPrice(priceAsString);
            // Add the BookInfo object to the list.
            bookInfos.Add(book);
        }
        return bookInfos;
    }
    /// <summary>
    /// Converts a string representation of a price to a double.
    /// </summary>
    /// <param name="priceAsString">The string representation of the price, e.g., "£42.96".</param>
    /// <returns>The converted price as a double. Returns -1 if the conversion fails.</returns>
    public static double ConvertPrice(string priceAsString)
    {
        // Define a regular expression pattern to match digits and the decimal point, the price (£42.96).
        var regularExpresstion = new Regex(@"[\d\.]+");
        // Attempt to find a match in the input string
        var match = regularExpresstion.Match(priceAsString);
        // If no match is found, return -1 indicating a conversion failure.
        if (!match.Success) return -1;
        // Convert the matched value to a double
        var price = double.Parse(match.Value);
        return price;
    }
}
