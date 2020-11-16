using Business.BusinessObjects;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Business.Helpers
{
    public static class StringHelper
    {
        public static void ClearDescriptions(List<DescriptionBO> descriptions)
        {
            foreach (var desc in descriptions)
            {
                desc.LongDescription = RemoveUnwantedTags(RemoveNewLines(desc.LongDescription));
                desc.ShortDescription = RemoveUnwantedTags(RemoveNewLines(desc.ShortDescription));
            }
        }
        public static void RemoveYoutubeLink(List<string> links)
        {
            for (int i = links.Count - 1; i >= 0; i--)
            {
                if (IsYoutubeLink(links[i]))
                    links.RemoveAt(i);
            }
        }

        private static bool IsYoutubeLink(string data)
        {
            return data.Contains("youtube.com");
        }
        private static string RemoveUnwantedTags(string data)
        {
            if (string.IsNullOrEmpty(data)) return string.Empty;

            var document = new HtmlDocument();
            document.LoadHtml(data);

            var acceptableTags = new String[] {  };

            var nodes = new Queue<HtmlNode>(document.DocumentNode.SelectNodes("./*|./text()"));
            while (nodes.Count > 0)
            {
                var node = nodes.Dequeue();
                var parentNode = node.ParentNode;

                if (!acceptableTags.Contains(node.Name) && node.Name != "#text")
                {
                    var childNodes = node.SelectNodes("./*|./text()");

                    if (childNodes != null)
                    {
                        foreach (var child in childNodes)
                        {
                            nodes.Enqueue(child);
                            parentNode.InsertBefore(child, node);
                        }
                    }
                    parentNode.RemoveChild(node);
                }
            }
            //Removing HTML entities
            return HttpUtility.HtmlDecode(document.DocumentNode.InnerHtml);
        }
        private static string RemoveNewLines(string data)
        {
            return data.Replace("\n", "").Replace("\r", "");
        }
    }
}
