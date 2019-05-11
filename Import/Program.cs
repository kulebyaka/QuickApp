using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DAL.Models;
using Import.Metadata;
using Utils;

namespace Import
{
    class Program
     {
        const string EndOfBookmark = "==========";

        static void Main(string[] args)
        {
            var directoryPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.ToString(), "TestResources");
            Console.WriteLine(GetMetadata(directoryPath));
            Console.WriteLine(GetHighlights(directoryPath));
            Console.ReadLine();

            object o = Encoding.Unicode.CodePage; 
            Encoding enc = Encoding.GetEncoding(int.Parse(o.ToString()));
        }

        private static string GetMetadata(string directoryPath)
        {
            
            var x = Directory.GetFiles(directoryPath, "*.mobi");

            MobiMetadata mobi = new MobiMetadata(x.First());
            string fileName = mobi.PDBHeader.Name;
            uint fileVersion = mobi.MobiHeader.FileVersion;
            string fullname = mobi.MobiHeader.FullName;
            string authorName = mobi.MobiHeader.EXTHHeader.Author;
            string updatedTitle = mobi.MobiHeader.EXTHHeader.UpdatedTitle;
            var dontKnow = mobi.MobiHeader.EXTHHeader.fieldListNoBlankRows;
            var anotherField = mobi.PalmDocHeader.ToString();
            return String.Format("Title = {0}; Author = {1}", fullname, authorName);
        }

        private static string GetHighlights(string directoryPath)
        {
            var clipping = new StringBuilder();
            var allText = File.ReadAllText(Path.Combine(directoryPath, "My Clippings.txt"));
            var bookmarks = MyClippingsParsing.GetBookmark(allText).GroupBy(x => x.Title);
            foreach (var group in bookmarks)
            {
                var x = new Book()
                {
                    Title = group.Key,
                    Bookmarks = group.Select(a=>new Bookmark()
                    {
                        Value = a.BookmarkValue
                    }).ToList()
                };

            }
            return "";
        }
    }
}
