namespace RestApi.Models;

public class DrillBlockPoint
{
    public int Id { get; set; }
    public int DrillBlockId { get; set; }
    
    public int Sequence { get; set; }
    
    public decimal X { get; set; }
    
    public decimal Y { get; set; }
    
    public decimal Z { get; set; }
    public DrillBlock DrillBlock { get; set; }
}