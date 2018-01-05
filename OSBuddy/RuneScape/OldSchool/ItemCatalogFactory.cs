using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace OSBuddy.RuneScape.OldSchool
{
    public sealed class ItemCatalogFactory
    {
        public ItemCatalogFactory()
        {
            _random = new Random(Guid.NewGuid().GetHashCode());
            _itemSerializer = new DataContractJsonSerializer(typeof(Item[]));
        }

        public ItemCatalog GetItemCatalog()
        {
            // todo: put in it's own function
            var files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache), "*.icat", SearchOption.TopDirectoryOnly);
            if (files.Length > 0)
            {
                foreach (var file in files)
                {
                    var creation = File.GetCreationTime(file) - DateTime.Now;
                    if (creation.Days < 30)
                    {
                        return new ItemCatalog(DeserializeItems(file));
                    }
                }
            }

            // download and return
            return new ItemCatalog(DownloadItems());
        }

        private string SaveItems(Item[] items)
        {
            var filename = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\\" + GetRandomString() + ".icat";
            using (var filestream = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                _itemSerializer.WriteObject(filestream, items);
            }
            return filename;
        }

        // todo: make better, problem with initial decoding
        private Item[] DownloadItems()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.rsbuddy.com/items.json");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Proxy = null;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        var rawData = reader.ReadToEnd();
                        var rawBytes = Encoding.UTF8.GetBytes(rawData);
                        using (var memory = new MemoryStream(rawBytes))
                        {
                            var items = (Item[])_itemSerializer.ReadObject(memory);
                            var file = SaveItems(items);
                            return items;
                        }
                    }
                }
            }
        }

        private Item[] DeserializeItems(string file)
        {
            using (var filestream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var items = (Item[])_itemSerializer.ReadObject(filestream);
                return items;
            }
        }

        private string GetRandomString()
        {
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            alphabet = alphabet.ToUpper();
            alphabet += "0123456789";
            var builder = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                builder.Append(alphabet[_random.Next(0, alphabet.Length)]);
            }
            return builder.ToString();
        }

        private DataContractJsonSerializer _itemSerializer;
        private Random _random;
    }
}
