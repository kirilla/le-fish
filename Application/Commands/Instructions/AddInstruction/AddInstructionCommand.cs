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

        var set = await database.InstructionSets
            .Where(x => x.Id == model.InstructionSetId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var page = new Instruction()
        {
            Name = model.Name,
            Script = model.Script,
            InstructionSetId = set.Id,
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
