using System.Runtime.Serialization;

namespace OSBuddy.RuneScape.OldSchool
{
    [DataContract]
    public sealed class Item
    {
        [DataMember(Name = "id")]
        public int Id { get; protected set; } 

        [DataMember(Name =  "stackable")]
        public bool Stackable { get; protected set; }

        [DataMember(Name = "name")]
        public string Name { get; protected set; }

        [DataMember(Name = "transferable")]
        public bool Tradeable { get; protected set; }

        [DataMember(Name = "members")]
        public bool MembersOnly { get; protected set; }

        [DataMember(Name = "store_price")]
        public int StorePrice { get; protected set; }

        [DataMember(Name = "high_alchemy")]
        public int HighAlchemy { get; protected set; }

        [DataMember(Name = "low_alchemy")]
        public int LowAlchemy { get; protected set; }

        public static explicit operator int(Item item)
        {
            return item.Id;
        }

        public override string ToString()
        {
            return Name;
        }
    }

}
