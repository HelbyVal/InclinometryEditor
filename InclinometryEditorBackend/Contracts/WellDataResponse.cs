namespace InclinometryEditorBackend.Contracts
{
    public record WellDataResponse(
        Guid id,
        int Num,
        double Md,
        double Inclination,
        double Azimut,
        double TVD,
        double dE,
        double dN,
        double DLS,
        double Z,
        double Y,
        double X
        );
}
