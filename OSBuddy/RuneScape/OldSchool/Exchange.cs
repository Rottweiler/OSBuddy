using System.Net;
using System.Runtime.Serialization.Json;

namespace OSBuddy.RuneScape.OldSchool
{
    public static class Exchange
    {
        static Exchange()
        {
            _exchangeSerializer = new DataContractJsonSerializer(typeof(ExchangeData));
        }

        /// <summary>
        /// Get the price for buying the item
        /// </summary>
        public static int GetPrice(Item item)
        {
            return GetPrice((int)item);
        }

        /// <summary>
        /// Get the price for buying the item
        /// </summary>
        public static int GetPrice(int item)
        {
            var data = GetData(item);
            return data.High;
        }

        /// <summary>
        /// Get the sell value of the item
        /// </summary>
        public static int GetValue(Item item)
        {
            return GetValue((int)item);
        }

        /// <summary>
        /// Get the sell value of the item
        /// </summary>
        public static int GetValue(int item)
        {
            var data = GetData(item);
            return data.Low;
        }

        /// <summary>
        /// Get the current Grand Exchange data for the item
        /// </summary>
        public static ExchangeData GetData(Item item)
        {
            return GetData((int)item);
        }

        /// <summary>
        /// Get the current Grand Exchange data for the item
        /// </summary>
        public static ExchangeData GetData(int item)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://api.rsbuddy.com/grandExchange/?a=guidePrice&i=" + item.ToString());
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Proxy = null;
            using (var response = request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    var data = (ExchangeData)_exchangeSerializer.ReadObject(responseStream);
                    return data;
                }
            }
        }

        /// <summary>
        /// Format the number into the RuneScape currency
        /// </summary>
        public static string GetFormattedCurrency(int value, bool exact)
        {
            if (exact)
            {
                return value.ToString("N0") + " gp";
            }
            else
            {
                if (value < 1000)
                {
                    return (value).ToString() + " gp";
                }
                else if (value < 1000000)
                {
                    return (value / 1000D).ToString("0.#") + "K" + " gp";
                }
                else if (value < 1000000000)
                {
                    return (value / 1000000D).ToString("0.#") + "M" + " gp";
                }
                else
                {
                    return (value / 1000000000D).ToString("0.#") + "B" + " gp";
                }
            }
        }

        private static DataContractJsonSerializer _exchangeSerializer;
    }
}
