namespace Lefish.Application.Commands.TargetInstructions.EditTargetInstruction;

public interface IEditTargetInstructionCommand
{
    Task Execute(IUserToken userToken, EditTargetInstructionCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
