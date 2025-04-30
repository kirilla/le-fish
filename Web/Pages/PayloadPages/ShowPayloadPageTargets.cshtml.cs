namespace Lefish.Web.Pages.PayloadPages;

public class ShowPayloadPageTargetsModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public PayloadPage PayloadPage { get; set; }

    public List<EmailTarget> EmailTargets { get; set; }

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

            EmailTargets = await database.EmailTargets
                .OrderBy(x => x.Address)
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
