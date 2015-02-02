namespace WebParsingFramework
{
    /// <summary>
    /// Наличие в розничном магазине.
    /// </summary>
    public class WebProductAvailabilityInShop
    {
        public WebProductAvailabilityInShop()
        {
        }

        public WebProductAvailabilityInShop(string shopName, string shopAddress, bool isAvailable)
        {
            ShopName = shopName;
            ShopAddress = shopAddress;
            IsAvailable = isAvailable;
        }

        public WebProductAvailabilityInShop(string shopAddress, bool isAvailable)
        {
            ShopName = shopAddress;
            ShopAddress = shopAddress;
            IsAvailable = isAvailable;
        }

        public string ShopName { get; set; }

        public string ShopAddress { get; set; }

        public bool IsAvailable { get; set; }

        public override string ToString()
        {
            return ShopName;
        }

        public bool Equals(WebProductAvailabilityInShop other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.ShopName, ShopName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (WebProductAvailabilityInShop)) return false;
            return Equals((WebProductAvailabilityInShop) obj);
        }

        public override int GetHashCode()
        {
            return ShopName.GetHashCode();
        }
    }
}