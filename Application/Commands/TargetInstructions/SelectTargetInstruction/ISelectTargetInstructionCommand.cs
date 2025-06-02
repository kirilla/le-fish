namespace Lefish.Application.Commands.TargetInstructions.SelectTargetInstruction;

public interface ISelectTargetInstructionCommand
{
    Task Execute(IUserToken userToken, SelectTargetInstructionCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
