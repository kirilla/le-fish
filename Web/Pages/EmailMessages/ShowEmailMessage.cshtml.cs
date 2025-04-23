namespace Lefish.Web.Pages.EmailMessages;

public class ShowEmailMessageModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public EmailMessage EmailMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!userToken.IsAuthenticated)
                throw new NotPermittedException();

            EmailMessage = await database.EmailMessages
                .Include(x => x.EmailAccount)
                .Include(x => x.EmailTarget)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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
