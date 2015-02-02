namespace WebParsingFramework
{
    public class WebShop
    {
        public WebShop(string name)
        {
            Name = name;
            Address = name;
        }

        public WebShop(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public bool Equals(WebShop other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(WebShop)) return false;
            return Equals((WebShop)obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}