using System;

namespace Maybe
{
    public struct Maybe<T>
    {
        public readonly T Value;
        public static Maybe<T> Nothing = default(Maybe<T>);

        public Maybe(T value) : this()
        {
            this.Value = value;
        }
    }

    public static class MaybeExtensions
    {
        public static Maybe<T> Lift<T>(this T value) => new Maybe<T>(value);

        /// Functor
        public static Maybe<TResult> Select<TSource, TResult>(this Maybe<TSource> maybe, Func<TSource, TResult> functor)
        {
            return maybe.Value != null
            ? functor(maybe.Value).Lift()
            : Maybe<TResult>.Nothing;
        }

        /// Monad
        public static Maybe<TResult> Select<TSource, TResult>(this Maybe<TSource> maybe, Func<TSource, Maybe<TResult>> arrow)
        {
            return maybe.Value != null
            ? arrow(maybe.Value)
            : Maybe<TResult>.Nothing;
        }

        /// Required for Linq from expression support
        public static Maybe<TProjection> SelectMany<TSource, TResult, TProjection>(this Maybe<TSource> maybe, 
        Func<TSource, Maybe<TResult>> arrow,
        Func<TSource, TResult, TProjection> projection)
        {
            return maybe.Select(
                result => arrow(result).Select(
                    projectionResult => projection(result, projectionResult)
                )
            );
        }
    }
}
