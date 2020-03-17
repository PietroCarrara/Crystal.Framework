using System;
using Crystal.Framework.Math;

namespace Crystal.Framework.Components
{
    public class Camera : IComponent
    {
        public bool Active = true;

        private float zoom = 1f;
        private float rotationZ = 0f;
        private Vector3 position = Vector3.One;
        private Vector2 origin = new Vector2(1280/2, 720/2);

        private Matrix4 scale, translate, rotate, originMatrix;

        /// <summary>
        /// The zoom of this camera. Higher values means more zoom
        /// </summary>
        public float Zoom
        {
            get => zoom;
            set
            {
                zoom = value;
                this.scale = Matrix4.CreateScale(zoom, zoom, zoom);
                updateTransform();
            }
        }

        /// <summary>
        /// Rotation in radians of this camera along the Z axis
        /// </summary>
        public float RotationZ
        {
            get => rotationZ;
            set
            {
                Console.WriteLine(rotationZ);
                rotationZ = value;
                this.rotate = Matrix4.CreateRotationZ(rotationZ);
                updateTransform();
            }
        }

        /// <summary>
        /// Position of this camera in 3D space
        /// </summary>
        public Vector3 Position
        {
            get => position;
            set
            {
                position = value;
                this.translate = Matrix4.CreateTranslation(-position.X, -position.Y, position.Z);
                updateTransform();
            }
        }

        public Vector2 Origin
        {
            get => this.origin;
            set
            {
                this.origin = value;
                this.originMatrix = Matrix4.CreateTranslation(this.origin);
            }
        }

        /// <summary>
        /// The matrix that represents the parameters of this camera
        /// </summary>
        public Matrix4 TransformationMatrix { get; private set; }

        /// <summary>
        /// The inverse of the TransformationMatrix
        /// </summary>
        public Matrix4 InverseTransformationMatrix { get; private set; }

        public Camera(Vector2 origin)
        {
            this.origin = origin;
            
            this.translate = Matrix4.CreateTranslation(-this.Position.X, -this.Position.Y, this.position.Z);
            this.rotate = Matrix4.CreateRotationZ(this.rotationZ);
            this.scale = Matrix4.CreateScale(this.zoom, this.zoom, this.zoom);
            this.originMatrix = Matrix4.CreateTranslation(this.origin);

            updateTransform();
        }

        public Camera()
        {
            this.translate = Matrix4.CreateTranslation(-this.Position.X, -this.Position.Y, this.position.Z);
            this.rotate = Matrix4.CreateRotationZ(this.rotationZ);
            this.scale = Matrix4.CreateScale(this.zoom, this.zoom, this.zoom);
            this.originMatrix = Matrix4.CreateTranslation(this.origin);

            updateTransform();
        }

        public Vector2 ScreenToWorld(Vector2 vector)
        {
            return vector.Transform(this.InverseTransformationMatrix);
        }

        public Vector2 WorldToScreen(Vector2 vector)
        {
            return vector.Transform(this.TransformationMatrix);
        }

        private void updateTransform()
        {
            this.TransformationMatrix = this.translate * this.rotate * this.scale * this.originMatrix;
            this.InverseTransformationMatrix = this.TransformationMatrix.Invert();
        }
    }
}