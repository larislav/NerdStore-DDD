using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Core.Data
{
    // Para tender a regra de 1 repositório por agregação
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        
        IUnitOfWork UnitOfWork { get; }
    }
}
