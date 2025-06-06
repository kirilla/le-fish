namespace Lefish.Web.Pages.WebPages;

public class VisitWebPageAsModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public WebPage WebPage { get; set; }

    public List<AttackSummary> Attacks { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            WebPage = await database.WebPages
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Attacks = await database.Attacks
                .Where(x => x.WebPageId == id)
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
