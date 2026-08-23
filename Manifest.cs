using Core.Modules.Manifest;
using Summary.WASenderApi;

[assembly: Feature(
    Id = WASenderApi.Features.WASenderApi,
    Name = WASenderApi.Localize.SOfWASenderApi,
    Description =WASenderApi.Localize.DOfWASenderApi,
    Category = WASenderApi.Public.Category,
    Dependencies = new[] { "Core.Workflows" },
    Version = "1.0.0"
)]
