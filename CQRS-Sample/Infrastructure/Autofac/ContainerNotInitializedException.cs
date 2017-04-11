using System;

namespace CQRS_Sample.Infrastructure.Autofac
{
    internal class ContainerNotInitializedException : Exception
    {
        public ContainerNotInitializedException(string message)
            : base(message)
        {

        }
    }
}