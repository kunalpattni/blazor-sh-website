namespace BlazorShWebsite.Server.Services;

using SitemapUrl = (string loc, DateOnly lastmod);


public static class Sitemap
{
    public static string Generate()
    {
        var urls = new List<SitemapUrl>
        {
            ("/", new DateOnly(2025, 10, 8)),
            ("/ball-animation", new DateOnly(2025, 10, 9)),
            ("/calculator", new DateOnly(2025, 10, 11)),
            ("/local-storage", new DateOnly(2025, 10, 11))
        };
        
        return $"""
                <?xml version="1.0" encoding="UTF-8"?>
                <urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
                {string.Join("\n", urls.Select(Url).ToList())}
                </urlset>
                """;
    }

    public static string Url(SitemapUrl url)
    {
        const string domain = "https://blazor.sh";
        var fqdn = $"{domain}{url.loc}";
        var dateString = url.lastmod.ToString("yyyy-MM-dd");
        return $"""
                <url>
                <loc>{fqdn}</loc>
                <lastmod>{dateString}</lastmod>
                </url>
                """;
    }
}