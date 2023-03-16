namespace RestApi.Models;

public class HolePoint
{
    public int Id { get; set; }
    public int HoleId { get; set; }
    
    public decimal X { get; set; }
    
    public decimal Y { get; set; }
    
    public decimal Z { get; set; }
    public Hole Hole { get; set; }
}