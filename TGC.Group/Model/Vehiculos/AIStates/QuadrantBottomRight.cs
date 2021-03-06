﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGC.Group.Model.Vehiculos.Estados;

namespace TGC.Group.Model.Vehiculos.AIStates
{
    class QuadrantBottomRight : Quadrant
    {
        public QuadrantBottomRight(EstadoVehiculo state) : base(state)
        {

        }

        override public void Execute()
        {
            this.state.Back();
            this.state.Left();
        }
    }
}
