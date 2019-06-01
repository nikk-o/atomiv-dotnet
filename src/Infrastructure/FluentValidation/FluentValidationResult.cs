﻿using FluentValidation.Results;
using Optivem.Core.Application;
using System.Collections.Generic;
using System.Linq;

namespace Optivem.Infrastructure.FluentValidation
{
    public class FluentValidationResult : IRequestValidationResult
    {
        private ValidationResult _result;

        public FluentValidationResult(ValidationResult result)
        {
            _result = result;
        }

        public bool IsValid => _result.IsValid;

        public IList<IRequestValidationError> Errors => _result.Errors.Select(GetValidationError).ToList();

        private static IRequestValidationError GetValidationError(ValidationFailure failure)
        {
            return new FluentValidationError(failure);
        }
    }
}