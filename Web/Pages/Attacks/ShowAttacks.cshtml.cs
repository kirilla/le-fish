namespace Lefish.Web.Pages.Attacks;

public class ShowAttacksModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public List<AttackSummary> Attacks { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            Attacks = await database.Attacks
                .OrderBy(x => x.Created)
                .ThenBy(x => x.Value)
                .Select(x => new AttackSummary()
                {
                    Id = x.Id,
                    Value = x.Value,
                    Created = x.Created,
                    PageName = x.WebPage.Name,
                    PayloadPageId = x.WebPageId,
                    TargetName = x.Target.Name,
                    TargetAddress = x.Target.Address,
                    TargetId = x.TargetId,
                })
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
