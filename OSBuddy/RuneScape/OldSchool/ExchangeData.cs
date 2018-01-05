using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace OSBuddy.RuneScape.OldSchool
{
    [DataContract]
    public sealed class ExchangeData
    {
        [DataMember(Name = "overall")]
        public int Price { get; protected set; }

        [DataMember(Name = "buying")]
        public int High { get; protected set; }

        [DataMember(Name = "buyingQuantity")]
        public int Demand { get; protected set; }

        [DataMember(Name = "selling")]
        public int Low { get; protected set; }

        [DataMember(Name = "sellingQuantity")]
        public int Supply { get; protected set; }
    }
}
