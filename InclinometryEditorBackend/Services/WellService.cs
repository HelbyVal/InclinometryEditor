using InclinometryEditorBackend.Models;
using InclinometryEditorBackend.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace InclinometryEditorBackend.Services
{
    public class WellService
    {
        private readonly WellDataRepository _wellDataRepository = new WellDataRepository();
        private readonly WellRepository _wellRepository = new WellRepository();

        public WellService() 
        {

        }


        public async Task<Well>AddWell(int userId, string discription, string title, DateOnly CreateDate)
        {
            Well well = new Well()
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = discription,
                CreateDate = CreateDate,
                UserId = userId
            };

            var res = await _wellRepository.Add(well);

            WellData firstData = WellData.CreateFirst(userId, well.Id);
            await _wellDataRepository.Add(firstData);

            return res;

        }
    }
}
