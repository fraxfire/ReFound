using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Cart
{
    [Key]
    public int CartId { get; set; }
    public string? UserId { get; set; }
    [Required]
    public string? Name { get; set; }
    public decimal Price { get; set; }


    [ForeignKey("Object")]
    public int ObjectId { get; set; }
    public int Quantity { get; set; }
}