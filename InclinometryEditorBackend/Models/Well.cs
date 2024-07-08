using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace InclinometryEditorBackend.Models
{
    public class Well
    {
        const int MAX_TITLE_LENGHT = 25;

        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }

    }
}
