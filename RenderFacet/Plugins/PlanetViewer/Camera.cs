using System;
using System.Numerics;
using Vortice.Mathematics;

namespace PlanetViewer
{
    public class Camera
    {
        public const float PI = 3.1415926535897931f;
        private const float PiOver4 = PI / 4.0f;
        private const float PiOver2 = PI / 2.0f;

        private Vector3 _front = -Vector3.UnitZ;
        private Vector3 _up = Vector3.UnitY;
        private Vector3 _right = Vector3.UnitX;

        private float _pitch;
        private float _yaw = -PiOver2;
        private float _fov = PiOver2;

        public Camera(Vector3 position, float aspectRatio)
        {
            Position = position;
            AspectRatio = aspectRatio;
        }

        public Vector3 Position { get; set; }

        public float AspectRatio { private get; set; }

        public Vector3 Front => _front;

        public Vector3 Up => _up;

        public Vector3 Right => _right;

        public float Pitch
        {
            get => MathHelper.ToDegrees(_pitch);
            set
            {
                var angle = MathHelper.Clamp(value, -89f, 89f);
                _pitch = MathHelper.ToRadians(angle);
                UpdateVectors();
            }
        }

        public float Yaw
        {
            get => MathHelper.ToDegrees(_yaw);
            set
            {
                _yaw = MathHelper.ToRadians(value);
                UpdateVectors();
            }
        }

        public float Fov
        {
            get => MathHelper.ToDegrees(_fov);
            set
            {
                var angle = MathHelper.Clamp(value, 1f, 45f);
                _fov = MathHelper.ToRadians(angle);
            }
        }

        public Matrix4x4 GetViewMatrix()
        {
            return Matrix4x4.CreateLookAt(Position, Position + _front, _up);
        }

        public Matrix4x4 GetProjectionMatrix()
        {
            return Matrix4x4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 100f);
        }

        private void UpdateVectors()
        {
            var x = (float)Math.Cos(_pitch) * (float)Math.Cos(_yaw);
            var y = (float)Math.Sin(_pitch);
            var z = (float)Math.Cos(_pitch) * (float)Math.Sin(_yaw);

            _front = new Vector3(x, y, z);
            Vector3.Normalize(_front);

            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(_right, _front));
        }
    }
}
