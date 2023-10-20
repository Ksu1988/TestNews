
//Your API key is: 2f1fef8150be40c9aedfa9303c561f31
using NewsAPI.Constants;
using TestNews;

Console.OutputEncoding = System.Text.Encoding.UTF8;
var newsApi = new News();
newsApi.GetArticles("Космос", SortBys.Popularity, Languages.RU, new DateTime(2023, 09, 25));




