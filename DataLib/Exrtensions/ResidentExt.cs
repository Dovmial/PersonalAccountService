using DataLib.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataLib.Extensions
{
    public static class ResidentExt
    {
        public static IQueryable<Resident> WithPersonalAccounts(
            this IQueryable<Resident> accounts, bool withResidents)
            => withResidents
                ? accounts.Include(r => r.PersonalAccounts)
                : accounts;
    }
}
