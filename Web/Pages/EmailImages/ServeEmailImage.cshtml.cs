namespace Lefish.Web.Pages.EmailImages;

public class ServeEmailImageModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                return NotFound();

            var image = await database.EmailImages
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            return File(image.Data, image.ContentType);
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
