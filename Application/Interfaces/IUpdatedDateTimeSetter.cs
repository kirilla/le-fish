using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lefish.Application.Interfaces
{
    public interface IUpdatedDateTimeSetter
    {
        void SetUpdated(ChangeTracker changeTracker);
    }
}
