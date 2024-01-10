namespace WebApi.Models
{
    public class Product : BaseModel
    {
        public string Description { get; set; } = null!;
        public int GroupID { get; set; }
        public Group Group { get; set; } = null!;
        public int Price { get; set; }
        public virtual List<Store> Stores { get; set; } = null!;

    }
}
