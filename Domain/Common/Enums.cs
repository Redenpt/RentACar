using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public enum FuelType
    {
        Gasoline = 1,
        Diesel = 2,
        Electric = 3,
        Hybrid = 4,
        LPG = 5
    }

    public enum VehicleStatus
    {
        Available = 1,
        Rented = 2,
    }

    public enum RentalStatus
    {
        Pending = 1,
        Active = 2,
        Completed = 3,
    }
}
