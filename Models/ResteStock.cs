namespace LimsReactifService.Models;

public class ResteStock
{
    public double Quantite { get; set; }
    public string Unite { get; set; } = string.Empty;
    public int IdReactif { get; set; }
    public string Designation { get; set; } = string.Empty;
    public DateTime? DateLastreport { get; set; }
}
