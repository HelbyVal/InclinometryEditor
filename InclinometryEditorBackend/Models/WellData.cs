using InclinometryEditorBackend.Contracts;
using InclinometryEditorBackend.Entities;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics.Eventing.Reader;
using System.Numerics;
using System.Runtime.Intrinsics.Arm;
using static System.Math;

namespace InclinometryEditorBackend.Models
{
    public class WellData
    {
        public string UserId { get; }
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
                        string UserId,
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
            this.WellId = WellId;
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

        public static WellData CreateFirst(string userId, Guid wellId)
        {
            return new WellData(Guid.NewGuid(), wellId, userId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        public static WellData Create(string userId, Guid wellId ,double Inclination, double Azimut, double Md, WellData prevWellData)
        {
            double Z = Cos(Inclination);
            double Y = Sin(Inclination) * Cos(Azimut);
            double X = Sin(Inclination) * Sin(Azimut);

            double prevI = prevWellData.Inclination;
            double prevA = prevWellData.Azimut;
            double prevMd = prevWellData.MD;

            double Dl = Acos(Cos(Inclination - prevI)) - Sin(Inclination) * Sin(prevI) * (1 - Cos(Azimut - prevA));
            double dMd = Md - prevMd;
            double DLS = Dl - dMd;

            double RF = 1;
            if (Dl != 0)
            {
                RF = 2 / Dl * Tan(Dl / 2);
            }

            double C = RF * dMd / 2;

            double TVD = prevWellData.TVD + C * (Z + prevWellData.Z);
            double dN = prevWellData.dN + C * (Y + prevWellData.Y);
            double dE = prevWellData.dE + C * (X + prevWellData.X);

            WellData result = new WellData(Guid.NewGuid(),
                                           wellId,
                                           userId,
                                           prevWellData.Num + 1,
                                           Md,
                                           Inclination,
                                           Azimut,
                                           TVD,
                                           dE,
                                           dN,
                                           DLS,
                                           Z,
                                           Y,
                                           X);
           
            return result;
        }

        public WellDataEntity ToEntity()
        {
            return new WellDataEntity()
            {
                Id = Id,
                WellEntityId = WellId,
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

        public WellDataResponse ToResponse()
        {
            return new WellDataResponse(
                Id,
                Num,
                MD,
                Inclination,
                Azimut,
                TVD,
                dE,
                dN,
                DLS,
                Z, Y, X
                );
        }
    }
}
