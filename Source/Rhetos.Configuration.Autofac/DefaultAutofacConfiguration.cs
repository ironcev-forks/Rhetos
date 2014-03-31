﻿/*
    Copyright (C) 2014 Omega software d.o.o.

    This file is part of Rhetos.

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Autofac;
using Rhetos.Utilities;
using Rhetos.Configuration.Autofac;
using System.Configuration;
using System.IO;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace Rhetos.Configuration.Autofac
{
    public class DefaultAutofacConfiguration : Module
    {
        public DefaultAutofacConfiguration(string rootPath, bool generate)
        {
            _paths = new Paths(rootPath);
            _generate = generate;
        }

        private readonly Paths _paths;
        private readonly bool _generate;

        protected override void Load(ContainerBuilder builder)
        {
            // Specific registrations configuration:
            if (_generate)
            {
                builder.RegisterModule(new DomModuleConfiguration(Paths.DomAssemblyName, DomAssemblyUsage.Generate));
                builder.RegisterModule(new NHibernateModuleConfiguration(null));
            }
            else
            {
                builder.RegisterModule(new DomModuleConfiguration(Paths.DomAssemblyName, DomAssemblyUsage.Load));
                builder.RegisterModule(new NHibernateModuleConfiguration(_paths.NHibernateMappingFile));
            }

            // General registrations:
            builder.RegisterInstance(new ConnectionString(SqlUtility.ConnectionString));
            builder.RegisterInstance<IDslSource>(new DiskDslScriptProvider(_paths.DslScriptsFolder));
            builder.RegisterInstance(new ResourcesFolder(_paths.ResourcesFolder));
            builder.RegisterModule(new SecurityModuleConfiguration());
            builder.RegisterModule(new UtilitiesModuleConfiguration());
            builder.RegisterModule(new DslModuleConfiguration());
            builder.RegisterModule(new CompilerConfiguration());
            builder.RegisterModule(new LoggingConfiguration());
            builder.RegisterModule(new ProcessingModuleConfiguration());
            builder.RegisterModule(new ExtensibilityModuleConfiguration());

            base.Load(builder);
        }
    }
}