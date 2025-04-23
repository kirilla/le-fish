namespace Lefish.Web.Pages.EmailAttachments;

public class ServeEmailAttachmentModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                return NotFound();

            var attachment = await database.EmailAttachments
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            return File(attachment.Data, attachment.ContentType);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch
        {
            return NotFound();
        }
    }
}
