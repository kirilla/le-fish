using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lefish.Application.Interfaces;

public interface IOnSaveFormatter
{
    void Format(ChangeTracker changeTracker);
}
