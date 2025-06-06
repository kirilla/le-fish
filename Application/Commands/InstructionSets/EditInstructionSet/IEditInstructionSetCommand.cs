namespace Lefish.Application.Commands.InstructionSets.EditInstructionSet;

public interface IEditInstructionSetCommand
{
    Task Execute(IUserToken userToken, EditInstructionSetCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
