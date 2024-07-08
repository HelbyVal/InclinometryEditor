namespace InclinometryEditorBackend.Contracts
{
    public record WellRequest(
        string Title,
        string Discription,
        DateOnly CreateDate
    );
}
