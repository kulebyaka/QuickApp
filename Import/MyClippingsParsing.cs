using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using DAL.Models;

namespace Utils
{
    public static class MyClippingsParsing
    {
        private const string CurrentCulture = "ru-RU"; //TODO: Read from smth
        private const string AddedOnEn = "Added on ";
        private const string AddedOnRu = "Добавлено: ";
        private const string RuDateAndTimeDelimiter = " в ";

        public static List<BookmarkImport> GetBookmark(string myClippings)
        {
            var regex = new Regex(@"(?m)(?<Title>.+)(?<Author>\(.+\))(\n|\r|\r\n)(?<TypePage>.+?)\|(?<location>.+?)\|(?<Date>.+?(\n|\r|\r\n))(?<BookmarkValue>\n.*?\n)");
            var matches = regex.Matches(myClippings);

            var importBookmarks = new List<BookmarkImport>();
            foreach (Match match in matches)
            {
                var dateAndTime = match.Groups["Date"].Value.Replace(AddedOnRu, "").Split(RuDateAndTimeDelimiter, 2);
                importBookmarks.Add(
                    new BookmarkImport()
                    {
                        Title = match.Groups["Title"].Value, 
                        Author = match.Groups["Author"].Value,
                        TypePage = match.Groups["TypePage"].Value,
                        Date = DateTime.Parse(dateAndTime[0].Trim(), CultureInfo.GetCultureInfo(CurrentCulture)),
                        BookmarkValue = match.Groups["BookmarkValue"].Value,
                    }
                );
            }
            return importBookmarks;
        }
    }


    public class BookmarkImport
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string TypePage { get; set; }
        public DateTime? Date { get; set; }
        public string BookmarkValue { get; set; }
    }
    
}

