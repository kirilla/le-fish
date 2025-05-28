namespace Lefish.Web.Pages.IpRanges;

public class ShowIpRangesModel(
    IUserToken userToken,
    IDatabaseService database,
    IOptions<IpFilterConfiguration> ipFilterOptions) : UserTokenPageModel(userToken)
{
    public readonly IpFilterConfiguration IpFilterConfiguration = ipFilterOptions.Value;

    public List<IpRange> IpRanges { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            IpRanges = await database.IpRanges.ToListAsync();

            IpRanges = IpRanges
                .OrderByDescending(x => x.Prefix) // Higher specificity
                .ThenBy(x => x.BaseAddress)
                .ToList();

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
