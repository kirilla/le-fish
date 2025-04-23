using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lefish.Application.Interfaces
{
    public interface ICreatedDateTimeSetter
    {
        void SetCreated(ChangeTracker changeTracker);
    }
}
