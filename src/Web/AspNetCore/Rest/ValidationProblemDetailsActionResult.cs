﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Optivem.Platform.Core.Common.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optivem.Platform.Web.AspNetCore.Rest
{
    public class ValidationProblemDetailsActionResult : IActionResult
    {
        private IActionContextProblemDetailsFactory<ValidationProblemDetails> _factory;
        private IJsonSerializationService _jsonSerializationService;

        public ValidationProblemDetailsActionResult(IActionContextProblemDetailsFactory<ValidationProblemDetails> factory,
            IJsonSerializationService jsonSerializationService)
        {
            _factory = factory;
            _jsonSerializationService = jsonSerializationService;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            var problemDetails = _factory.Create(context);
            context.HttpContext.Response.StatusCode = problemDetails.Status.Value;
            return context.HttpContext.Response.WriteJsonAsync(problemDetails, _jsonSerializationService);
        }
    }
}