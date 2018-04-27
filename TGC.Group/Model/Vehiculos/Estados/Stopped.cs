﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGC.Group.Model.Vehiculos.Estados;
using TGC.Core.Mathematica;

namespace TGC.Group.Model.Vehiculos.Estados
{
    class Stopped : EstadoVehiculo
    {
        public Stopped(Vehiculo auto) : base(auto)
        {

        }

        override public void advance()
        {
            base.advance();
            move();
            auto.setEstado(new Forward(this.auto));
        }

        public override void back()
        {
            base.back();
            move();
            auto.setEstado(new Backward(this.auto));
        }

    }
}