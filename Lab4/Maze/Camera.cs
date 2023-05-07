using System;
using OpenTK;

namespace Maze
{
    internal class Camera
    {
        private const float ZNear = 0.01f;
        private const float ZFar = 100f;

        private Vector3 _front = -Vector3.UnitZ;
        private Vector3 _up = Vector3.UnitY;
        private Vector3 _right = Vector3.UnitX;

        private readonly float _fov = MathHelper.PiOver3;
        private float _pitch = 0;
        private float _yaw = -MathHelper.PiOver2;

        public Vector3 Position;
        public Vector3 Right => _right;
        public Vector3 Front => _front;
        public Vector3 Up => _up;

        public float AspectRatio { private get; set; }

        public float Pitch
        {
            get => MathHelper.RadiansToDegrees( _pitch );
            set
            {
                var angle = MathHelper.Clamp( value, -89f, 89f );
                _pitch = MathHelper.DegreesToRadians( angle );
                UpdateVectors();
            }
        }

        public float Yaw
        {
            get => MathHelper.RadiansToDegrees( _yaw );
            set
            {
                _yaw = MathHelper.DegreesToRadians( value );
                UpdateVectors();
            }
        }

        public Camera( Vector3 position, float aspectRatio )
        {
            Position = position;
            AspectRatio = aspectRatio;
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt( Position, Position + _front, _up );
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView( _fov, AspectRatio, ZNear, ZFar );
        }

        private void UpdateVectors()
        {
            _front.X = (float) Math.Cos( _pitch ) * (float) Math.Cos( _yaw );
            _front.Y = (float) Math.Sin( _pitch );
            _front.Z = (float) Math.Cos( _pitch ) * (float) Math.Sin( _yaw );
            _front.Normalize();

            _right = Vector3.Normalize( Vector3.Cross( _front, Vector3.UnitY ) );
            _up = Vector3.Normalize( Vector3.Cross( _right, _front ) );
        }
    }
}
