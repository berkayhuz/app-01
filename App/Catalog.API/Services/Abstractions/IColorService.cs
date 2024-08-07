using Catalog.LIB.Entities;

public interface IColorService
{
	Task<Color> CreateColorAsync(string name, string code);
	Task<bool> UpdateColorAsync(Guid id, string name, string code);
	Task<bool> DeleteColorAsync(Guid id);
	Task<bool> RestoreColorAsync(Guid id);
	Task<List<Color>> GetColorsAsync();
}