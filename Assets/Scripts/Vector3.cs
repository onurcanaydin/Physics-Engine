using UnityEngine;

namespace cyclone
{
    public class Vector3
    {
        public float x;
        public float y;
        public float z;
        private float pad;

        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }
        public Vector3(float xComp, float yComp, float zComp)
        {
            x = xComp;
            y = yComp;
            z = zComp;
        }
        private void Invert()
        {
            x = -x;
            y = -y;
            z = -z;
        }
        private float Magnitude()
        {
            return Mathf.Sqrt(x * x + y * y + z * z);
        }
        private float SquareMagnitude()
        {
            return x * x + y * y + z * z;
        }
        private void Normalize()
        {
            float length = Magnitude();
            if (length > 0)
            {
                x /= length;
                y /= length;
                z /= length;
            }
        }
        public static Vector3 operator *(Vector3 vec, float value)
        {
            return new Vector3(vec.x * value, vec.y * value, vec.z * value);
        }
        public static Vector3 operator +(Vector3 vec, Vector3 vec2)
        {
            return new Vector3(vec.x + vec2.x, vec.y + vec2.y, vec.z + vec2.z);
        }
        public static Vector3 operator -(Vector3 vec, Vector3 vec2)
        {
            return new Vector3(vec.x - vec2.x, vec.y - vec2.y, vec.z - vec2.z);
        }
        public void AddScaledVector(Vector3 vec, float scale)
        {
            x += vec.x * scale;
            y += vec.y * scale;
            z += vec.z * scale;
        }
        private float ScalarProduct(Vector3 vec)
        {
            return x * vec.x + y * vec.y + z * vec.z;
        }
        private Vector3 CrossProduct(Vector3 vec)
        {
            return new Vector3(y * vec.z - z * vec.y, z * vec.x - x * vec.z, x * vec.y - y * vec.x);
        }
        public static Vector3 operator %(Vector3 vec, Vector3 vec2)
        {
            return vec.CrossProduct(vec2);
        }
        public UnityEngine.Vector3 CycloneToUnity()
        {
            return new UnityEngine.Vector3(x, y, z);
        }
    }
}