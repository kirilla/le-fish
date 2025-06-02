namespace Lefish.Application.Commands.TargetInstructions.EditTargetInstruction;

public class EditTargetInstructionCommand(IDatabaseService database) : IEditTargetInstructionCommand
{
    public async Task Execute(
        IUserToken userToken, EditTargetInstructionCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();
        model.TruncateByStringLength();

        var page = await database.TargetInstructions
            .Where(x => x.Id == model.TargetInstructionId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.TargetInstructions
            .AnyAsync(x =>
                x.Name == model.Name &&
                x.Id != model.TargetInstructionId))
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
