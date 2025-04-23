namespace Lefish.Application.Commands.Companies.EditCompany;

public interface IEditCompanyCommand
{
    Task Execute(IUserToken userToken, EditCompanyCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
