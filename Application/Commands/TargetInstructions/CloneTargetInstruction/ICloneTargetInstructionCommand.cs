namespace Lefish.Application.Commands.TargetInstructions.CloneTargetInstruction;

public interface ICloneTargetInstructionCommand
{
    Task<int> Execute(IUserToken userToken, CloneTargetInstructionCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
