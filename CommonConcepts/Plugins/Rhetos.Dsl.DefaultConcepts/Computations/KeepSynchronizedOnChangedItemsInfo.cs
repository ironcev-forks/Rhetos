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

namespace Rhetos.Dsl.DefaultConcepts
{
    [Export(typeof(IConceptInfo))]
    public class KeepSynchronizedOnChangedItemsInfo : IConceptInfo, IValidationConcept
    {
        [ConceptKey]
        public PersistedDataStructureInfo Persisted { get; set; }

        [ConceptKey]
        public ChangesOnChangedItemsInfo UpdateOnChange { get; set; }

        [ConceptKey]
        public ReadChangedItemsOnSaveInfo ReadChanged { get; set; }

        public string FilterSaveExpression { get; set; }

        public void CheckSemantics(IEnumerable<IConceptInfo> concepts)
        {
            if (UpdateOnChange.DependsOn != ReadChanged.DataStructure)
                throw new Exception("KeepSynchronizedOnChangedItemsInfo must have UpdateOnChange.DependsOn == ReadChanged.DataStructure."
                    + " UpdateOnChange.DependsOn is '" + UpdateOnChange.DependsOn.GetKeyProperties() + "',"
                    + " ReadChanged.DataStructure is '" + ReadChanged.DataStructure.GetKeyProperties() + "'.");
        }
    }
}
