using InclinometryEditorBackend.Entities;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics.Eventing.Reader;
using System.Numerics;

namespace InclinometryEditorBackend.Models
{
    public class WellData
    {
        public int UserId { get; }
        public Guid WellId { get; }
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
                        Guid WellId,
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

        public static WellData CreateFirst(int userId, Guid wellId)
        {
            return new WellData(Guid.NewGuid(), wellId, userId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        public static WellData Create(int userId, int num, Guid wellId ,double Inclination, double Azimut, double Md, WellData? prevWellData)
        {
            double Z = Math.Cos(Inclination);
            double Y = Math.Sin(Inclination) * Math.Cos(Azimut);
            double X = Math.Sin(Inclination) * Math.Sin(Azimut);


            

            


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
