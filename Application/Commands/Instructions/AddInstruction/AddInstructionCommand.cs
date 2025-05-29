namespace Lefish.Application.Commands.Instructions.AddInstruction;

public class AddInstructionCommand(IDatabaseService database) : IAddInstructionCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddInstructionCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();
        model.TruncateByStringLength();

        if (await database.Instructions
            .AnyAsync(x => x.Name == model.Name))
            throw new BlockedByExistingException();

        var page = new Instruction()
        {
            Name = model.Name,
            Script = model.Script,
        };

        database.Instructions.Add(page);

        await database.SaveAsync(userToken);

        return page.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
