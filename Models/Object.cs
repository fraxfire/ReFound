namespace ReFound.Models
{
    public class Object
{
    public int ObjectId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }  // Proprietario dell'oggetto
}

}