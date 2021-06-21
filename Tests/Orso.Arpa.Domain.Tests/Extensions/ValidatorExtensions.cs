using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Orso.Arpa.Domain.Errors;

namespace Orso.Arpa.Domain.Tests.Extensions
{
    public static class ValidatorExtensions
    {
        public static void ShouldThrowNotFoundExceptionFor<T, TValue>(this IValidator<T> validator,
            Expression<Func<T, TValue>> expression, TValue value, string typeName) where T : class, new()
        {
            var instanceToValidate = new T();

            var memberAccessor = new MemberAccessor<T, TValue>(expression, true);
            memberAccessor.Set(instanceToValidate, value);

            Func<TestValidationResult<T>> testValidationResultFunction = () => validator.TestValidate(instanceToValidate, opt => opt.IncludeProperties(memberAccessor.Member.Name));
            testValidationResultFunction.Should().ThrowExactly<NotFoundException>().WithMessage(typeName + " could not be found.");
        }

        public static void ShouldThrowNotFoundExceptionFor<T, TValue>(this IValidator<T> validator, Expression<Func<T, TValue>> expression, T objectToTest, string typeName) where T : class
        {
            TValue value = expression.Compile()(objectToTest);
            var memberAccessor = new MemberAccessor<T, TValue>(expression, true);
            Func<TestValidationResult<T>> testValidationResultFunction = () => validator.TestValidate(objectToTest, opt => opt.IncludeProperties(memberAccessor.Member.Name));
            testValidationResultFunction.Should().ThrowExactly<NotFoundException>().WithMessage(typeName + " could not be found.");
        }

        public static IEnumerable<ValidationFailure> ShouldHaveValidationErrorForExact<T, TValue>(this IValidator<T> validator,
            Expression<Func<T, TValue>> expression, TValue value) where T : class, new()
        {
            var instanceToValidate = new T();

            var memberAccessor = new MemberAccessor<T, TValue>(expression, true);
            memberAccessor.Set(instanceToValidate, value);

            TestValidationResult<T> testValidationResult = validator.TestValidate(instanceToValidate, opt => opt.IncludeProperties(memberAccessor.Member.Name));
            return testValidationResult.ShouldHaveValidationErrorFor(expression);
        }

        public static IEnumerable<ValidationFailure> ShouldHaveValidationErrorForExact<T, TValue>(this IValidator<T> validator, Expression<Func<T, TValue>> expression, T objectToTest) where T : class
        {
            TValue value = expression.Compile()(objectToTest);
            var memberAccessor = new MemberAccessor<T, TValue>(expression, true);
            TestValidationResult<T> testValidationResult = validator.TestValidate(objectToTest, opt => opt.IncludeProperties(memberAccessor.Member.Name));
            return testValidationResult.ShouldHaveValidationErrorFor(expression);
        }

        public static void ShouldNotHaveValidationErrorForExact<T, TValue>(this IValidator<T> validator,
            Expression<Func<T, TValue>> expression, TValue value) where T : class, new()
        {

            var instanceToValidate = new T();

            var memberAccessor = new MemberAccessor<T, TValue>(expression, true);
            memberAccessor.Set(instanceToValidate, value);

            TestValidationResult<T> testValidationResult = validator.TestValidate(instanceToValidate, opt => opt.IncludeProperties(memberAccessor.Member.Name));
            testValidationResult.ShouldNotHaveValidationErrorFor(expression);
        }

        public static void ShouldNotHaveValidationErrorForExact<T, TValue>(this IValidator<T> validator, Expression<Func<T, TValue>> expression, T objectToTest) where T : class
        {
            TValue value = expression.Compile()(objectToTest);
            var memberAccessor = new MemberAccessor<T, TValue>(expression, true);
            TestValidationResult<T> testValidationResult = validator.TestValidate(objectToTest, opt => opt.IncludeProperties(memberAccessor.Member.Name));
            testValidationResult.ShouldNotHaveValidationErrorFor(expression);
        }
    }

}
