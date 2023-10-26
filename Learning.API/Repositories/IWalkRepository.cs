using Learning.API.Models.Domain;
using Learning.API.Models.DTO;

namespace Learning.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null,
            bool isAscending = true, int pageNumber = 1,int pageSize = 1000);
        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk?> updateAsync(Guid id, Walk walk);

        Task<Walk?> deleteasync(Guid id);
    }
}
