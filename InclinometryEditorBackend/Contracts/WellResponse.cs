namespace InclinometryEditorBackend.Contracts
{
    public record WellResponse (
        Guid Id,
        string Title,
        string Discription,
        string CreateDate
    );
}
