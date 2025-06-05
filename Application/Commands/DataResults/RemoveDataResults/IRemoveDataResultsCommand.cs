namespace Lefish.Application.Commands.DataResults.RemoveDataResults;

public interface IRemoveDataResultsCommand
{
    Task Execute(IUserToken userToken, RemoveDataResultsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
