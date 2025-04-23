namespace Lefish.Application.Commands.Companies.AddCompany;

public interface IAddCompanyCommand
{
    Task<int> Execute(IUserToken userToken, AddCompanyCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
