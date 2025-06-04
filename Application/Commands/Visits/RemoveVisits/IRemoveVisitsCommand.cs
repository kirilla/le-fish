namespace Lefish.Application.Commands.Visits.RemoveVisits;

public interface IRemoveVisitsCommand
{
    Task Execute(IUserToken userToken, RemoveVisitsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
