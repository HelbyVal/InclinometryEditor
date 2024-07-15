﻿using InclinometryEditorBackend.Models;
using Microsoft.AspNetCore.SignalR;

namespace InclinometryEditorBackend.Entities
{
    public class WellDataEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid WellEntityId { get; set; }
        public int Num {  get; set; }
        public double MD { get; set; }
        public double Inclination {  get; set; }
        public double Azimut { get; set; }
        public double TVD { get; set; }
        public double dE { get; set; }
        public double dN { get; set; }
        public double DLS { get; set; }
        public double Z { get; set; }
        public double Y { get; set; }
        public double X { get; set; }
        public WellEntity? WellEntity { get; set; }
    }
}
