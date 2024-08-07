﻿using InclinometryEditorBackend.Entities;
using InclinometryEditorBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace InclinometryEditorBackend.Repositories
{
    public class WellDataRepository
    {
        public WellDataRepository()
        {
        }

        public async Task<List<WellData>> Get(Guid WellId, string UserId)
        {
            using (var _dbContext = new InclinometryDBContext())
            {

                var WellDataEntities = await _dbContext.WellDatas.Where(x => x.WellEntityId == WellId)
                                                                 .AsNoTracking()
                                                                 .ToListAsync();

                var result = WellDataEntities
                    .Select(x => new WellData(
                        x.Id,
                        x.WellEntityId,
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
                    .OrderBy(x => x.Num)
                    .ToList();

                return result;
            }
        }

        public async Task<WellData?> GetPrevious(Guid WellId, int prev, int userId)
        {
            using (var _dbContext = new InclinometryDBContext())
            {
                var WellDataEntities = await _dbContext.WellDatas.Where(x => x.WellEntityId == WellId && x.Num == prev)
                                                     .AsNoTracking()
                                                     .ToListAsync();

                var result = WellDataEntities
                    .Select(x => new WellData(
                        x.Id,
                        x.WellEntityId,
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
                    .LastOrDefault();

                return result;
            }
        }

        public async Task<WellData?> GetLast(Guid WellId, string userId)
        {
            using (var _dbContext = new InclinometryDBContext())
            {
                var WellDataEntities = await _dbContext.WellDatas.Where(x => x.WellEntityId == WellId && x.UserId == userId)
                                                     .AsNoTracking()
                                                     .ToListAsync();

                var result = WellDataEntities
                    .OrderBy(x => x.Num)
                    .Select(x => new WellData(
                        x.Id,
                        x.WellEntityId,
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
                    .LastOrDefault();

                return result;
            }
        }

        public async Task<WellData> Add(WellData wellData)
        {
            using (var _dbContext = new InclinometryDBContext())
            {
                var wellDataEntity = wellData.ToEntity();

                await _dbContext.AddAsync(wellDataEntity);
                await _dbContext.SaveChangesAsync();

                return wellData;
            }
        }

        internal async Task<Guid> DeleteLast(string userId, Guid wellId)
        {
            using (var _dbContext = new InclinometryDBContext())
            {
                var res = await _dbContext.WellDatas.Where(x => x.WellEntityId == wellId && x.UserId == userId).OrderBy(x => x.Num).LastOrDefaultAsync();
                _dbContext.WellDatas.Remove(res);
                _dbContext.SaveChanges();
                                          
                return wellId;
            }
        }
    }

}
