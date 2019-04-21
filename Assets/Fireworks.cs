using UnityEngine;
using cyclone;
using Vector3 = cyclone.Vector3;

public class Fireworks : Particle
{
    public int type;
    public float age;

    public bool Update(float duration)
    {
        Integrate(duration);
        age -= duration;
        return (age < 0);
    }

    public struct FireworksRule
    {
        int type;
        float minAge;
        float maxAge;
        Vector3 minVelocity;
        Vector3 maxVelocity;
        float damping;

        public struct Payload
        {
            public int type;
            public int count;

            public void set(int type, int count)
            {
                this.type = type;
                this.count = count;
            }
        }

        public int payloadCount;

        public Payload[] payloads;

        public void init(int payloadCount)
        {
            this.payloadCount = payloadCount;
            payloads = new Payload[payloadCount];
        }

        public void setParameters(int type, float minAge, float maxAge, Vector3 minVelocity, Vector3 maxVelocity, float damping)
        {
            this.type = type;
            this.minAge = minAge;
            this.maxAge = maxAge;
            this.minVelocity = minVelocity;
            this.maxVelocity = maxVelocity;
            this.damping = damping;
        }

        public void Create(Fireworks fireworks, Fireworks fireworksParent)
        {
            fireworks.type = type;
            fireworks.age = Random.Range(minAge, maxAge);
            Vector3 vel = new Vector3(0, 0, 0);
            if (fireworksParent != null)
            {
                Debug.Log("burda");
                Vector3 pos = fireworksParent.GetPosition();
                fireworks.SetPosition(pos.x, pos.y, pos.z);
                vel = fireworksParent.GetVelocity();
            }
            else
            {
                Debug.Log("hayir burda");
                fireworks.SetPosition(0, 0, 0);
            }
            Debug.Log(vel.x);
            Debug.Log(vel.y);
            Debug.Log(vel.z);

            //vel += maxVelocity;//Vector3.RandomVector3(minVelocity, maxVelocity);
            Vector3 gravity = new Vector3(0, -9.8f, 0);

            fireworks.SetVelocity(vel.x, vel.y, vel.z);
            fireworks.SetMass(1);
            fireworks.SetDamping(damping);
            fireworks.SetAcceleration(gravity.x, gravity.y, gravity.z);
            fireworks.ClearAccumulator();
        }
    }
}
