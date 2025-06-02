namespace Lefish.Application.Commands.TargetInstructions.AddTargetInstruction;

public interface IAddTargetInstructionCommand
{
    Task Execute(IUserToken userToken, AddTargetInstructionCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
