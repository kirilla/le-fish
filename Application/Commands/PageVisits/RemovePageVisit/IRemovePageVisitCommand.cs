namespace Lefish.Application.Commands.PageVisits.RemovePageVisit;

public interface IRemovePageVisitCommand
{
    Task Execute(IUserToken userToken, RemovePageVisitCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
