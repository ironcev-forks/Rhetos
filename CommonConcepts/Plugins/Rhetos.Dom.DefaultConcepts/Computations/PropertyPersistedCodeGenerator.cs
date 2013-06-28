﻿/*
    Copyright (C) 2013 Omega software d.o.o.

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
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.Dom.DefaultConcepts
{
    [Export(typeof(IConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(PropertyPersistedInfo))]
    public class PropertyPersistedCodeGenerator : IConceptCodeGenerator
    {
        private static string ComparePropertySnippet(PropertyPersistedInfo info)
        {
            return string.Format(
@"                        if (same && (sourceEnum.Current.{0} == null && destEnum.Current.{0} != null || sourceEnum.Current.{0} != null && !sourceEnum.Current.{0}.Equals(destEnum.Current.{0})))
                            same = false;
", info.Property.Name);
        }

        private static string ClonePropertySnippet(PropertyPersistedInfo info)
        {
            return string.Format(
@",
                                    {0} = sourceEnum.Current.{0}", info.Property.Name);
        }

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (PropertyPersistedInfo) conceptInfo;
            codeBuilder.InsertCode(ComparePropertySnippet(info), PersistedDataStructureCodeGenerator.ComparePropertyTag, info.Property.DataStructure);
            codeBuilder.InsertCode(ClonePropertySnippet(info), PersistedDataStructureCodeGenerator.ClonePropertyTag, info.Property.DataStructure);
        }
    }
}
