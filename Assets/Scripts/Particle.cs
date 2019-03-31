using UnityEngine;

namespace cyclone
{
    public class Particle
    {
        public Vector3 position;
        public Vector3 velocity;
        public Vector3 acceleration;
        public Vector3 forceAccum;
        public float damping;
        public float inverseMass;

        public Particle()
        {
            position = new Vector3();
            velocity = new Vector3();
            acceleration = new Vector3();
            forceAccum = new Vector3();
            damping = 0.995f;
            inverseMass = 1f;
        }

        public void Integrate(float duration)
        {
            if (inverseMass <= 0)
            {
                return;
            }

            if (duration <= 0)
            {
                return;
            }

            position.AddScaledVector(velocity, duration);
            /*should be uncommented for games with short acceleration bursts
            position.AddScaledVector(acceleration, duration * duration * 0.5f);*/

            Vector3 resultingAcc = new Vector3(acceleration.x, acceleration.y, acceleration.z);
            resultingAcc.AddScaledVector(forceAccum, inverseMass);

            velocity.AddScaledVector(resultingAcc, duration);

            velocity *= Mathf.Pow(damping, duration);

            ClearAccumulator();
        }

        private void ClearAccumulator()
        {
            forceAccum.x = 0;
            forceAccum.y = 0;
            forceAccum.z = 0;
        }
    }
}

