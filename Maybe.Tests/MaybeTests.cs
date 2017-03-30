using System;
using Xunit;
using Maybe;

namespace Maybe.Tests
{
    public class MaybeTests
    {
        [Fact]
        public void ShouldAddResults()
        {
            var result = from firstNumber in 1.Lift()
                        from secondNumber in 2.Lift()
                        select firstNumber + secondNumber;

            Assert.Equal(3, result.Value);
        }

        [Fact]
        public void ShouldAvoidNullReference()
        {
            var result = from firstNumber in 1.Lift()
                        from secondNumber in ((int?)null).Lift()
                        select firstNumber + secondNumber;
            
            Assert.Equal(Maybe<int?>.Nothing, result);
        }
    }
}
