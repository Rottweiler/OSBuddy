﻿using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OSBuddy.RuneScape.OldSchool
{
    //http://www.rsbuddy.com/items.json
    //http://api.rsbuddy.com/grandExchange/?a=guidePrice&i=2
    public sealed class ItemCatalog
    {
        public List<Item> Items
        {
            get { return _items; }
            private set { _items = value; }
        }

        public ItemCatalog(List<Item> items)
        {
            _items = items;
        }

        public ItemCatalog(Item[] items)
        {
            _items = new List<Item>(items);
        }

        public ItemCatalog(IEnumerable<Item> items)
        {
            _items = new List<Item>(items);
        }

        public IEnumerable<Item> Search(string query)
        {
            var normalizedQuery = Normalize(query);

            foreach (var item in _items)
            {
                var normalizedItem = Normalize(item.Name);

                if (normalizedItem == normalizedQuery)
                {
                    yield return item;
                }

                if (normalizedItem.StartsWith(normalizedQuery))
                {
                    yield return item;
                }

                if (Regex.Match(normalizedItem, normalizedQuery).Success)
                {
                    yield return item;
                }
            }
        }

        private string Normalize(string query)
        {
            var builder = new StringBuilder();
            foreach (char c in query)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    builder.Append(c);
                }
            }
            return builder.ToString().ToLowerInvariant();
        }

        private List<Item> _items;
    }
}