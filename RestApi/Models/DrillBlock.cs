namespace RestApi.Models;

public class DrillBlock
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public DateTime UpdateDate { get; set; }
    public ICollection<DrillBlockPoint> DrillBlockPoints { get; set; }
}