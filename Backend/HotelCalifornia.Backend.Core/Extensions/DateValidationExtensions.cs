using System;
using FluentValidation;
using HotelCalifornia.Backend.Core.Models;

namespace HotelCalifornia.Backend.Core.Extensions
{
    public static class DateValidationExtensions
    {
        public static IRuleBuilderOptions<T, DateTime?> IsSameOrLaterThanDate<T>(this IRuleBuilder<T, DateTime?> ARuleBuilder, DateTime ABeforeDate)
            =>  ARuleBuilder.Must(ADate => ADate == null || ADate.Value >= ABeforeDate);

        public static IRuleBuilderOptions<T, DateTime> IsSameOrLaterThanDate<T>(this IRuleBuilder<T, DateTime> ARuleBuilder, DateTime ABeforeDate)
            => ARuleBuilder.Must(ADate => ADate >= ABeforeDate);

        public static IRuleBuilderOptions<T, DateRangeValidator> IsValidDateRange<T>(this IRuleBuilder<T, DateRangeValidator> ARuleBuilder)
            =>  ARuleBuilder.Must(ADate => ADate.EndDate == null || ADate.StartDate <= ADate.EndDate);
    }
}