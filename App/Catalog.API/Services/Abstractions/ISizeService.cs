using Catalog.LIB.Entities;

public interface ISizeService
{
	Task<Size> CreateSizeAsync(string name, string code);
	Task<bool> UpdateSizeAsync(Guid id, string name, string code);
	Task<bool> DeleteSizeAsync(Guid id);
	Task<bool> RestoreSizeAsync(Guid id);
	Task<List<Size>> GetSizesAsync();
}