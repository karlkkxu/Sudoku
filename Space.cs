using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Sudoku
{
    class Space
    {
        private int value = 0;
        private List<Dependency> dependencies = new List<Dependency>();

        public Space(int value)
        {
            this.value = value;
        }

        public Space(int value, Space kohdeSpace, int type)
        {
            this.value = value;
            addDependency(kohdeSpace, type);
        }

        public Space(int value, Space kohdeSpace, int type, Space kohde2, int type2)
        {
            this.value = value;
            addDependency(kohdeSpace, type);
            addDependency(kohde2, type2);
        }

        public void addDependency(Space kohdeSpace, int type)
        {
            this.dependencies.Add(new Dependency(this, kohdeSpace, type));
        }

        public int getValue()
        {
            return this.value;
        }

        public void setValue(int value)
        {
            this.value = value;
        }

        public List<Dependency> getDependency()
        {
            return this.dependencies;
        } 
    }
}
