namespace Lefish.Application.Commands.TargetInstructions.CloneTargetInstruction;

public class CloneTargetInstructionCommand(IDatabaseService database) : ICloneTargetInstructionCommand
{
    public async Task<int> Execute(
        IUserToken userToken, CloneTargetInstructionCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var existing = await database.TargetInstructions
            .AsNoTracking()
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var clone = new TargetInstruction()
        {
            AttackId = existing.AttackId,
            Name = model.Name,
            Script = existing.Script,
            Reference = Random.Shared.Next(),
            InstructionStatus = InstructionStatus.Available,
        };

        database.TargetInstructions.Add(clone);

        await database.SaveAsync(userToken);

        return clone.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
