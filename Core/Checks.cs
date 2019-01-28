﻿using System;
using System.Collections.Generic;
using Core.Collection;
using Core.Primitive;

namespace Core
{

    public sealed class Checks
    {

        public static void Is(object actual, Type expectedType)
        {
            NotNull(expectedType);
            NotNull(actual);
            if (actual.GetType() != expectedType)
            {
                Fail<ArgumentException>($"Expected type {expectedType.Name} but was {actual.GetType().Name}");
            }
        }

        public static void NotNull(object obj)
        {
            NotNull(obj, "Expected {not null} but was {null}.");
        }

        public static void NotNull(object obj, string message)
        {
            NotNull<NullReferenceException>(obj, message);
        }

        public static void NotNull<T>(object obj, string message) where T : Exception
        {
            if(obj == null)
            {
                Fail<T>(message);
            }
        }

        public static void NotNullOrEmpty(string value)
        {

            NotNull(value, "Expected not null but was null.");
            if (value.Trim().Length == 0) {
                Fail<ArgumentException>("Expected not empty but was empty.");
            }
        }

        public static void Null(object obj)
        {
            Null(obj, "Expected null but was not null.");
        }

        public static void Null(object obj, string message)
        {
            Null<ArgumentException>(obj, message);
        }

        private static void Null<T>(object obj, string message) where T : Exception
        {
            if (obj != null)
            {
                Fail<T>(message);
            }
        }

        public static void NotNullOrEmpty<TC>(ICollection<TC> collection)
        {
            NotNullOrEmpty<ArgumentException, TC>(collection, "Expected not empty but was empty.");
        }

        public static void NotNullOrEmpty<TC>(ICollection<TC> collection, string message)
        {
            NotNullOrEmpty<ArgumentException, TC>(collection, message);
        }

        private static void NotNullOrEmpty<TE, TC>(ICollection<TC> collection, string message) where TE : Exception
        {
            if(collection == null || collection.Count == 0)
            {
               Fail<TE>(message);
            }
        }

        public static void Equals(int expected, int actual)
        {
            Equals<ArgumentException>(expected, actual, $"Expected {expected} but was {actual}.");
        }

        public static void Equals(string expected, string actual)
        {
            Equals<ArgumentException>(expected, actual, $"Expected {expected} but was {actual}.");
        }

        public static void Equals<T>(T expected, T actual)
        {
            if(!expected.Equals(actual))
            {
                Fail<ArgumentException>($"Expected {expected} but was {actual}.");
            }
        }

        public static void Equals<T>(T[] actual, T[] expected)
        {
            if (!Arrays.Equals(actual,expected))
            {
                 Fail<ArgumentException>($"Expected {expected} but was {actual}.");
            }
        }

        public static void Equals<T>(object expected, object actual, string message) where T : Exception
        {

            if(!expected.Equals(actual))
            {
                Fail<T>(message);
            }
        }

        public static void NotEquals(int expected, int actual, string message)
        {
            NotEquals<ArgumentException>(expected, actual, message);
        }

        public static void NotEquals(object expected, object actual)
        {
            NotEquals<ArgumentException>(expected, actual, "Value must not be equal.");
        }

        public static void NotEquals<T>(object expected, object actual, string message) where T : Exception
        {
            if (ReferenceEquals(expected, actual) || expected.Equals(actual))
            {
                Fail<T>(message);
            }
        }

        public static void IsFalse(bool value)
        {
            IsFalse(value, "Expected false but was true.");
        }

        public static void IsFalse(bool value, string message)
        {
            IsFalse<ArgumentException>(value, message);
        }

        public static void IsFalse<T>(bool? value, string message) where T : Exception
        {
            if(value != false)
            {
                Fail<T>(message);
            }
        }

        public static void IsTrue(bool value)
        {
            IsTrue(value, "Expected true but was false.");
        }

        public static void IsTrue(bool value, string message)
        {
            IsTrue<ArgumentException>(value, message);
        }

        public static void IsTrue<T>(bool value, string message) where T : Exception
        {
            if(value != true)
            {
                Fail<T>(message);
            }
        }

        public static void InRange(int min, int max, int actual, string message)
        {
            if (actual < min || actual > max)
            {
                Fail<OverflowException>(message);
            }
        }

        public static void InRange(int actual, Range<int> range, string message)
        {
            if (actual < range.GetStart() || actual > range.GetStart())
            {
                Fail<OverflowException>(message);
            }
        }

        public static void LessThan<T>(int comparator, int actual, string message) where T : Exception
        {
            if (actual >= comparator)
            {
                Fail<T>(message);
            }
        }

        public static void LessThanOrEqual<T>(int comparator, int actual, string message) where T : Exception
        {
            if(actual > comparator)
            {
                Fail<T>(message);
            }
        }

        public static void GreaterThan<T>(int actual, int comparator, string message) where T : Exception
        {
            if (actual <= comparator)
            {
                Fail<T>(message);
            }
        }

        public static void GreaterThanOrEqual<T>(int actual, int comparator, string message) where T : Exception
        {
            if (actual < comparator)
            {
                Fail<T>(message);
            }
        }

        private static void Fail<T>(string message) where T : Exception
        {
            var type = typeof(T);

            var constructor = type.GetConstructor(new[]{
                typeof(string)
            });
            
            if (constructor == null)
            {
                throw new ArgumentException("Exception[" + type.FullName + "] must have a constructor of a single string parameter.");
            }

            throw (T) constructor.Invoke(new object[] {
                   message
            });
        }
    }

}

