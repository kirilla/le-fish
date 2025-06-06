namespace Lefish.Application.Commands.InstructionSets.CloneInstructionSet;

public interface ICloneInstructionSetCommand
{
    Task<int> Execute(IUserToken userToken, CloneInstructionSetCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
