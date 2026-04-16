using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arta.Domain.Core.Model
{
    public abstract class Entity<TId> : IEntity<TId>
    {
        public TId Id { get; protected set; } = default!;

        public override bool Equals(object? obj)
        {
            if (obj is not Entity<TId> other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Equals(Id, default) || Equals(other.Id, default))
                return false;

            return Id.Equals(other.Id);
        }

        public override int GetHashCode() =>
            (GetType().ToString() + Id).GetHashCode();
    }

}
