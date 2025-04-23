using Lefish.Application.Commands.EmailImages.EditEmailImage;

namespace Lefish.Web.Pages.EmailImages;

public class EditEmailImageModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditEmailImageCommand command) : UserTokenPageModel(userToken)
{
    public EmailImage EmailImage { get; set; }

    [BindProperty]
    public EditEmailImageCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailImage = await database.EmailImages
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditEmailImageCommandModel()
            {
                EmailImageId = EmailImage.Id,
                Name = EmailImage.Name,
                ContentType = EmailImage.ContentType,
            };

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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailImage = await database.EmailImages
                .Where(x => x.Id == CommandModel.EmailImageId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-image/{id}");
        }
        catch (BlockedByNameException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns en annat bild med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
