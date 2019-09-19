﻿using Autofac;
using Rhetos.Compiler;
using Rhetos.Deployment;
using Rhetos.Dom;
using Rhetos.Dsl;
using Rhetos.Extensibility;
using Rhetos.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhetos.Configuration.Autofac
{
    /// <summary>
    /// This module handles code generation and code compilation. 
    /// Requires refactoring to separate the two.
    /// </summary>
    public class DeployModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            AddDsl(builder);
            AddDom(builder);
            AddPersistence(builder);
            AddCompiler(builder);

            base.Load(builder);
        }

        // TODO: this is misnomer, since CoreModule also has AddDsl group
        private void AddDsl(ContainerBuilder builder)
        {
            builder.RegisterType<DiskDslScriptLoader>().As<IDslScriptsProvider>().SingleInstance();
            builder.RegisterType<Tokenizer>().SingleInstance();
            builder.RegisterType<DslModelFile>().As<IDslModelFile>().SingleInstance();
            builder.RegisterType<DslParser>().As<IDslParser>();
            builder.RegisterType<MacroOrderRepository>().As<IMacroOrderRepository>();
            builder.RegisterType<ConceptMetadata>().SingleInstance();
            builder.RegisterType<InitializationConcept>().As<IConceptInfo>(); // This plugin is registered manually because FindAndRegisterPlugins does not scan core Rhetos dlls.
            Plugins.FindAndRegisterPlugins<IConceptInfo>(builder);
            Plugins.FindAndRegisterPlugins<IConceptMacro>(builder, typeof(IConceptMacro<>));
            Plugins.FindAndRegisterPlugins<IConceptMetadataExtension>(builder);
        }

        private void AddDom(ContainerBuilder builder)
        {
            builder.RegisterType<DomGeneratorOptions>().SingleInstance();
            builder.RegisterType<DomGenerator>().As<IDomainObjectModel>().SingleInstance();
        }

        private void AddPersistence(ContainerBuilder builder)
        {
            builder.RegisterType<DataMigrationScriptsFromDisk>().As<IDataMigrationScriptsProvider>();
            builder.RegisterType<EntityFrameworkMappingGenerator>().As<IGenerator>();
            Plugins.FindAndRegisterPlugins<IConceptMapping>(builder, typeof(ConceptMapping<>));

        }

        private void AddCompiler(ContainerBuilder builder)
        {
            builder.RegisterType<CodeBuilder>().As<ICodeBuilder>();
            builder.RegisterType<CodeGenerator>().As<ICodeGenerator>();
            builder.RegisterType<AssemblyGenerator>().As<IAssemblyGenerator>();
            Plugins.FindAndRegisterPlugins<IConceptCodeGenerator>(builder);
        }
    }
}
