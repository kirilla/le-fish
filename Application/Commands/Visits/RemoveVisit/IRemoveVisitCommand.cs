namespace Lefish.Application.Commands.Visits.RemoveVisit;

public interface IRemoveVisitCommand
{
    Task Execute(IUserToken userToken, RemoveVisitCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
