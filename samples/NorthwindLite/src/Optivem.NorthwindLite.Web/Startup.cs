﻿using AutoMapper;
using MediatR;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Optivem.Core.Application;
using Optivem.Core.Domain;
using Optivem.Infrastructure.Mapping.AutoMapper;
using Optivem.Infrastructure.Messaging.MediatR;
using Optivem.Infrastructure.Persistence.EntityFrameworkCore;
using Optivem.NorthwindLite.Core.Application;
using Optivem.NorthwindLite.Core.Application.Interface.Customers.Commands;
using Optivem.NorthwindLite.Core.Application.Interface.Customers.Queries.BrowseAll;
using Optivem.NorthwindLite.Core.Application.Interface.Customers.Queries.List;
using Optivem.NorthwindLite.Core.Application.Interface.Customers.Retrieve;
using Optivem.NorthwindLite.Core.Application.Interface.Requests.Customers;
using Optivem.NorthwindLite.Core.Application.Interface.Services;
using Optivem.NorthwindLite.Core.Application.UseCases;
using Optivem.NorthwindLite.Core.Application.UseCases.Customers;
using Optivem.NorthwindLite.Core.Domain.Entities;
using Optivem.NorthwindLite.Infrastructure.Mapping;
using Optivem.NorthwindLite.Infrastructure.Messaging;
using Optivem.NorthwindLite.Infrastructure.Persistence;
using Optivem.NorthwindLite.Infrastructure.Validation;
using System;
using FluentValidation;
using Optivem.Infrastructure.Validation.FluentValidation;
using Optivem.Web.AspNetCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Optivem.Common.Serialization;
using Optivem.Infrastructure.Serialization.Json.NewtonsoftJson;

namespace Optivem.NorthwindLite.Web
{
    public class Startup
    {
        public const string DatabaseContextConnectionStringKey = "Context";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // TODO: VC: Move to base, automatic lookup of everything implementing IService, auto-DI

            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var mediatRAssemblies = typeof(CreateCustomerMediatorRequestHandler); // TODO: VC
            var autoMapperAssemblies = typeof(CreateCustomerRequestProfile).Assembly; // allAssemblies; // TODO: VC
            var fluentValidationAssemblies = typeof(CreateCustomerRequestValidator).Assembly;

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2) // TODO: VC: Check if needed?
                .AddHateoas(options =>
                {
                    options
                        .AddLink<FindCustomerResponse>("find-customer", p => new { id = p.Id })
                        .AddLink<FindCustomerResponse>("create-customer")
                        ;
                        // .AddLink<List<PersonDto>>("create-person")
                        // .AddLink<PersonDto>("update-person", p => new { id = p.Id })
                        // .AddLink<PersonDto>("delete-person", p => new { id = p.Id });
                })
                // .AddFluentValidation(e => e.RegisterValidatorsFromAssembly(fluentValidationAssemblies))
                ;

            // TODO: VC: Test HATEOAS



            // Application - Use Cases
            services.AddScoped<IUseCase<ListCustomersRequest, ListCustomersResponse>, ListCustomersUseCase>();
            services.AddScoped<IUseCase<FindCustomerRequest, FindCustomerResponse>, FindCustomerUseCase>();
            services.AddScoped<IUseCase<CreateCustomerRequest, CreateCustomerResponse>, CreateCustomerUseCase>();

            // Application - Services
            services.AddScoped<ICustomerService, CustomerService>();

            // Infrastructure - Repository
            var connection = Configuration.GetConnectionString(DatabaseContextConnectionStringKey);

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IUnitOfWork, UnitOfWork<DatabaseContext>>();
            services.AddScoped<IReadonlyRepository<Customer, int>, CustomerRepository>();
            services.AddScoped<IRepository<Customer, int>, CustomerRepository>();

            // Infrastructure - Mapping
            services.AddAutoMapper(autoMapperAssemblies);
            services.AddScoped<IRequestMapper, RequestMapper>();
            services.AddScoped<IResponseMapper, ResponseMapper>();

            // Infrastructure - Validation
            services.AddScoped(typeof(IRequestValidationHandler<>), typeof(RequestValidationHandler<>));
            services.AddScoped(typeof(IRequestValidator<>), typeof(FluentValidationRequestValidator<>));
            services.AddScoped<IValidator<CreateCustomerRequest>, CreateCustomerRequestValidator>();

            // Infrastructure - Messaging
            services.AddMediatR(mediatRAssemblies);
            services.AddScoped<IUseCaseMediator, UseCaseMediator>();
            // services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddScoped<IPipelineBehavior<MediatorRequest<CreateCustomerRequest, CreateCustomerResponse>, CreateCustomerResponse>, ValidationPipelineBehavior<CreateCustomerRequest, CreateCustomerResponse>>();

            var validationProblemDetailsFactory = new ValidationActionContextProblemDetailsFactory();
            var jsonSerializationService = new JsonSerializationService();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ctx
                    => new ValidationProblemDetailsActionResult(validationProblemDetailsFactory, jsonSerializationService);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var registry = new ExceptionProblemDetailsFactoryRegistry(new SystemExceptionProblemDetailsFactory());
            registry.Add(new BadHttpRequestExceptionProblemDetailsFactory());
            registry.Add(new RequestValidationExceptionProblemDetailsFactory());

            var problemDetailsFactory = new ExceptionProblemDetailsFactory(registry);
            IJsonSerializationService jsonSerializationService = new JsonSerializationService();

            app.UseExceptionHandler(problemDetailsFactory, jsonSerializationService);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}