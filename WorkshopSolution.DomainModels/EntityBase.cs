using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WorkshopSolution.DomainModels
{
  public abstract class EntityBase
  {
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int Id { get; set; }

    public bool IsValid()
    {
      var props = GetType().GetProperties();
      foreach (var prop in props)
      {
        var attrs = prop.GetCustomAttributes<ValidationAttribute>(true);
        foreach (var attr in attrs)
        {
          var val = prop.GetValue(this);
          if (!attr.IsValid(val))
          {
            return false;
          }
        }
      }
      return true;
    }

  }
}