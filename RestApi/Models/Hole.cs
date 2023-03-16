namespace RestApi.Models;

public class Hole
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public int DrillBlockId { get; set; }
    
    public decimal Depth { get; set; }

    public DrillBlock DrillBlock { get; set; }
    
    public ICollection<HolePoint> HolePoints { get; set; }
}