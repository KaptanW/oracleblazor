mkdir -p src/OracleBlazor.Core/Entities
mkdir -p src/OracleBlazor.Application/Interfaces src/OracleBlazor.Application/DTOs src/OracleBlazor.Application/Validators
mkdir -p src/OracleBlazor.Infrastructure/Persistence src/OracleBlazor.Infrastructure/Repositories
mkdir -p src/OracleBlazor.Api/Controllers src/OracleBlazor.Api/Mappings

touch src/OracleBlazor.Core/Entities/Asset.cs
touch src/OracleBlazor.Application/Interfaces/IAssetService.cs
touch src/OracleBlazor.Application/DTOs/AssetDtos.cs
touch src/OracleBlazor.Application/Validators/AssetValidators.cs
touch src/OracleBlazor.Infrastructure/Persistence/AppDbContext.cs
touch src/OracleBlazor.Infrastructure/Repositories/AssetRepository.cs
touch src/OracleBlazor.Api/Controllers/AssetsController.cs
touch src/OracleBlazor.Api/Mappings/MappingProfile.cs

dotnet add src/OracleBlazor.Infrastructure package Oracle.ManagedDataAccess.Core
dotnet add src/OracleBlazor.Infrastructure package Microsoft.EntityFrameworkCore.Design