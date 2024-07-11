namespace InclinometryEditorBackend.Contracts
{
    public record WellResponse (
        Guid Id,
        string Title,
        string Description,
        DateOnly CreateDate
    );
}
