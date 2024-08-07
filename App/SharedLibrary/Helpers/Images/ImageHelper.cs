using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SharedLibrary.DTOs;
using SharedLibrary.Entities.Enums;

namespace SharedLibrary.Helpers.Images
{
	public class ImageHelper : IImageHelper
	{
		private const string ImgFolder = "Uploads";
		private const string ProductImagesFolder = "Product-Images";
		private const string CategoryImagesFolder = "Category-Images";

		private readonly string _sharedLibraryPath;

		public ImageHelper(IConfiguration configuration)
		{
			var projectRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "SharedLibrary");
			_sharedLibraryPath = Path.Combine(projectRoot, ImgFolder);

			Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");
			Console.WriteLine($"Shared Library Path: {_sharedLibraryPath}");

			if (!Directory.Exists(_sharedLibraryPath))
			{
				Directory.CreateDirectory(_sharedLibraryPath);
			}
		}

		public async Task<ImageUploadedDto> UploadAsync(string name, IFormFile imageFile, ImageType imageType, string folderName = null)
		{
			folderName ??= imageType switch
			{
				ImageType.Category => CategoryImagesFolder,
				ImageType.Product => ProductImagesFolder,
				_ => ProductImagesFolder
			};

			var uploadPath = Path.Combine(_sharedLibraryPath, folderName);

			if (!Directory.Exists(uploadPath))
				Directory.CreateDirectory(uploadPath);

			var fileExtension = Path.GetExtension(imageFile.FileName);
			var safeFileName = ReplaceInvalidChars(name);
			var newFileName = $"{safeFileName}_{DateTime.Now:yyyyMMddHHmmssfff}{fileExtension}";
			var filePath = Path.Combine(uploadPath, newFileName);

			try
			{
				await using var stream = new FileStream(filePath, FileMode.Create);
				await imageFile.CopyToAsync(stream);
				Console.WriteLine($"File uploaded to: {filePath}");
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Dosya yüklenirken bir hata oluştu", ex);
			}

			return new ImageUploadedDto
			{
				FullName = $"{folderName}/{newFileName}"
			};
		}

		public void Delete(string imageName)
		{
			var filePath = Path.Combine(_sharedLibraryPath, imageName);
			if (File.Exists(filePath))
				File.Delete(filePath);
		}

		private string ReplaceInvalidChars(string fileName)
		{
			var invalidChars = Path.GetInvalidFileNameChars().Concat(new[]
			{
				'İ', 'ı', 'Ğ', 'ğ', 'Ü', 'ü', 'ş', 'Ş', 'Ö', 'ö', 'Ç', 'ç', 'é', '!', '\'', '^', '+', '%', '/', '(', ')',
				'=', '?', '_', '*', 'æ', 'ß', '@', '€', '<', '>', '#', '$', '½', '{', '[', ']', '}', '\\', '|', '~', '¨',
				',', ';', '`', '.', ':', ' '
			});
			foreach (var c in invalidChars) fileName = fileName.Replace(c.ToString(), string.Empty);

			return fileName;
		}
	}
}
