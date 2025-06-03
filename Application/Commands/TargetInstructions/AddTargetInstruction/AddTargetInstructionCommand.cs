namespace Lefish.Application.Commands.TargetInstructions.AddTargetInstruction;

public class AddTargetInstructionCommand(IDatabaseService database) : IAddTargetInstructionCommand
{
    public async Task Execute(
        IUserToken userToken, AddTargetInstructionCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();
        model.TruncateByStringLength();

        var attack = await database.Attacks
            .Where(x => x.Id == model.AttackId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var page = new TargetInstruction()
        {
            AttackId = attack.Id,
            Name = model.Name,
            Script = model.Script,
            Reference = Random.Shared.Next(),
            InstructionStatus = InstructionStatus.Available,
        };

        database.TargetInstructions.Add(page);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
