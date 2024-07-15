namespace InclinometryEditorBackend.Entities
{
    public class WellEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly CreateDate { get; set; }
        public List<WellDataEntity>? WellDataEntities { get; set; }
    }
}
