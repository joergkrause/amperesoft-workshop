using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Markup;

namespace WorkshopSolution.DomainModels
{
  public class Rack : EntityBase
  {

    [BsonElement("Name")] // optional
    [StringLength(100), Required]
    [RackName("Demo")]
    public string Name { get; set; }

    [Range(1, 1000)]
    public int Height { get; set; }

    [Range(1, 1000)]
    public int Width { get; set; }

    [NotMapped]
    public int Area { get => Height * Width; }
  }


  public class RackNameAttribute(string NameChar) : ValidationAttribute
  {
    public override bool IsValid(object? value)
    {
      if (value is string vs)
      {
        return vs.StartsWith(NameChar);
      }
      return base.IsValid(value);
    }
  }

}
