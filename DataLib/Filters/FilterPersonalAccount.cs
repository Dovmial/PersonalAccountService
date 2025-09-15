using System;

namespace DataLib.Filters
{
    public record FilterPersonalAccount
        (
            string numberOfPersonalAccount,
            DateTime? ActiveForDate,
            string Address,
            bool? IsAnyResidents,
            string Fullname
        );
}
