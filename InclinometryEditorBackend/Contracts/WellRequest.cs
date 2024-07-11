namespace InclinometryEditorBackend.Contracts
{
    public record WellRequest(
        string Title,
        string Description,
        DateOnly CreateDate
    );
}
