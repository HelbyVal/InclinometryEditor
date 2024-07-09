namespace InclinometryEditorBackend.Contracts
{
    public record WellDataRequest(
        Guid WellId,
        double Inclination,
        double Azimut,
        double Md
        );
}
