using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arta.Domain.Core.Commons
{
    // Generic interface for mapping between two types
    public interface IMapper<TSource, TDestination>
    {
        TDestination ToEntity(TSource source);
        TSource ToDto(TDestination destination);
    }
}
