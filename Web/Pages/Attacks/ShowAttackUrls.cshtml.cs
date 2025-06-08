namespace Lefish.Web.Pages.Attacks;

public class ShowAttackUrlsModel(
    IUserToken userToken,
    IDatabaseService database,
    IOptions<TemplateConfiguration> templateConfiguration) : UserTokenPageModel(userToken)
{
    public readonly TemplateConfiguration Config = templateConfiguration.Value;

    public string PageUrl { get; set; }
    public string PageScriptUrl { get; set; }
    public string FetchInstructionUrl { get; set; }
    public string UploadDataUrl { get; set; }

    public Attack Attack { get; set; }
    public Target Target { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            Attack = await database.Attacks
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Target = await database.Targets
                .Where(x => x.Id == Attack.TargetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PageUrl = $"/page/{Attack.Value}";
            PageScriptUrl = $"/script/{Attack.Value}";
            FetchInstructionUrl = $"/fetch-instruction/{Attack.Value}";
            UploadDataUrl = $"/upload/{Attack.Value}";

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
