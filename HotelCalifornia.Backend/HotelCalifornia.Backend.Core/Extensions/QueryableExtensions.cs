﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace HotelCalifornia.Backend.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> ASet, bool ACondition, 
            Expression<Func<T, bool>> AFunc)
        {
            return ACondition ? ASet.Where(AFunc) : ASet;
        }

        public static IQueryable<T> WhereIfElse<T>(this IQueryable<T> ASet, bool ACondition, 
            Expression<Func<T, bool>> AIfFunc, Expression<Func<T, bool>> AElseFunc)
        {
            return ASet.Where(ACondition ? AIfFunc : AElseFunc);
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> AExpression)
        {
            var LCandidateExpr = AExpression.Parameters[0];
            var LBody = Expression.Not(AExpression.Body);
            return Expression.Lambda<Func<T, bool>>(LBody, LCandidateExpr);
        }
    }
}
