﻿namespace DomainLayer.Models
{
    /// <summary>
    /// Allows to 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Option<T> where T : class
    {
        private T? _object = null;

        public static Option<T> Some(T obj) => new() { _object = obj };
        public static Option<T> None() => new();

        public Option<TResult> Map<TResult>(Func<T, TResult> map) where TResult : class =>
            _object is null ? Option<TResult>.None() : Option<TResult>.Some(map(_object));
    }
}
