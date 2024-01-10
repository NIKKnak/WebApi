namespace WebApi.Models
{
    public class Store : BaseModel
    {
        public virtual List<Product> Products { get; set; } 
        public int Count { get; set; }

    }
}
