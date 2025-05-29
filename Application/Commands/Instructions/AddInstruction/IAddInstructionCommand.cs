namespace Lefish.Application.Commands.Instructions.AddInstruction;

public interface IAddInstructionCommand
{
    Task<int> Execute(IUserToken userToken, AddInstructionCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
