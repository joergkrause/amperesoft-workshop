using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopSolution.DomainModels
{
  public static class EntityExtensions
  {
    public static bool IsValid(this IEnumerable<EntityBase> entities)
    {
      return entities.All(e => e.IsValid());
    }

  }
}
