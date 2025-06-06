namespace Lefish.Application.Commands.InstructionSets.AddInstructionSet;

public interface IAddInstructionSetCommand
{
    Task<int> Execute(IUserToken userToken, AddInstructionSetCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
