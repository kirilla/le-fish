namespace Lefish.Application.Commands.InstructionSets.EditInstructionSet;

public class EditInstructionSetCommand(IDatabaseService database) : IEditInstructionSetCommand
{
    public async Task Execute(
        IUserToken userToken, EditInstructionSetCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var set = await database.InstructionSets
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        set.Name = model.Name;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
