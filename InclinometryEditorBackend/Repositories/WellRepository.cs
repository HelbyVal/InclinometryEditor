using InclinometryEditorBackend.Entities;
using InclinometryEditorBackend.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;

namespace InclinometryEditorBackend.Repositories
{
    public class WellRepository 
    { 
        public WellRepository() 
        {
        }

        public async Task<List<Well>> Get(int userId)
        {
            using (var _dbContext = new InclinometryDBContext()) {
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
        }

        public async Task<Well> Add(Well well)
        {
            using (var _dbContext = new InclinometryDBContext())
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
}
