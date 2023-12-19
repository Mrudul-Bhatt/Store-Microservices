namespace Store.Models
{
    public class Commerce
    {
        public Consumer User { get; set; }
        public List<Product> Products { get; set; }
        public Cart Cart { get; set; }
    }
}

