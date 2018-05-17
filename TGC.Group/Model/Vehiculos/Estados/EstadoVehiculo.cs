﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGC.Core.Mathematica;
using TGC.Core.Sound;

namespace TGC.Group.Model.Vehiculos.Estados
{
    abstract class EstadoVehiculo
    {
        protected Vehiculo auto;
        protected Tgc3dSound audio;

        public EstadoVehiculo(Vehiculo auto)
        {
            this.auto = auto;
        }

        virtual public void Advance()
        {
            auto.GetDeltaTiempoAvance().acumularTiempo(auto.GetElapsedTime());
            auto.SetVelocidadActual(auto.VelocidadFisica());
            return;
        }

        virtual public void Back()
        {
            auto.GetDeltaTiempoAvance().acumularTiempo(auto.GetElapsedTime());
            auto.SetVelocidadActual(auto.VelocidadFisicaRetroceso());
            return;
        }

        virtual public void Jump()
        {
            auto.GetDeltaTiempoSalto().acumularTiempo(auto.GetElapsedTime());
            this.cambiarEstado(new Jumping(this.auto));
            return;
        }

        virtual public void SpeedUpdate()
        {
            return;
        }

        virtual public void JumpUpdate()
        {
            return;
        }

        protected void Move(TGCVector3 desplazamiento)
        {
            this.auto.Move(desplazamiento);
        }

        protected float VelocidadFisicaDeSalto()
        {
            return auto.GetVelocidadActualDeSalto() + (-auto.GetAceleracionGravedad()) * auto.GetDeltaTiempoSalto().tiempoTranscurrido();
        }

        virtual public void Right(CamaraEnTerceraPersona camara)
        {
            float rotacionReal = auto.GetVelocidadDeRotacion() * auto.GetElapsedTime();
            rotacionReal = (auto.GetVelocidadActual() > 0) ? rotacionReal : -rotacionReal;
            this.auto.Girar(rotacionReal, camara);

        }

        //lo mismo que arriba
        virtual public void Left(CamaraEnTerceraPersona camara)
        {
            float rotacionReal = auto.GetVelocidadDeRotacion() * auto.GetElapsedTime();
            rotacionReal = (auto.GetVelocidadActual() < 0) ? rotacionReal : -rotacionReal;
            this.auto.Girar(rotacionReal, camara);
        }

        protected void liberarRecursos()
        {
            if(audio != null)
            {
                this.audio.stop();
                this.audio.dispose();
            }
            
        }

        protected void cambiarEstado(EstadoVehiculo nuevoEstado)
        {
            this.liberarRecursos();
            this.auto.SetEstado(nuevoEstado);
        }

        public void UpdateWheels()
        {
            var rotacionReal = this.auto.GetVelocidadActual() * this.auto.GetElapsedTime();
            this.auto.UpdateFrontWheels(Math.Abs(rotacionReal));
        }
    }
}
