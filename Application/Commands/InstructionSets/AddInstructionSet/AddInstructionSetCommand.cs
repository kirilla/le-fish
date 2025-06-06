namespace Lefish.Application.Commands.InstructionSets.AddInstructionSet;

public class AddInstructionSetCommand(IDatabaseService database) : IAddInstructionSetCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddInstructionSetCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var set = new InstructionSet()
        {
            Name = model.Name,
        };

        database.InstructionSets.Add(set);

        await database.SaveAsync(userToken);

        return set.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
