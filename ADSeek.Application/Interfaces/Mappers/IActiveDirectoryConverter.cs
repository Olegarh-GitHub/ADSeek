using System.Collections.Generic;
using ADSeek.Domain.Models;

namespace ADSeek.Application.Interfaces.Mappers
{
    public interface IActiveDirectoryConverter<TSource>
    {
        public ActiveDirectoryObject Map(TSource source);
        
        public IEnumerable<ActiveDirectoryObject> Map(IEnumerable<TSource> source);
    }
}