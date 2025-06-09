namespace Lefish.Application.Commands.InstructionSets.CloneInstructionSet;

public class CloneInstructionSetCommand(IDatabaseService database) : ICloneInstructionSetCommand
{
    public async Task Execute(
        IUserToken userToken, CloneInstructionSetCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var set = await database.InstructionSets
            .AsNoTracking()
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.InstructionSets
            .AnyAsync(x => x.Name == model.Name))
            throw new BlockedByExistingException();

        var instructions = await database.Instructions
            .AsNoTracking()
            .Where(x => x.InstructionSetId == model.Id)
            .OrderBy(x => x.Created)
            .ToListAsync();

        var newSet = new InstructionSet()
        {
            Name = model.Name,
        };

        database.InstructionSets.Add(newSet);

        var newInstructions = instructions
            .Select(x => new Instruction()
            {
                Name = x.Name,
                Script = x.Script,
                Created = x.Created,
                InstructionSet = newSet,
            })
            .ToList();

        database.Instructions.AddRange(newInstructions);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
