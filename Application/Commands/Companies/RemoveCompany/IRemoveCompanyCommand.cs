namespace Lefish.Application.Commands.Companies.RemoveCompany;

public interface IRemoveCompanyCommand
{
    Task Execute(IUserToken userToken, RemoveCompanyCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
