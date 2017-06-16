﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MichaelBrandonMorris.Extensions.CollectionExtensions
{
    /// <summary>
    ///     Provides useful extensions for classes that implement
    ///     <see cref="IList{T}" />.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        ///     Allows use of OrderBy on <see cref="IList{T}" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="list"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IList<TResult> OrderBy<T, TResult>(
            this IList<TResult> list,
            Expression<Func<TResult, T>> expression)
        {
            return list.AsQueryable().OrderBy(expression).ToList();
        }

        /// <summary>
        ///     Allows use of OrderBy on <see cref="IList{T}" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IList<T> OrderBy<T>(
            this IList<T> list,
            Func<T, object> predicate)
        {
            return list.AsEnumerable().OrderBy(predicate).ToList();
        }

        /// <summary>
        ///     Returns the item from the end of the <see cref="IList{T}" />.
        /// </summary>
        public static T Peek<T>(this IList<T> list)
        {
            return list.Any() ? list.Last() : default(T);
        }

        /// <summary>
        ///     Removes and returns the item from the end of the
        ///     <see cref="IList{T}" />.
        /// </summary>
        public static T Pop<T>(this IList<T> list)
        {
            var t = list.Last();
            list.Remove(t);
            return t;
        }

        /// <summary>
        ///     Adds an item to the end of the <see cref="IList{T}" />.
        /// </summary>
        public static void Push<T>(this IList<T> list, T t)
        {
            list.Add(t);
        }

        /// <summary>
        ///     Projects each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of
        ///     <see>
        ///         <cref>source</cref>
        ///     </see>
        ///     .
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The type of the value returned by
        ///     <see>
        ///         <cref>selector</cref>
        ///     </see>
        ///     .
        /// </typeparam>
        /// <param name="source">
        ///     A sequence of values to invoke a transform function on.
        /// </param>
        /// <param name="selector">
        ///     A transform function to apply to each element.
        /// </param>
        /// <returns></returns>
        public static IList<TResult> Select<TSource, TResult>(
            this IList<TSource> source,
            Func<TSource, TResult> selector)
        {
            return source.AsEnumerable().Select(selector).ToList();
        }

        /// <summary>
        ///     Returns the shuffled <see cref="IList{T}" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            var random = new Random();
            var n = list.Count;

            for (var i = 0; i < n - 2; i++)
            {
                var j = random.Next(i, n);
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }

            return list;
        }

        /// <summary>
        ///     Returns a specified number of contiguous elements from the
        ///     start of a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of source.
        /// </typeparam>
        /// <param name="source">
        ///     The sequence to return elements from.
        /// </param>
        /// <param name="count">
        ///     The number of elements to return.
        /// </param>
        /// <returns>
        ///     An <see cref="IList{T}" /> that contains the specified number 
        ///     of elements from the start of the input sequence.
        /// </returns>
        public static IList<TSource> Take
            <TSource>(this IList<TSource> source, int count)
        {
            return source.AsEnumerable().Take(count).ToList();
        }

        /// <summary>
        ///     Allows use of Where on <see cref="IList{T}" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IList<T> Where<T>(
            this IList<T> list,
            Func<T, bool> predicate)
        {
            return list.AsEnumerable().Where(predicate).ToList();
        }
    }
}