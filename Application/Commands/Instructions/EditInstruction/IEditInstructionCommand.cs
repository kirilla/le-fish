namespace Lefish.Application.Commands.Instructions.EditInstruction;

public interface IEditInstructionCommand
{
    Task Execute(IUserToken userToken, EditInstructionCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
