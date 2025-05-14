using System.ComponentModel.DataAnnotations.Schema;

namespace LimsReactifService.Dtos;

public class ResteStockDto
{
    public double Quantite { get; set; }
    public string Unite { get; set; } = string.Empty;
    public DateTime DateParam { get; set; } = DateTime.Now;
    public int IdReactif { get; set; }
}
