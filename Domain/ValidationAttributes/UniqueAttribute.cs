using AutoMapper;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class UniqueAttribute<T> : ValidationAttribute
    where T : class
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var entityType = validationContext.ObjectInstance.GetType();
        if (entityType.GetProperty("EncryptedId")?.GetValue(validationContext.ObjectInstance) is null)
        {
            var primaryKeyProperty = entityType.GetProperties()
                .FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length > 0);
            var primaryKeyValue = primaryKeyProperty?.GetValue(validationContext.ObjectInstance);

            if (primaryKeyValue is not null && primaryKeyValue.Equals(0))
            {
                var context = (AppDbContext)validationContext.GetService(typeof(AppDbContext))!;
                var method = typeof(DbContext).GetMethod(nameof(DbContext.Set), Type.EmptyTypes);
                if (method is not null)
                {
                    var mapper = (IMapper)validationContext.GetService(typeof(IMapper))!;
                    _ = mapper.Map<T>(validationContext.ObjectInstance);

                    bool anyDuplicate = context.Set<T>()
                        .AsNoTracking()
                        .Any(e => EF.Property<object>(e, validationContext.MemberName!).Equals(value));

                    if (anyDuplicate)
                    {
                        return new ValidationResult(Error.DuplicateSubmission(validationContext.MemberName, value?.ToString()));
                    }
                }
            }
        }
        return ValidationResult.Success!;
    }
}