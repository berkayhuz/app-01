using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Catalog.API.Helpers.Images
{
	public class FileUploadOperation : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			if (operation == null || operation.OperationId == null)
			{
				return;
			}

			if (operation.OperationId.ToLower() == "apicategorycreatecategorypost")
			{
				operation.Parameters.Clear();
				operation.RequestBody = new OpenApiRequestBody
				{
					Content = new Dictionary<string, OpenApiMediaType>
					{
						["multipart/form-data"] = new OpenApiMediaType
						{
							Schema = new OpenApiSchema
							{
								Type = "object",
								Properties = new Dictionary<string, OpenApiSchema>
								{
									["name"] = new OpenApiSchema { Type = "string" },
									["createdBy"] = new OpenApiSchema { Type = "string" },
									["imageFile"] = new OpenApiSchema { Type = "string", Format = "binary" }
								},
								Required = new HashSet<string> { "name", "createdBy", "imageFile" }
							}
						}
					}
				};
			}
		}
	}
}