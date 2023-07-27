using Patrick_WebAPI.Models.Domain;

namespace Patrick_WebAPI.Repositories
{
	public interface IWalkRepository
	{
		Task<Walk> CreateAsync(Walk walk);

		Task<List<Walk>> GetAllAsync();

		Task<Walk?> GetByIdAsync(Guid id); 
	}
}
