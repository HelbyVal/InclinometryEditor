using InclinometryEditorBackend.Entities;
using InclinometryEditorBackend.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using System.Reflection.Metadata.Ecma335;

namespace InclinometryEditorBackend.Repositories
{
    public class WellRepository 
    { 
        public WellRepository() 
        {
        }

        public async Task<List<Well>> Get(string userId)
        {
            using (var _dbContext = new InclinometryDBContext()) {
                var WellEntities = await _dbContext.Wells.Where(x => x.UserId == userId)
                                                         .AsNoTracking()
                                                         .ToListAsync();
                var result = WellEntities
                    .Select(Well.ParseFromEntity)
                    .ToList();

                return result;
            }
        }

        public async Task<Well> Add(Well well)
        {
            using (var _dbContext = new InclinometryDBContext())
            {
                var wellEntity = well.ToEntity();
                await _dbContext.Wells.AddAsync(wellEntity);
                await _dbContext.SaveChangesAsync();

                return well;
            }
        }

        public async Task<Well> Update(Guid idWell, string title, string description, DateOnly createDate, string UserId)
        {
            using (var _dbContext = new InclinometryDBContext())
            {
                await _dbContext.Wells.Where(x => x.Id == idWell && x.UserId == UserId)
                                      .ExecuteUpdateAsync(s => s
                                            .SetProperty(b => b.Title, b => title)
                                            .SetProperty(b => b.Description, b => description)
                                            .SetProperty(b => b.CreateDate, b => createDate));
                
                return new Well() 
                { 
                    Id = idWell,
                    Title = title,
                    Description = description,
                    CreateDate = createDate
                };
            }
        }

        internal async Task<Guid> Delete(Guid wellId, string userId)
        {
            using (var _dbContext = new InclinometryDBContext())
            {
                await _dbContext.Wells.Where(x => x.Id == wellId && x.UserId == userId).ExecuteDeleteAsync();
                return wellId;
            }
        }
    }
}
