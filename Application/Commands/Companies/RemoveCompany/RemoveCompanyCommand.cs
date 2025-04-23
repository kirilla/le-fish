namespace Lefish.Application.Commands.Companies.RemoveCompany;

public class RemoveCompanyCommand(IDatabaseService database) : IRemoveCompanyCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveCompanyCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var company = await database.Companies
            .Where(x => x.Id == model.CompanyId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.Companies.Remove(company);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
