namespace Lefish.Application.Commands.Instructions.RemoveInstruction;

public class RemoveInstructionCommand(IDatabaseService database) : IRemoveInstructionCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveInstructionCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var page = await database.Instructions
            .Where(x => x.Id == model.InstructionId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.Instructions.Remove(page);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
