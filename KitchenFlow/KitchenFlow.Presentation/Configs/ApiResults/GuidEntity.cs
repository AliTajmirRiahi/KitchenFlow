using Arta.Domain.Core.Model;

namespace KitchenFlow.Presentation.Configs.ApiResults
{

    public class GuidEntity : Entity<Guid>
    {
        public GuidEntity(Guid guid)
        {
            Id = guid;
        }
    }
}
