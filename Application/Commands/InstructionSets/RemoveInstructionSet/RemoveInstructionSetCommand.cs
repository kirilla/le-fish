namespace Lefish.Application.Commands.InstructionSets.RemoveInstructionSet;

public class RemoveInstructionSetCommand(IDatabaseService database) : IRemoveInstructionSetCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveInstructionSetCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var set = await database.InstructionSets
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.InstructionSets.Remove(set);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
