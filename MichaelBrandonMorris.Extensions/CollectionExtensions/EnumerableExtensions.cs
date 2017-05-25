﻿using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;

namespace MichaelBrandonMorris.Extensions.CollectionExtensions
{
    /// <summary>
    /// Provides useful extensions for classes that implement 
    /// <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Whether or not the <see cref="IEnumerable{T}"/> is empty.
        /// </summary>
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }

        /// <summary>
        /// Whether or not the <see cref="IEnumerable{T}"/> is null or empty.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || enumerable.IsEmpty();
        }

        /// <summary>
        /// Whether or not the <see cref="IEnumerable{T}"/> has multiple items.
        /// </summary>
        public static bool Multiple<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Count() > 1;
        }

        /// <summary>
        /// Converts this <see cref="IEnumerable{ExpandoObject}"/> to a 
        /// <see cref="DataTable"/>.
        /// </summary>
        public static DataTable ToDataTable(
            this IEnumerable<ExpandoObject> expandoObjectEnumerable)
        {
            var expandoObjectArray =
                expandoObjectEnumerable as ExpandoObject[] ??
                expandoObjectEnumerable.ToArray();

            if (expandoObjectArray.IsNullOrEmpty()) return null;
            var dataTable = new DataTable();

            var firstExpandoObjectAsDictionary =
                (IDictionary<string, object>) expandoObjectArray.First();

            var keys = firstExpandoObjectAsDictionary.Keys;
            foreach (var key in keys)
            {
                dataTable.Columns.Add(key);
            }

            foreach (var expandoObject in expandoObjectArray)
            {
                var expandoObjectAsDictionary =
                    (IDictionary<string, object>) expandoObject;

                var expandoObjectValuesAsArray =
                    expandoObjectAsDictionary.Values.ToArray();

                dataTable.Rows.Add(expandoObjectValuesAsArray);
            }

            return dataTable;
        }

        /// <summary>
        /// Whether or not the <see cref="IEnumerable{String}"/> contains the 
        /// specified string, ignoring case.
        /// </summary>
        /// <param name="strings"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(
            this IEnumerable<string> strings, string s)
        {
            return strings.Any(str => str.EqualsOrdinalIgnoreCase(s));
        }
    }
}