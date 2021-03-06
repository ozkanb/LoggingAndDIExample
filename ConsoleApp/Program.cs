﻿using Autofac;
using CatLibrary;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Autofac container.
            var builder = new ContainerBuilder();

            builder.RegisterType<Cat>().As<ICat>();

            // Create Logger<T> when ILogger<T> is required.
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>));

            // Use NLogLoggerFactory as a factory required by Logger<T>.
            builder.RegisterType<NLogLoggerFactory>().AsImplementedInterfaces().InstancePerLifetimeScope();

            // Finish registrations and prepare the container that can resolve things.
            var container = builder.Build();

            // Entry point. This provides our logger instance to a Cat's constructor.
            ICat cat = container.Resolve<ICat>();

            // Run
            string result = cat.MakeSound();
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
