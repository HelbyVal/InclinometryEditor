using InclinometryEditorBackend.Models;
using InclinometryEditorBackend.Repositories;

namespace InclinometryEditorBackend.Services
{
    public class WellDataService
    {
        private readonly WellDataRepository _wellDataRepository = new WellDataRepository();
        private readonly WellRepository _wellRepository = new WellRepository();
        public WellDataService() { }

        public async Task<WellData> AddWellData(int UserId, Guid WellId, double Inclination, double Azimut, double Md)
        {
            var prevWellData = await _wellDataRepository.GetLast(WellId, UserId);

            var newWellData = WellData.Create(UserId, WellId, Inclination, Azimut, Md, prevWellData);
            
            return await _wellDataRepository.Add(newWellData);

        }
    }
}
