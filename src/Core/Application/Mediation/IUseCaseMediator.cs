﻿using System.Threading.Tasks;

namespace Optivem.Core.Application
{
    public interface IUseCaseMediator
    {
        Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest request);
    }
}