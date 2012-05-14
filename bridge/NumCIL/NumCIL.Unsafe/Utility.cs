﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumCIL.Generic;
using System.Reflection;

namespace NumCIL.Unsafe
{
    /// <summary>
    /// Utility functions for unsafe methods
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Gets a value describing if unsafe methods are supported
        /// </summary>
        public static bool SupportsUnsafe
        {
            get
            {
                try
                {
                    return DoTest() == 1;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets a <see cref="System.Reflection.MethodInfo"/> instance for the apply operation, bound to the data type, but unbound in the operand type.
        /// Returns null if the data type is not supported.
        /// </summary>
        /// <typeparam name="T">The type of data to operate on</typeparam>
        /// <returns>A <see cref="System.Reflection.MethodInfo"/> instance, bound to the data type, but unbound in the operand type, or null if no such method exists</returns>
        public static MethodInfo GetBinaryApply<T>()
        {
            if (!typeof(T).IsPrimitive || !SupportsUnsafe)
                return null;

            string name = "UFunc_Op_Inner_Binary_Flush_" + typeof(T).Name.Replace(".", "_");
            return typeof(NumCIL.Unsafe.Apply).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic);
        }

        /// <summary>
        /// Gets a <see cref="System.Reflection.MethodInfo"/> instance for the apply operation, bound to the data type, but unbound in the operand type.
        /// Returns null if the data type is not supported.
        /// </summary>
        /// <typeparam name="T">The type of data to operate on</typeparam>
        /// <returns>A <see cref="System.Reflection.MethodInfo"/> instance, bound to the data type, but unbound in the operand type, or null if no such method exists</returns>
        public static MethodInfo GetUnaryApply<T>()
        {
            if (!typeof(T).IsPrimitive || !SupportsUnsafe)
                return null;

            string name = "UFunc_Op_Inner_Unary_Flush_" + typeof(T).Name.Replace(".", "_");
            return typeof(NumCIL.Unsafe.Apply).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic);
        }

        /// <summary>
        /// Gets a <see cref="System.Reflection.MethodInfo"/> instance for the apply operation, bound to the data type, but unbound in the operand type.
        /// Returns null if the data type is not supported.
        /// </summary>
        /// <typeparam name="T">The type of data to operate on</typeparam>
        /// <returns>A <see cref="System.Reflection.MethodInfo"/> instance, bound to the data type, but unbound in the operand type, or null if no such method exists</returns>
        public static MethodInfo GetNullaryApply<T>()
        {
            if (!typeof(T).IsPrimitive || !SupportsUnsafe)
                return null;

            string name = "UFunc_Op_Inner_Nullary_Flush_" + typeof(T).Name.Replace(".", "_");
            return typeof(NumCIL.Unsafe.Apply).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic);
        }

        /// <summary>
        /// Gets a <see cref="System.Reflection.MethodInfo"/> instance for the aggregate operation, bound to the data type, but unbound in the operand type.
        /// Returns null if the data type is not supported.
        /// </summary>
        /// <typeparam name="T">The type of data to operate on</typeparam>
        /// <returns>A <see cref="System.Reflection.MethodInfo"/> instance, bound to the data type, but unbound in the operand type, or null if no such method exists</returns>
        public static MethodInfo GetAggregate<T>()
        {
            if (!typeof(T).IsPrimitive || !SupportsUnsafe)
                return null;

            string name = "Aggregate_Entry_" + typeof(T).Name.Replace(".", "_");
            return typeof(NumCIL.Unsafe.Aggregate).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic);
        }

        /// <summary>
        /// Gets a <see cref="System.Reflection.MethodInfo"/> instance for the reduce operation, bound to the data type, but unbound in the operand type.
        /// Returns null if the data type is not supported.
        /// </summary>
        /// <typeparam name="T">The type of data to operate on</typeparam>
        /// <returns>A <see cref="System.Reflection.MethodInfo"/> instance, bound to the data type, but unbound in the operand type, or null if no such method exists</returns>
        public static MethodInfo GetReduce<T>()
        {
            if (!typeof(T).IsPrimitive || !SupportsUnsafe)
                return null;

            string name = "UFunc_Reduce_Inner_Flush_" + typeof(T).Name.Replace(".", "_");
            return typeof(NumCIL.Unsafe.Aggregate).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic);
        }

        /// <summary>
        /// Gets a <see cref="System.Reflection.MethodInfo"/> instance for the copy operation, bound for the data type
        /// Returns null if the data type is not supported.
        /// </summary>
        /// <typeparam name="T">The type of data to operate on</typeparam>
        /// <returns>A <see cref="System.Reflection.MethodInfo"/> instance, bound to the data type, or null if no such method exists</returns>
        public static MethodInfo GetCopyToManaged<T>()
        {
            if (!typeof(T).IsPrimitive || !SupportsUnsafe)
                return null;

            return typeof(NumCIL.Unsafe.Copy).GetMethod("Memcpy", new Type[] { typeof(T[]), typeof(IntPtr), typeof(long) });
        }

        /// <summary>
        /// Gets a <see cref="System.Reflection.MethodInfo"/> instance for the copy operation, bound for the data type
        /// Returns null if the data type is not supported.
        /// </summary>
        /// <typeparam name="T">The type of data to operate on</typeparam>
        /// <returns>A <see cref="System.Reflection.MethodInfo"/> instance, bound to the data type, or null if no such method exists</returns>
        public static MethodInfo GetCopyFromManaged<T>()
        {
            if (!typeof(T).IsPrimitive || !SupportsUnsafe)
                return null;

            return typeof(NumCIL.Unsafe.Copy).GetMethod("Memcpy", new Type[] { typeof(IntPtr), typeof(T[]), typeof(long) });
        }

        /// <summary>
        /// Method that performs an unsafe operation
        /// </summary>
        /// <returns>A test value</returns>
        private static long DoTest()
        {
            long[] x = new long[1];
            unsafe
            {
                fixed (long* y = x)
                {
                    y[0] = 1;
                }
            }

            return x[0];
        }
    }
}