using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Commands;

namespace Orso.Arpa.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGenericMediatorHandlers(this IServiceCollection services)
        {
            Assembly assembly = typeof(Login.Handler).Assembly;
            foreach (Type entityType in assembly.GetTypes().Where(n => n.BaseType == typeof(BaseEntity)))
            {
                services.AddGenericDetailsHandler(entityType);
                services.AddGenericListHandler(entityType);
                services.AddGenericDeleteHandler(entityType);
                services.AddGenericModifyHandler(entityType, assembly);
                services.AddGenericCreateHandler(entityType, assembly);
                services.AddGenericDeleteHandler(entityType, assembly);
            }

            return services;
        }

        private static void AddGenericDetailsHandler(this IServiceCollection services, Type entityType)
        {
            Type queryType = typeof(Domain.General.GenericHandlers.Details.Query<>).MakeGenericType(entityType);
            Type handlerType = typeof(Domain.General.GenericHandlers.Details.Handler<>).MakeGenericType(entityType);
            Type handlerInterfaceType = typeof(IRequestHandler<,>).MakeGenericType(queryType, entityType);
            services.AddTransient(handlerInterfaceType, handlerType);
        }

        public static void AddGenericListHandler(this IServiceCollection services, Type entityType)
        {
            Type queryType = typeof(Domain.General.GenericHandlers.List.Query<>).MakeGenericType(entityType);
            Type handlerType = typeof(Domain.General.GenericHandlers.List.Handler<>).MakeGenericType(entityType);
            Type responseType = typeof(IQueryable<>).MakeGenericType(entityType);
            Type handlerInterfaceType = typeof(IRequestHandler<,>).MakeGenericType(queryType, responseType);
            services.AddTransient(handlerInterfaceType, handlerType);
        }

        private static void AddGenericModifyHandler(this IServiceCollection services, Type entityType, Assembly assembly)
        {
            Type commandInterfaceType = typeof(Domain.General.GenericHandlers.Modify.IModifyCommand<>).MakeGenericType(entityType);
            IEnumerable<Type> commandTypes = assembly.GetTypes().Where(t => t.GetTypeInfo().ImplementedInterfaces.Contains(commandInterfaceType));
            foreach (Type commandType in commandTypes)
            {
                Type handlerType = typeof(Domain.General.GenericHandlers.Modify.Handler<>).MakeGenericType(entityType);
                Type responseType = typeof(Unit);
                Type handlerInterfaceType = typeof(IRequestHandler<,>).MakeGenericType(commandType, responseType);
                services.AddTransient(handlerInterfaceType, handlerType);
            }
        }

        private static void AddGenericCreateHandler(this IServiceCollection services, Type entityType, Assembly assembly)
        {
            Type commandInterfaceType = typeof(Domain.General.GenericHandlers.Create.ICreateCommand<>).MakeGenericType(entityType);
            IEnumerable<Type> commandTypes = assembly.GetTypes().Where(t => t.GetTypeInfo().ImplementedInterfaces.Contains(commandInterfaceType));
            foreach (Type commandType in commandTypes)
            {
                Type handlerType = typeof(Domain.General.GenericHandlers.Create.Handler<>).MakeGenericType(entityType);
                Type handlerInterfaceType = typeof(IRequestHandler<,>).MakeGenericType(commandType, entityType);
                services.AddTransient(handlerInterfaceType, handlerType);
            }
        }

        private static void AddGenericDeleteHandler(this IServiceCollection services, Type entityType)
        {
            Type commandType = typeof(Domain.General.GenericHandlers.Delete.Command<>).MakeGenericType(entityType);
            Type handlerType = typeof(Domain.General.GenericHandlers.Delete.Handler<>).MakeGenericType(entityType);
            Type responseType = typeof(Unit);
            Type handlerInterfaceType = typeof(IRequestHandler<,>).MakeGenericType(commandType, responseType);
            services.AddTransient(handlerInterfaceType, handlerType);
        }

        private static void AddGenericDeleteHandler(this IServiceCollection services, Type entityType, Assembly assembly)
        {
            Type commandInterfaceType = typeof(Domain.General.GenericHandlers.Delete.IDeleteCommand<>).MakeGenericType(entityType);
            Type commandType = Array.Find(assembly.GetTypes(), t => t.GetTypeInfo().ImplementedInterfaces.Contains(commandInterfaceType));
            if (commandType != null)
            {
                Type handlerType = typeof(Domain.General.GenericHandlers.Delete.Handler<>).MakeGenericType(entityType);
                Type responseType = typeof(Unit);
                Type handlerInterfaceType = typeof(IRequestHandler<,>).MakeGenericType(commandType, responseType);
                services.AddTransient(handlerInterfaceType, handlerType);
            }
        }
    }
}
