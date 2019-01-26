﻿using Optivem.Platform.Core.Common.Parsing;

namespace Optivem.Platform.Infrastructure.Common.Parsing.Default
{
    public abstract class BaseParser<T> : IParser<T>
    {
        public T Parse(string value)
        {
            if(value == null)
            {
                return default(T);
            }

            return ParseInner(value);
        }

        protected abstract T ParseInner(string value);
    }
}