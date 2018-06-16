﻿using System;
using System.Collections.Generic;
using System.Linq;
using TGC.Core.Mathematica;

namespace TGC.Group.Model.Vehiculos.AIStates
{
    class SearchWeapons : AIState
    {
        public SearchWeapons(ArtificialIntelligence AI) : base(AI)
        {

        }

        override protected void DeterminateState()
        {
            Vehicle car = Scene.GetInstance().auto;
            if (this.AI.IsEnemyInRadar(car))
            {
                this.AI.ChangeState(new FollowingCar(this.AI));
            }
            else if (this.AI.DoIHaveEnoughWeapons())
            {
                this.AI.ChangeState(new TakeAWalk(this.AI));
            }
        }

        public override void Run()
        {
            base.Run();
            List<Collidable> weapons = Scene.GetInstance().GetWeapons(this.AI);
            Collidable weapon = this.SelectTheNearest(weapons);
            Quadrant quadrant = this.GetCuadrante(weapon.GetPosition());
            quadrant.Execute();
                       
        }

        private Collidable SelectTheNearest(List<Collidable> weapons)
        {
            GlobalConcepts g = GlobalConcepts.GetInstance();
            weapons.Sort((w1, w2) => g.DistanceBetweenTwoPoints(this.AI.GetPosition(), w1.GetPosition()).CompareTo(g.DistanceBetweenTwoPoints(this.AI.GetPosition(), w2.GetPosition())));
            return weapons.First();
        }
    }
}
