using TheDojoLeague.Models;
using System.Collections.Generic;
namespace TheDojoLeague.Factory
{
    public interface IFactory<T> where T : BaseEntity {}
}