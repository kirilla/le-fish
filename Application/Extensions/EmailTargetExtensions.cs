namespace Lefish.Application.Extensions;

public static class EmailTargetExtensions
{
    public static void GeneratePhishingKey(this EmailTarget target, List<string> takenKeys)
    {
        string? key = null;

        while (key == null)
        {
            key = Random.Shared.Next().ToString();

            if (takenKeys.Any(x => x == key))
                key = null;
        }

        target.PersonKey = key;
    }
}
