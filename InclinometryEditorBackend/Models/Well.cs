using InclinometryEditorBackend.Contracts;
using InclinometryEditorBackend.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace InclinometryEditorBackend.Models
{
    public class Well
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly CreateDate { get; set; }

        public WellResponse ToResponse()
        {
            return new WellResponse(Id,Title,Description,CreateDate);
        }
        public WellEntity ToEntity()
        {
            return new WellEntity()
            {
                Id = Id,
                Title = Title,
                Description = Description,
                CreateDate = CreateDate,
                UserId = UserId
            };
        }

        public static Well ParseFromEntity(WellEntity e)
        {
            return new Well()
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                CreateDate = e.CreateDate,
            };
        }
    }
}
