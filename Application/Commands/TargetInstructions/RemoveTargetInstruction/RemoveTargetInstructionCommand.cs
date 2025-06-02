namespace Lefish.Application.Commands.TargetInstructions.RemoveTargetInstruction;

public class RemoveTargetInstructionCommand(IDatabaseService database) : IRemoveTargetInstructionCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveTargetInstructionCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var page = await database.TargetInstructions
            .Where(x => x.Id == model.TargetInstructionId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.TargetInstructions.Remove(page);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
