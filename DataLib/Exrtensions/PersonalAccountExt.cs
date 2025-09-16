using DataLib.Entities;
using DataLib.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataLib.Extensions
{
    public static class PersonalAccountExt
    {
        public static IQueryable<PersonalAccount> WithResidents(
            this IQueryable<PersonalAccount> accounts, bool withResidents)
            => withResidents
                ? accounts.Include(a => a.Residents)
                : accounts;

        public static IQueryable<PersonalAccount> ApplyFilter(
            this IQueryable<PersonalAccount> accounts,
            FilterPersonalAccount filter)
        {
            if (filter is null)
                return accounts;

            //номер уникальный, другие фильтры не важны, всегда один результат
            if (!string.IsNullOrEmpty(filter.NumberOfPersonalAccount))
                return accounts.FindByNumberPersonalAccount(filter.NumberOfPersonalAccount);

            return accounts
                .HasResidents(filter.IsAnyResidents)
                .ActiveForDate(filter.ActiveForDate)
                .ByAddress(filter.Address)
                .ByFullname(filter.Fullname);
        }

        internal static IQueryable<PersonalAccount> HasResidents(
            this IQueryable<PersonalAccount> accounts,
            bool? hasResidents)
            => hasResidents.HasValue
                ? hasResidents.Value
                    ? accounts.Where(x => x.Residents.Any()) //хотя бы один жилец
                    : accounts.Where(x => !x.Residents.Any()) //пустые
                : accounts; //все

        internal static IQueryable<PersonalAccount> FindByNumberPersonalAccount(
            this IQueryable<PersonalAccount> accounts,
            string numberOfPersonalAccount)
        {
            int number = int.Parse(numberOfPersonalAccount);
            return accounts.Where(x => x.Id == number);
        }

        internal static IQueryable<PersonalAccount> ActiveForDate(
            this IQueryable<PersonalAccount> accounts,
            DateTime? checkActiveForDate)
            => checkActiveForDate.HasValue
                ? accounts.Where(x => x.DateActivate <= checkActiveForDate.Value &&
                    (!x.DateFinish.HasValue) || x.DateFinish.Value > checkActiveForDate)
                : accounts;

        internal static IQueryable<PersonalAccount> ByAddress(
            this IQueryable<PersonalAccount> accounts,
            string address)
            => string.IsNullOrEmpty(address)
                ? accounts
                : accounts.Where(x => x.Address.Contains(address));

        internal static IQueryable<PersonalAccount> ByFullname(
            this IQueryable<PersonalAccount> accounts,
            string fullname)
            => string.IsNullOrEmpty(fullname)
                ? accounts
                : accounts.Where(x => x.Residents.Any(r =>
                    r.LastName.Contains(fullname) ||
                    r.FirstName.Contains(fullname) ||
                    r.SecondName.Contains(fullname)));

        /// <summary>
        /// расширение сортировки без ограничения вложенности
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="sort"></param>
        /// <param name="direct"></param>
        /// <returns></returns>
        public static IQueryable<PersonalAccount> ApplySort(
            this IQueryable<PersonalAccount> accounts,
            string sort,
            string direct)
        {
            if (string.IsNullOrEmpty(sort))
                return accounts;

            string[] fields = sort.Split(',');
            string[] directionValues = direct?.Split(',');

            int directionLenth = directionValues?.Length ?? 0;
            IOrderedQueryable<PersonalAccount> accountsOrdered = null;

            for (int i = 0; i < fields.Length; ++i)
            {
                bool isAscending = i < directionLenth &&
                    directionValues[i].Equals("asc", StringComparison.OrdinalIgnoreCase);

                Expression<Func<PersonalAccount, object>> keySelector = fields[i].ToLower() switch
                {
                    "number"        => x => x.Number,
                    "address"       => x => x.Address,
                    "area"          => x => x.Area,
                    "dateactivate"  => x => x.DateActivate,
                    "datefinish"    => x => x.DateFinish,
                    _               => x => x.Id
                };

                if(i == 0)
                {
                    accountsOrdered = isAscending
                        ? accounts.OrderBy(keySelector)
                        : accounts.OrderByDescending(keySelector);
                }
                else
                {
                    accountsOrdered = isAscending
                        ? accountsOrdered.ThenBy(keySelector)
                        : accountsOrdered.ThenByDescending(keySelector);
                }
            }
            return accountsOrdered ?? accounts;
        }

        public static IQueryable<PersonalAccount> ToPage(
            this IQueryable<PersonalAccount> accounts,
            int page, int pageSize) 
            => accounts
                .Skip((page-1) * pageSize)
                .Take(pageSize);
    }
}