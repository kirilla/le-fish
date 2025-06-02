namespace Lefish.Application.Commands.TargetInstructions.SelectTargetInstruction;

public class SelectTargetInstructionCommand(IDatabaseService database) : ISelectTargetInstructionCommand
{
    public async Task Execute(
        IUserToken userToken, SelectTargetInstructionCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();
        model.TruncateByStringLength();

        var pageKey = await database.PageKeys
            .Where(x => x.Id == model.PageKeyId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var instruction = await database.Instructions
            .Where(x => x.Id == model.InstructionId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var page = new TargetInstruction()
        {
            PageKeyId = pageKey.Id,
            Name = instruction.Name,
            Script = instruction.Script,
            Reference = Random.Shared.Next(),
        };

        database.TargetInstructions.Add(page);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
