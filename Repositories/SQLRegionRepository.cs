using Authentication.Data;
using Authentication.Models.Domain;
using Authentication.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repositories
{

    public class SQLRegionRepository : IRegionRepository
    {
        private readonly AuthenticationDBContext dbContext;
        private readonly ILogger<SQLRegionRepository> _logger;

        public SQLRegionRepository(AuthenticationDBContext dbContext, ILogger<SQLRegionRepository> logger)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all regions from the database.");
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> CreateRegionAsync(Region regionDomainModel)
        {
            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();
            return regionDomainModel;
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {
                throw new InvalidOperationException($"Region with ID {id} not found.");
            }

            regionDomainModel.Code = region.Code;
            regionDomainModel.Name = region.Name;
            regionDomainModel.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return regionDomainModel;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var regionDomainModelExist = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModelExist == null)
            {
                return null;
            }

            dbContext.Regions.Remove(regionDomainModelExist);
            await dbContext.SaveChangesAsync();
            return regionDomainModelExist;
        }
    }
}
