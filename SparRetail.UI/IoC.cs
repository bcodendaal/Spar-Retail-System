﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spar.Retail.UI
{
    public static class IoC
    {
        public static IContainer Container { get { return _container; } }
        private static IContainer _container;

        public static void BootStrap(Action<ContainerBuilder> configure)
        {
            var builder = new ContainerBuilder();
            configure(builder);
            _container = builder.Build();
        }

    }
}