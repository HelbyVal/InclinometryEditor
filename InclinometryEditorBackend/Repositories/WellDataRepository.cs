using InclinometryEditorBackend.Entities;
using InclinometryEditorBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace InclinometryEditorBackend.Repositories
{
    public class WellDataRepository
    {
        private readonly InclinometryDBContext _dbContext;
        public WellDataRepository(InclinometryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WellData>> Get(int WellId, int UserId)
        {
            var WellDataEntities = await _dbContext.WellDatas.Where(x => x.WellEntityId == WellId)
                                                     .AsNoTracking()
                                                     .ToListAsync();

            var result = WellDataEntities
                .Select(x => new WellData(
                    x.Id,
                    x.UserId,
                    x.Num,
                    x.MD,
                    x.Inclination,
                    x.Azimut,
                    x.TVD,
                    x.dE,
                    x.dN,
                    x.DLS,
                    x.Z,
                    x.Y,
                    x.X))
                .ToList();

            return result;
        }

        public async Task<List<WellData>> GetPrevious(int WellId, int prev, int userId)
        {
            var WellDataEntities = await _dbContext.WellDatas.Where(x => x.WellEntityId == WellId && x.Num == prev - 1)
                                                     .AsNoTracking()
                                                     .ToListAsync();

            var result = WellDataEntities
                .Select(x => new WellData(
                    x.Id,
                    x.UserId,
                    x.Num,
                    x.MD,
                    x.Inclination,
                    x.Azimut,
                    x.TVD,
                    x.dE,
                    x.dN,
                    x.DLS,
                    x.Z,
                    x.Y,
                    x.X))
                .ToList();

            return result;
        }
        
        public async Task<WellData> Add(WellData wellData)
        {
            var wellDataEntity = wellData.ToEntity();

            await _dbContext.AddAsync(wellDataEntity);
            await _dbContext.SaveChangesAsync();

            return wellData;
        }
    }

}
