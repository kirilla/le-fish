namespace Lefish.Application.Commands.PageVisits.RemovePageVisits;

public interface IRemovePageVisitsCommand
{
    Task Execute(IUserToken userToken, RemovePageVisitsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
