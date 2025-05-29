namespace Lefish.Application.Commands.Instructions.CloneInstruction;

public interface ICloneInstructionCommand
{
    Task<int> Execute(IUserToken userToken, CloneInstructionCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
