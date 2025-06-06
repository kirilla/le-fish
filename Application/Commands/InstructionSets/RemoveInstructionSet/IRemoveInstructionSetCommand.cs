namespace Lefish.Application.Commands.InstructionSets.RemoveInstructionSet;

public interface IRemoveInstructionSetCommand
{
    Task Execute(IUserToken userToken, RemoveInstructionSetCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
