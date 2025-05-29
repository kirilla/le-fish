namespace Lefish.Application.Commands.Instructions.CloneInstruction;

public class CloneInstructionCommand(IDatabaseService database) : ICloneInstructionCommand
{
    public async Task<int> Execute(
        IUserToken userToken, CloneInstructionCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var page = await database.Instructions
            .AsNoTracking()
            .Where(x => x.Id == model.InstructionId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.Instructions
            .AnyAsync(x => x.Name == model.Name))
            throw new BlockedByExistingException();

        var newPage = new Instruction()
        {
            Name = model.Name,
            Script = page.Script,
        };

        database.Instructions.Add(newPage);

        await database.SaveAsync(userToken);

        return newPage.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
