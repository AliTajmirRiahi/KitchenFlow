using Arta.Domain.Core.Model;

namespace KitchenFlow.Presentation.Configs.ApiResults
{
    public class IntEntity : Entity<int>
    {
        public IntEntity(int id)
        {
            Id = id;
        }
    }
}
