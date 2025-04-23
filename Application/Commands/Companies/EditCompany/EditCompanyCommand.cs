namespace Lefish.Application.Commands.Companies.EditCompany;

public class EditCompanyCommand(IDatabaseService database) : IEditCompanyCommand
{
    public async Task Execute(
        IUserToken userToken, EditCompanyCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var company = await database.Companies
            .Where(x => x.Id == model.CompanyId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.Companies
            .AnyAsync(x =>
                x.Name == model.Name &&
                x.Id != model.CompanyId))
            throw new BlockedByNameException();

        company.Name = model.Name;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
