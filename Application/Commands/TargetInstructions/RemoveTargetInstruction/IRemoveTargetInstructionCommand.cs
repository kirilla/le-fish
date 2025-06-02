namespace Lefish.Application.Commands.TargetInstructions.RemoveTargetInstruction;

public interface IRemoveTargetInstructionCommand
{
    Task Execute(IUserToken userToken, RemoveTargetInstructionCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
