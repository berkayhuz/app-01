using Authentication.LIB.Entities;
using System.Text.Json;

public static class JsonDataHelper
{
    public static async Task<List<City>> GetCitiesAsync()
    {
        using (var stream = new FileStream("JsonFiles/il.json", FileMode.Open, FileAccess.Read))
        {
            return await JsonSerializer.DeserializeAsync<List<City>>(stream);
        }
    }

    public static async Task<List<District>> GetDistrictsAsync()
    {
        using (var stream = new FileStream("JsonFiles/ilce.json", FileMode.Open, FileAccess.Read))
        {
            return await JsonSerializer.DeserializeAsync<List<District>>(stream);
        }
    }
}