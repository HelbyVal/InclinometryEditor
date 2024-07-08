using InclinometryEditorBackend.Entities;
using InclinometryEditorBackend.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;

namespace InclinometryEditorBackend.Repositories
{
    public class WellRepository
    {
        private readonly InclinometryDBContext _dbContext;
        public WellRepository(InclinometryDBContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<Well>> Get(int userId)
        {
            var WellEntities = await _dbContext.Wells.Where(x => x.UserId == userId)
                                                     .AsNoTracking()
                                                     .ToListAsync();

            var result = WellEntities
                .Select(x => new Well()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CreateDate = x.CreateDate,
                })
                .ToList();

            return result;
        }

        public async Task<Well> Add(Well well)
        {
            var wellEntity = new WellEntity()
            {
                Id = well.Id,
                Title = well.Title,
                Description = well.Description,
                CreateDate = well.CreateDate,
                UserId = well.UserId
            };
            await _dbContext.Wells.AddAsync(wellEntity);
            await _dbContext.SaveChangesAsync();

            return well;
        }
    }
}
