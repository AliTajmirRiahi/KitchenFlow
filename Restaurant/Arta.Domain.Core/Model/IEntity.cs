using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arta.Domain.Core.Model
{
    public interface IEntity<TId>
    {
        TId Id { get; }

        // Flag for Soft Delete
        bool IsDeleted { get; }
        // Stamp for Concurrency control
        byte[] Version { get; }

        void Delete(); // Method to trigger soft delete
    }
}
