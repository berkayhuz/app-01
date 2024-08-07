using Microsoft.AspNetCore.Http;
using SharedLibrary.DTOs;
using SharedLibrary.Entities.Enums;

namespace SharedLibrary.Helpers.Images
{
	public interface IImageHelper
	{
		Task<ImageUploadedDto> UploadAsync(string name, IFormFile imageFile, ImageType imageType, string folderName = null);
		void Delete(string imageName);
	}
}