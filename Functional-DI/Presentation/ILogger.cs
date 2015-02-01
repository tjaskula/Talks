using System;

namespace Presentation
{
    public interface ILogger
    {
        void LogError(Exception e);
    }
}