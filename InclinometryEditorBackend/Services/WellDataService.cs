using InclinometryEditorBackend.Models;
using InclinometryEditorBackend.Repositories;

namespace InclinometryEditorBackend.Services
{
    public class WellDataService
    {
        private readonly WellDataRepository _wellDataRepository = new WellDataRepository();
        private readonly WellRepository _wellRepository = new WellRepository();
        public WellDataService() { }

        public async Task<WellData> AddWellData(int UseId, int WellId)
        {

        }
    }
}
