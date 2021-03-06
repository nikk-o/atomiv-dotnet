﻿using Atomiv.Core.Application;
using System;

namespace Atomiv.Template.Core.Application.Commands.Orders
{
    public class SubmitOrderCommand : IRequest<SubmitOrderCommandResponse>
    {
        public Guid Id { get; set; }
    }
}