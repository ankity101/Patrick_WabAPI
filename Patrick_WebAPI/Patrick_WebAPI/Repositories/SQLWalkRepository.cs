using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Patrick_WebAPI.Data;
using Patrick_WebAPI.Models.Domain;
using System.Data.Entity;


namespace Patrick_WebAPI.Repositories
{
	public class SQLWalkRepository : IWalkRepository
	{
		private readonly NZWalksDbContext dbContext;

		public SQLWalkRepository( NZWalksDbContext dbContext)
		{
			 this.dbContext = dbContext;
		}
		public async Task<Walk> CreateAsync(Walk walk)
		{
			await dbContext.Walks.AddAsync(walk);
			await dbContext.SaveChangesAsync();
			return walk;
		}

		public async Task<List<Walk>> GetAllAsync()
		{
			return await dbContext.Walks.ToListAsync();
		}

		public async Task<Walk?> GetByIdAsync(Guid id)
		{
			 
			return await dbContext.Walks.FirstOrDefaultAsync(x=> x.ID == id);
		}
	} 
}
