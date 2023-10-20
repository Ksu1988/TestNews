using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using System.Text.RegularExpressions;

namespace TestNews
{
    internal class News
    {
        internal ArticlesResult GetArticles(string q, SortBys sortBys, Languages lang, DateTime dateTime)
        {
            var apiKey = System.Configuration.ConfigurationManager.AppSettings["key"];
            var newsApiClient = new NewsApiClient(apiKey);
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = q,
                SortBy = sortBys,
                Language = lang,
                From = dateTime
            });
            var pattern =  new Regex("[^aeiouy]+", RegexOptions.IgnoreCase);
            if (lang== Languages.RU)
            {
                pattern = new Regex("[^уеёыаоэяию]+", RegexOptions.IgnoreCase);                
            }
            if (articlesResponse.Status == Statuses.Ok)
            {
                foreach (var article in articlesResponse.Articles)
                {
                    var words = Regex.Matches(article.Description, @"\w+").ToList();
                    var maxWord = "";
                    var maxVowelsCount = 0;
                    foreach (var word in words)
                    {
                        var vowelsMatches = pattern.Matches(word.Value);
                        if (vowelsMatches.Count() > maxVowelsCount)
                        {
                            maxWord = word.Value;
                            maxVowelsCount = vowelsMatches.Count();
                        }
                    }
                    Console.WriteLine($"{article.Description} - {maxWord}");
                }
            }
            else
            {
                Console.WriteLine(articlesResponse.Error);
            }
            return articlesResponse;
        }


    }
}
