namespace Lefish.Application.Commands.Instructions.EditInstruction;

public class EditInstructionCommand(IDatabaseService database) : IEditInstructionCommand
{
    public async Task Execute(
        IUserToken userToken, EditInstructionCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();
        model.TruncateByStringLength();

        var page = await database.Instructions
            .Where(x => x.Id == model.InstructionId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.Instructions
            .AnyAsync(x =>
                x.Name == model.Name &&
                x.Id != model.InstructionId))
            throw new BlockedByExistingException();

        page.Name = model.Name;
        page.Script = model.Script;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
