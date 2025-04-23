namespace Lefish.Application.Commands.Companies.AddCompany;

public class AddCompanyCommand(IDatabaseService database) : IAddCompanyCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddCompanyCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.Companies
            .AnyAsync(x => x.Name == model.Name))
            throw new BlockedByExistingException();

        var company = new Company()
        {
            Name = model.Name,
        };

        database.Companies.Add(company);

        await database.SaveAsync(userToken);

        return company.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
