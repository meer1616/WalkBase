using Authentication.Models.Domain;
using Authentication.Models.DTO;
using Microsoft.AspNetCore.Mvc;


namespace Authentication.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllWalkAsync(string? filterOn=null, string? filterQuery=null, string? sortBy=null, bool isAscending=true, int pageNumber =1, int pageSize=10);

        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk?> UpdateAsync(Guid id, Walk walk);

        Task<Walk?> DeleteAsync(Guid id);
    }
}
