using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.TestHelper;

namespace Orso.Arpa.Tests.Shared.Extensions
{
    public static class ValidatorExtensions
    {
        public static void ShouldHaveNotFoundErrorFor<T, TValue>(this IValidator<T> validator,
            Expression<Func<T, TValue>> expression, TValue value, string typeName) where T : class, new()
        {
            var instanceToValidate = new T();

            var memberAccessor = new MemberAccessor<T, TValue>(expression, true);
            memberAccessor.Set(instanceToValidate, value);

            TestValidationResult<T> result = validator.TestValidate(instanceToValidate, opt => opt.IncludeProperties(memberAccessor.Member.Name));
            result.ShouldHaveValidationErrorFor(expression)
                .WithErrorCode("404")
                .WithErrorMessage(typeName + " could not be found.");
        }

        public static void ShouldHaveNotFoundErrorFor<T, TValue>(this IValidator<T> validator, Expression<Func<T, TValue>> expression, T objectToTest, string typeName) where T : class
        {
            TValue value = expression.Compile()(objectToTest);
            var memberAccessor = new MemberAccessor<T, TValue>(expression, true);
            TestValidationResult<T> result = validator.TestValidate(objectToTest, opt => opt.IncludeProperties(memberAccessor.Member.Name));
            result.ShouldHaveValidationErrorFor(expression)
                .WithErrorCode("404")
                .WithErrorMessage(typeName + " could not be found.");
        }

        public static IEnumerable<ValidationFailure> ShouldHaveValidationErrorForExact<T, TValue>(
            this IValidator<T> validator,
            Expression<Func<T, TValue>> expression,
            TValue value) where T : class, new()
        {
            var instanceToValidate = new T();

            var memberAccessor = new MemberAccessor<T, TValue>(expression, true);
            memberAccessor.Set(instanceToValidate, value);

            TestValidationResult<T> testValidationResult = validator.TestValidate(instanceToValidate, opt => opt.IncludeProperties(memberAccessor.Member.Name));
            return testValidationResult.ShouldHaveValidationErrorFor(expression);
        }

        public static IEnumerable<ValidationFailure> ShouldHaveValidationErrorForExact<T, TValue>(
            this IValidator<T> validator,
            Expression<Func<T, TValue>> expression,
            T objectToTest) where T : class
        {
            TValue value = expression.Compile()(objectToTest);
            var memberAccessor = new MemberAccessor<T, TValue>(expression, true);
            TestValidationResult<T> testValidationResult = validator.TestValidate(objectToTest, opt => opt.IncludeProperties(memberAccessor.Member.Name));
            return testValidationResult.ShouldHaveValidationErrorFor(expression);
        }

        public static void ShouldNotHaveValidationErrorForExact<T, TValue>(
            this IValidator<T> validator,
            Expression<Func<T, TValue>> expression,
            TValue value) where T : class, new()
        {

            var instanceToValidate = new T();

            var memberAccessor = new MemberAccessor<T, TValue>(expression, true);
            memberAccessor.Set(instanceToValidate, value);

            TestValidationResult<T> testValidationResult = validator.TestValidate(instanceToValidate, opt => opt.IncludeProperties(memberAccessor.Member.Name));
            testValidationResult.ShouldNotHaveValidationErrorFor(expression);
        }

        public static void ShouldNotHaveValidationErrorForExact<T, TValue>(
            this IValidator<T> validator,
            Expression<Func<T, TValue>> expression,
            T objectToTest) where T : class
        {
            TValue value = expression.Compile()(objectToTest);
            var memberAccessor = new MemberAccessor<T, TValue>(expression, true);
            TestValidationResult<T> testValidationResult = validator.TestValidate(objectToTest, opt => opt.IncludeProperties(memberAccessor.Member.Name));
            testValidationResult.ShouldNotHaveValidationErrorFor(expression);
        }
    }

}
