using System;

namespace DataLib.Filters
{
    public record FilterPersonalAccount
        (
            string NumberOfPersonalAccount,
            DateTime? ActiveForDate,
            string Address,
            bool? IsAnyResidents,
            string Fullname
        );
}
