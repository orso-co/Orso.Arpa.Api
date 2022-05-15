using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.TestHelper;

namespace Orso.Arpa.Tests.Shared.Extensions
{
    public static class ValidatorExtensions
    {
        public static async Task ShouldHaveNotFoundErrorForAsync<T, TValue>(this IValidator<T> validator,
            Expression<Func<T, TValue>> expression, TValue value, string typeName) where T : class, new()
        {
            var instanceToValidate = new T();

            string propertyName = ValidatorOptions.Global.PropertyNameResolver(typeof(T), expression.GetMember(), expression);
            typeof(T).GetProperty(propertyName).SetValue(instanceToValidate, value, null);

            TestValidationResult<T> result = await validator.TestValidateAsync(instanceToValidate, opt => opt.IncludeProperties(propertyName));
            result.ShouldHaveValidationErrorFor(expression)
                .WithErrorCode("404")
                .WithErrorMessage(typeName + " could not be found.");
        }

        public static async Task ShouldHaveNotFoundErrorFor<T, TValue>(this IValidator<T> validator, Expression<Func<T, TValue>> expression, T objectToTest, string typeName) where T : class
        {
            TValue value = expression.Compile()(objectToTest);
            string propertyName = ValidatorOptions.Global.PropertyNameResolver(typeof(T), expression.GetMember(), expression);
            TestValidationResult<T> result = await validator.TestValidateAsync(objectToTest, opt => opt.IncludeProperties(propertyName));
            result.ShouldHaveValidationErrorFor(expression)
                .WithErrorCode("404")
                .WithErrorMessage(typeName + " could not be found.");
        }

        public static async Task<ITestValidationWith> ShouldHaveValidationErrorForExactAsync<T, TValue>(
            this IValidator<T> validator,
            Expression<Func<T, TValue>> expression,
            TValue value) where T : class, new()
        {
            var instanceToValidate = new T();

            string propertyName = ValidatorOptions.Global.PropertyNameResolver(typeof(T), expression.GetMember(), expression);
            typeof(T).GetProperty(propertyName).SetValue(instanceToValidate, value, null);

            TestValidationResult<T> testValidationResult = await validator.TestValidateAsync(instanceToValidate, opt => opt.IncludeProperties(propertyName));
            return testValidationResult.ShouldHaveValidationErrorFor(expression);
        }

        public static ITestValidationWith ShouldHaveValidationErrorForExact<T, TValue>(
           this IValidator<T> validator,
           Expression<Func<T, TValue>> expression,
           TValue value) where T : class, new()
        {
            var instanceToValidate = new T();

            string propertyName = ValidatorOptions.Global.PropertyNameResolver(typeof(T), expression.GetMember(), expression);
            typeof(T).GetProperty(propertyName).SetValue(instanceToValidate, value, null);

            TestValidationResult<T> testValidationResult = validator.TestValidate(instanceToValidate, opt => opt.IncludeProperties(propertyName));
            return testValidationResult.ShouldHaveValidationErrorFor(expression);
        }

        public static ITestValidationWith ShouldHaveValidationErrorForExact<T, TValue>(
            this IValidator<T> validator,
            Expression<Func<T, TValue>> expression,
            T objectToTest) where T : class
        {
            TValue value = expression.Compile()(objectToTest);
            string propertyName = ValidatorOptions.Global.PropertyNameResolver(typeof(T), expression.GetMember(), expression);
            TestValidationResult<T> testValidationResult = validator.TestValidate(objectToTest, opt => opt.IncludeProperties(propertyName));
            return testValidationResult.ShouldHaveValidationErrorFor(expression);
        }

        public static async Task<ITestValidationWith> ShouldHaveValidationErrorForExactAsync<T, TValue>(
            this IValidator<T> validator,
            Expression<Func<T, TValue>> expression,
            T objectToTest) where T : class
        {
            TValue value = expression.Compile()(objectToTest);
            string propertyName = ValidatorOptions.Global.PropertyNameResolver(typeof(T), expression.GetMember(), expression);
            TestValidationResult<T> testValidationResult = await validator.TestValidateAsync(objectToTest, opt => opt.IncludeProperties(propertyName));
            return testValidationResult.ShouldHaveValidationErrorFor(expression);
        }

        public static async Task ShouldNotHaveValidationErrorForExactAsync<T, TValue>(
            this IValidator<T> validator,
            Expression<Func<T, TValue>> expression,
            TValue value) where T : class, new()
        {
            var instanceToValidate = new T();

            string propertyName = ValidatorOptions.Global.PropertyNameResolver(typeof(T), expression.GetMember(), expression);
            typeof(T).GetProperty(propertyName).SetValue(instanceToValidate, value, null);

            TestValidationResult<T> testValidationResult = await validator.TestValidateAsync(instanceToValidate, opt => opt.IncludeProperties(propertyName));
            testValidationResult.ShouldNotHaveValidationErrorFor(expression);
        }

        public static void ShouldNotHaveValidationErrorForExact<T, TValue>(
            this IValidator<T> validator,
            Expression<Func<T, TValue>> expression,
            TValue value) where T : class, new()
        {
            var instanceToValidate = new T();

            string propertyName = ValidatorOptions.Global.PropertyNameResolver(typeof(T), expression.GetMember(), expression);
            typeof(T).GetProperty(propertyName).SetValue(instanceToValidate, value, null);

            TestValidationResult<T> testValidationResult = validator.TestValidate(instanceToValidate, opt => opt.IncludeProperties(propertyName));
            testValidationResult.ShouldNotHaveValidationErrorFor(expression);
        }

        public static async Task ShouldNotHaveValidationErrorForExactAsync<T, TValue>(
            this IValidator<T> validator,
            Expression<Func<T, TValue>> expression,
            T objectToTest) where T : class
        {
            TValue value = expression.Compile()(objectToTest);
            string propertyName = ValidatorOptions.Global.PropertyNameResolver(typeof(T), expression.GetMember(), expression);
            TestValidationResult<T> testValidationResult = await validator.TestValidateAsync(objectToTest, opt => opt.IncludeProperties(propertyName));
            testValidationResult.ShouldNotHaveValidationErrorFor(expression);
        }

        public static void ShouldNotHaveValidationErrorForExact<T, TValue>(
            this IValidator<T> validator,
            Expression<Func<T, TValue>> expression,
            T objectToTest) where T : class
        {
            TValue value = expression.Compile()(objectToTest);
            string propertyName = ValidatorOptions.Global.PropertyNameResolver(typeof(T), expression.GetMember(), expression);
            TestValidationResult<T> testValidationResult = validator.TestValidate(objectToTest, opt => opt.IncludeProperties(propertyName));
            testValidationResult.ShouldNotHaveValidationErrorFor(expression);
        }
    }
}
