namespace Lefish.Web.Pages.PayloadPages;

public class VisitPayloadPageAsModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public PayloadPage PayloadPage { get; set; }

    public List<PageKeyPlus> PageKeys { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PayloadPage = await database.PayloadPages
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PageKeys = await database.Attacks
                .Where(x => x.PayloadPageId == id)
                .OrderBy(x => x.EmailTarget.Name)
                .ThenBy(x => x.Value)
                .Select(x => new PageKeyPlus()
                {
                    Id = x.Id,
                    TargetName = x.EmailTarget.Name,
                    TargetAddress = x.EmailTarget.Address,
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
