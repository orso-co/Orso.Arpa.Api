using System;
using System.Linq.Expressions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.TestHelper;
using Orso.Arpa.Domain.Errors;

namespace Orso.Arpa.Domain.Tests.Extensions
{
    public static class ValidatorExtensions
    {
        public static void ShouldThrowNotFoundExceptionFor<T, TValue>(this IValidator<T> validator,
            Expression<Func<T, TValue>> expression, TValue value, string typeName, string ruleSet = null) where T : class, new()
        {
            var instanceToValidate = new T();

            var memberAccessor = new MemberAccessor<T, TValue>(expression, true);
            memberAccessor.Set(instanceToValidate, value);

            Func<TestValidationResult<T>> testValidationResultFunction = () => validator.TestValidate(instanceToValidate, ruleSet);
            testValidationResultFunction.Should().ThrowExactly<NotFoundException>().WithMessage(typeName + " could not be found.");
        }

        public static void ShouldThrowNotFoundExceptionFor<T, TValue>(this IValidator<T> validator, Expression<Func<T, TValue>> expression, T objectToTest, string typeName, string ruleSet = null) where T : class
        {
            TValue value = expression.Compile()(objectToTest);
            Func<TestValidationResult<T>> testValidationResultFunction = () => validator.TestValidate(objectToTest, ruleSet);
            testValidationResultFunction.Should().ThrowExactly<NotFoundException>().WithMessage(typeName + " could not be found.");
        }
    }
}
