using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lefish.Application.Interfaces;

public interface IOnSaveValidator
{
    void Validate(ChangeTracker changeTracker);
}
