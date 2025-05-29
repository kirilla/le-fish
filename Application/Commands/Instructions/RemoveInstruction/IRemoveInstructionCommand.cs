namespace Lefish.Application.Commands.Instructions.RemoveInstruction;

public interface IRemoveInstructionCommand
{
    Task Execute(IUserToken userToken, RemoveInstructionCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
