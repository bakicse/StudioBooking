namespace Domain.ValidationAttributes;
public static class Error
{
    public static string DuplicateSubmission(string? fieldName, string? fieldValue)
    {
        return $"{fieldName}: {fieldValue} already exists. Please try again with different {fieldName}.";
    }
}
