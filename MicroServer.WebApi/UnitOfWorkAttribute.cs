using System;
using Microsoft.EntityFrameworkCore;

namespace MicroServer.WebApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class UnitOfWorkAttribute : Attribute
    {
        public Type[] DbContexteTypes { get; init; }
        public UnitOfWorkAttribute(params Type[] dbContextTyps)
        {
            this.DbContexteTypes = dbContextTyps;
            foreach (var type in dbContextTyps)
            {
                if (!typeof(DbContext).IsAssignableFrom(type))
                {
                    throw new ArgumentException($"{type} must inherit from DbContext");
                }
            }
        }
    }
}

