namespace Lefish.Application.Commands.InstructionSets.CloneInstructionSet;

public class CloneInstructionSetCommand(IDatabaseService database) : ICloneInstructionSetCommand
{
    public async Task<int> Execute(
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

        //var instructions = await database.Instructions
        //    .AsNoTracking()
        //    .Where(x => x.InstructionSetId == model.Id)
        //    .ToListAsync();

        var newSet = new InstructionSet()
        {
            Name = model.Name,
        };

        database.InstructionSets.Add(newSet);

        //foreach (var instruction in instructions)
        //{
        //    var newInstruction = new Instruction()
        //    {
        //        Name = instruction.Name,
        //        Script = instruction.Script,
        //        Name = instruction.Name,
        //        Created = instruction.Created,
        //        //InstructionSet = newSet,
        //    };

        //    database.Instructions.Add(newInstruction);
        //}

        await database.SaveAsync(userToken);

        return newSet.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
