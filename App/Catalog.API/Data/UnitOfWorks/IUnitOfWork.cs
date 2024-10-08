﻿using Catalog.API.Data.Repositories.Abstractions;
using Catalog.LIB.Entities;

namespace Catalog.API.Data.UnitOfWorks;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<T> GetRepository<T>() where T : class, IBaseEntity, new();

    Task<int> SaveAsync();
    int Save();
}