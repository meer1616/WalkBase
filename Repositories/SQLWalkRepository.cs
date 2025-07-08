using Authentication.Data;
using Authentication.Models.Domain;
using Authentication.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly AuthenticationDBContext dbcontext;

        public SQLWalkRepository(AuthenticationDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbcontext.Walks.AddAsync(walk);
            await dbcontext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllWalkAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1,int pageSize = 10)
        {
            var walks= dbcontext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                if(filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }

                //else if (filterOn.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase) && double.TryParse(filterQuery, out double length))
                //{
                //    walks = walks.Where(x => x.LengthInKm == length);
                //}
                //else if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                //{
                //    walks = walks.Where(x => x.Description.Contains(filterQuery, StringComparison.OrdinalIgnoreCase));
                //}
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks=isAscending?walks.OrderBy(x=> x.Name):walks.OrderByDescending(x=>x.Name);
                }
                else if(sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //Pagination

            var skipResults = (pageNumber - 1) * pageSize;
            walks = walks.Skip(skipResults).Take(pageSize);

            return await walks.ToListAsync();
            //return await dbcontext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbcontext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);

        }
        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var walkDomainModel = await dbcontext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDomainModel == null)
            {
                throw new InvalidOperationException($"Walk with ID {id} not found.");
            }
            walkDomainModel.Name = walk.Name;
            walkDomainModel.LengthInKm = walk.LengthInKm;
            walkDomainModel.Description = walk.Description;
            walkDomainModel.WalkImgUrl = walk.WalkImgUrl;
            walkDomainModel.RegionId = walk.RegionId;
            walkDomainModel.DifficultyId = walk.DifficultyId;
            await dbcontext.SaveChangesAsync();
            return walkDomainModel;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walkDomainModelExist = await dbcontext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDomainModelExist == null)
            {
                return null;
            }
            dbcontext.Walks.Remove(walkDomainModelExist);
            await dbcontext.SaveChangesAsync();
            return walkDomainModelExist;
        }
    }
}