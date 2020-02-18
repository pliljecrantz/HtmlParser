using HtmlAgilityPack;
using System;
using System.IO;
using System.Net;

namespace HtmlParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //CopyHtmlFiles();
            //RemoveSrcSet();
            //ListFeedFolders();
            //DeleteFeedFolders();
            //ListIndex2Files();
            //DeleteIndex2Files();
            Console.Read();
        }

        private static void DeleteIndex2Files()
        {
            string[] filePaths = Directory.GetFiles(@"C:\Temp\LEN\LivetEfterNoa\wwwlivetefternoase\", "index2.html", SearchOption.AllDirectories);
            foreach (var file in filePaths)
            {
                File.Delete(file);
            }

            Console.WriteLine("DeleteIndex2Files() done");
        }

        private static void ListIndex2Files()
        {
            string[] filePaths = Directory.GetFiles(@"C:\Temp\LEN\LivetEfterNoa\wwwlivetefternoase\", "index2.html", SearchOption.AllDirectories);

            foreach (var file in filePaths)
            {
                if (file.Contains("index2"))
                {
                    Console.WriteLine(file);
                }
            }

            Console.WriteLine("Number of index2.html files: " + filePaths.Length);
        }

        private static void DeleteFeedFolders()
        {
            string[] folderPaths = Directory.GetDirectories(@"C:\Temp\LEN\LivetEfterNoa\wwwlivetefternoase\", "feed", SearchOption.AllDirectories);
            foreach (var folderPath in folderPaths)
            {
                Directory.Delete(folderPath, true);
            }

            Console.WriteLine("DeleteFeedFolders() done");
        }

        private static void ListFeedFolders()
        {
            string[] filePaths = Directory.GetFiles(@"C:\Temp\LEN\LivetEfterNoa\wwwlivetefternoase\", "feed", SearchOption.AllDirectories);

            foreach (var file in filePaths)
            {
                if (file.Contains("feed"))
                {
                    Console.WriteLine(file);
                }
            }

            Console.WriteLine("Number of feed folders: " + filePaths.Length);
        }

        private static void RemoveSrcSet()
        {
            var client = new WebClient();
            string[] filePaths = Directory.GetFiles(@"C:\Temp\LEN\LivetEfterNoa\wwwlivetefternoase\", "*.html", SearchOption.AllDirectories);

            foreach (var file in filePaths)
            {
                if (!file.Contains("feed") && !file.Contains("wp-content") && file.EndsWith("index2.html"))
                {
                    var split = file.Split(".")[0];
                    var newFileName = split.Remove(split.Length - 1) + ".html";
                    var stream = client.OpenRead(file);
                    using (var reader = new StreamReader(stream))
                    {
                        var str = reader.ReadToEnd();
                        var doc = new HtmlDocument();
                        doc.LoadHtml(str);
                        var elements = doc.DocumentNode.SelectNodes("//img");

                        if (elements != null)
                        {
                            foreach (var element in elements)
                            {
                                element.Attributes.Remove("srcset");
                            }
                        }

                        doc.Save(newFileName);
                        reader.Close();
                    }
                }
            }

            Console.WriteLine("RemoveSrcSet() done");
        }

        private static void CopyHtmlFiles()
        {
            var client = new WebClient();
            string[] filePaths = Directory.GetFiles(@"C:\Temp\LEN\LivetEfterNoa\wwwlivetefternoase\", "index.html", SearchOption.AllDirectories);

            foreach (var file in filePaths)
            {
                if (!file.Contains("feed") && !file.Contains("wp-content"))
                {
                    var stream = client.OpenRead(file);
                    using (var reader = new StreamReader(stream))
                    {
                        var newFileName = file.Split(".")[0] + "2" + ".html";
                        var str = reader.ReadToEnd();
                        var doc = new HtmlDocument();
                        doc.LoadHtml(str);
                        doc.Save(newFileName);
                        stream.Close();
                    }
                }
            }

            Console.WriteLine("CopyHtmlFiles() done");
        }
    }
}
