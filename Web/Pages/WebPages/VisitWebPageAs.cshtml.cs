namespace Lefish.Web.Pages.PayloadPages;

public class VisitPayloadPageAsModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public WebPage PayloadPage { get; set; }

    public List<AttackSummary> Attacks { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PayloadPage = await database.WebPages
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Attacks = await database.Attacks
                .Where(x => x.PayloadPageId == id)
                .OrderBy(x => x.Target.Name)
                .ThenBy(x => x.Value)
                .Select(x => new AttackSummary()
                {
                    Id = x.Id,
                    TargetName = x.Target.Name,
                    TargetAddress = x.Target.Address,
                    Value = x.Value,
                })
                .ToListAsync();

            return Page();
        }
        catch (NotFoundException)
        {
            return Redirect("/help/notfound");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
