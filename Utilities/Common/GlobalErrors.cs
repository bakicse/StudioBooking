namespace Shared.Common;
public static class GlobalErrors
{
    public static string DuplicateSubmission()
    {
        return "This combination already exists.\nPlease try again with different items.";
    }

    public static string FilterByIdNotFound(string? item, string? filterBy)
    {
        return $"There is no {item} related to this {filterBy}";
    }

    public static string NotFound(string? item)
    {
        return $"{item} not found.";
    }

    public static string InvalidSubmission()
    {
        return "Invalid Submission.";
    }
}
