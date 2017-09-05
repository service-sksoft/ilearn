using System;

namespace ILearn.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ILearnContext Init();
    }
}