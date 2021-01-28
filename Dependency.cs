using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    class Dependency
    {
        private Space omaSpace;
        private Space kohdeSpace;
        //1 = vierekkäisyys, +1
        //2 = x2, kaksinkertaisuus
        private int type;

        public Dependency(Space omaSpace, Space kohdeSpace, int type)
        {
            this.omaSpace = omaSpace;
            this.kohdeSpace = kohdeSpace;
            this.type = type;
        }

        public bool doesHold()
        {
            //Jos toista ruutua ei ole vielä määritelty, turha vertailla
            if (kohdeSpace.getValue() == 0) return true;

            switch (this.type)
            {
               case 1:
                    if (omaSpace.getValue() + 1 == kohdeSpace.getValue() || kohdeSpace.getValue() + 1 == omaSpace.getValue()) return true;
                    return false;

               case 2:
                    if (omaSpace.getValue() * 2 == kohdeSpace.getValue() || kohdeSpace.getValue() * 2 == omaSpace.getValue()) return true;
                    return false;

               default:
                    return true;
            }
        }
    }
}
