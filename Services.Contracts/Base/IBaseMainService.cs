namespace Services.Contracts.Base;
public interface IBaseMainService<TViewModel, TDto> where TViewModel : class where TDto : class
{
    Task<IEnumerable<TViewModel>> GetListAsync();
    Task<TDto?> GetByIdAsync(string encryptedId);
    Task<bool> CreateAsync(TDto entity);
    Task<bool> UpdateAsync(TDto entity);
}
