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

using Rhetos.Dsl;
using System;
using System.Collections.Generic;

namespace Rhetos.DatabaseGenerator.Test
{
    public class MockDslModel : IDslModel
    {
        public MockDslModel(IEnumerable<IConceptInfo> conceptInfos) { Concepts = conceptInfos; }

        public IEnumerable<IConceptInfo> Concepts { get; private set; }

        public IConceptInfo FindByKey(string conceptKey) { throw new NotImplementedException(); }

        public T GetIndex<T>() where T : IDslModelIndex
        {
            if (typeof(T) == typeof(DslModelIndexByType))
            {
                IDslModelIndex index = new DslModelIndexByType();
                foreach (var concept in Concepts)
                    index.Add(concept);
                return (T)index;
            }
            else
                throw new NotImplementedException($"Type '{typeof(T)}' is not supported in this mock.");
        }
    }
}
