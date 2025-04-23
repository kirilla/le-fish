using Lefish.Application.Commands.EmailImages.RemoveEmailImage;

namespace Lefish.Web.Pages.EmailImages;

public class RemoveEmailImageModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveEmailImageCommand command) : UserTokenPageModel(userToken)
{
    public EmailImage EmailImage { get; set; }

    [BindProperty]
    public RemoveEmailImageCommandModel CommandModel { get; set; }

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

            CommandModel = new RemoveEmailImageCommandModel()
            {
                EmailImageId = EmailImage.Id,
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

    public async Task<IActionResult> OnPostAsync()
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

            return Redirect($"/show-email-template/{EmailImage.EmailTemplateId}");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
