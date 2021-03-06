﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGC.Core.Mathematica;
using Microsoft.DirectX.Direct3D;

namespace TGC.Group.Model
{
    public class CustomSprite : IDisposable
    {
        public CustomSprite()
        {
            initialize();
        }

        public static CustomSprite CreateImage(string path, TGCVector2 scalation, float rotation, TGCVector2 translation)
        {
            CustomSprite sprite = new CustomSprite();
            sprite.Bitmap = new CustomBitmap(GlobalConcepts.GetInstance().GetMediaDir() + "GUI\\HUB\\Velocimetro\\VelocimetroSinFlecha.png", GlobalConcepts.GetInstance().GetScreen());
            sprite.position = translation;
            sprite.rotation = rotation;
            sprite.scaling = new TGCVector2(0,0);
            return sprite;
        }

        #region Miembros de IDisposable

        public void Dispose()
        {
            if (Bitmap != null)
            {
                Bitmap.Dispose();
            }
        }

        #endregion Miembros de IDisposable

        private void initialize()
        {
            //Set the identity matrix.
            TransformationMatrix = TGCMatrix.Identity;

            //Set an empty rectangle to indicate the entire bitmap.
            SrcRect = Rectangle.Empty;

            //Initialize transformation properties.
            position = TGCVector2.Zero;
            scaling = TGCVector2.One;
            scalingCenter = TGCVector2.Zero;
            rotation = 0;
            rotationCenter = TGCVector2.Zero;

            Color = Color.White;
        }

        private void UpdateTransformationMatrix()
        {
            TransformationMatrix = TGCMatrix.Transformation2D(scalingCenter, 0, scaling, rotationCenter, rotation, position);
        }

        #region Public members

        /// <summary>
        ///     The transformation matrix.
        /// </summary>
        public TGCMatrix TransformationMatrix { get; set; }

        /// <summary>
        ///     The source rectangle to be drawn from the bitmap.
        /// </summary>
        public Rectangle SrcRect { get; set; }

        /// <summary>
        ///     The linked bitmap for the sprite.
        /// </summary>
        public CustomBitmap Bitmap { get; set; }

        /// <summary>
        ///     The color of the sprite.
        /// </summary>
        public Color Color { get; set; }

        private TGCVector2 position;

        /// <summary>
        ///     The sprite position in the 2D plane.
        /// </summary>
        public TGCVector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                UpdateTransformationMatrix();
            }
        }

        private float rotation;

        /// <summary>
        ///     The angle of rotation in radians.
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
                UpdateTransformationMatrix();
            }
        }

        private TGCVector2 rotationCenter;

        /// <summary>
        ///     The position of the centre of rotation
        /// </summary>
        public TGCVector2 RotationCenter
        {
            get { return rotationCenter; }
            set
            {
                rotationCenter = value;
                UpdateTransformationMatrix();
            }
        }

        private TGCVector2 scalingCenter;

        /// <summary>
        ///     The position of the centre of scaling
        /// </summary>
        public TGCVector2 ScalingCenter
        {
            get { return scalingCenter; }
            set
            {
                scalingCenter = value;
                UpdateTransformationMatrix();
            }
        }

        private TGCVector2 scaling;

        /// <summary>
        ///     The scaling factors in the x and y axes.
        /// </summary>
        public TGCVector2 Scaling
        {
            get { return scaling; }
            set
            {
                scaling = value;
                UpdateTransformationMatrix();
            }
        }

        #endregion Public members
    }
}
