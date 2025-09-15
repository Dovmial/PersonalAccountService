using DataLib.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataLib.Extensions
{
    public static class PersonalAccountExt
    {
        public static IQueryable<PersonalAccount> WithResidents(
            this IQueryable<PersonalAccount> accounts, bool withResidents)
            => withResidents
                ? accounts.Include(a => a.Residents)
                : accounts;
    }
}
