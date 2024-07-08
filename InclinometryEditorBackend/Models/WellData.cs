using InclinometryEditorBackend.Entities;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics.Eventing.Reader;

namespace InclinometryEditorBackend.Models
{
    public class WellData
    {
        public int UserId { get; }
        public Guid Id { get; }
        public int Num { get; } 
        public double MD { get; }
        public double Inclination { get; }
        public double Azimut { get; }
        public double TVD { get; }
        public double dE { get; }
        public double dN { get; }
        public double DLS { get; }
        public double Z { get; }
        public double Y { get; }
        public double X { get; }

        public WellData(Guid Id,
                        int UserId,
                        int Num,
                        double MD,
                        double Inclination,
                        double Azimut,
                        double TVD,
                        double dE,
                        double dN,
                        double DLS,
                        double Z,
                        double Y,
                        double X)
        {
            this.Id = Id;
            this.UserId = UserId;
            this.Num = Num;
            this.MD = MD;
            this.Inclination = Inclination;
            this.Azimut = Azimut;
            this.TVD = TVD;
            this.dE = dE;
            this.dN = dN;
            this.DLS = DLS;
            this.Z = Z;
            this.Y = Y;
            this.X = X;
        }

        public static WellData Create(double Inclination, double Azimut, double Md)
        {
            return null;
        }

        public WellDataEntity ToEntity()
        {
            return new WellDataEntity()
            {
                Id = Id,
                UserId = UserId,
                Num = Num,
                MD = MD,
                Inclination = Inclination,
                Azimut = Azimut,
                TVD = TVD,
                dE = dE,
                dN = dN,
                DLS = DLS,
                Z = Z,
                Y = Y,
                X = X
            };
        }
    }
}
