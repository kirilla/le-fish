namespace Lefish.Application.Commands.DataResults.EditDataResult;

public class EditDataResultCommand(IDatabaseService database) : IEditDataResultCommand
{
    public async Task Execute(
        IUserToken userToken, EditDataResultCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var result = await database.DataResults
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        result.JsonData = model.JsonData;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
