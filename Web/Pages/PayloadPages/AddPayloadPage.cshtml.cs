using Lefish.Application.Commands.PayloadPages.AddWebPage;

namespace Lefish.Web.Pages.PayloadPages;

public class AddPayloadPageModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddWebPageCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public AddWebPageCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddWebPageCommandModel()
            {
                Name = "Förslag",
                Html = GetDefaultTemplate(),
            };

            return Page();
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

            if (!ModelState.IsValid)
                return Page();

            var id = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-payload-page/{id}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns en annan sida med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }

    private string GetDefaultTemplate()
    {
        return """
            <!DOCTYPE html>
            <html lang="en-US">
            <head>
                <meta charset="utf-8" />
                <title>Sida</title>
                <script src="[[script_url]]" defer=""></script>
            </head>
            <body>
                <h1>Rubrik</h1>
                <script>
                    console.log('hej [[attack_token]]');
                </script>
            </body>
            </html>
            """;
    }
}
