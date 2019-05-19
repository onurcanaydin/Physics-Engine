using UnityEngine;
using Vector3 = cyclone.Vector3;

public class ParticleCable
{
    public float maxLength;
    public float restitution;

    public void fillContact(ParticleContact contact)
    {
        float length = (contact.particles[0].GetPosition() - contact.particles[1].GetPosition()).Magnitude();
        if (length < maxLength)
        {
            return;
        }

        Vector3 normal = contact.particles[0].GetPosition() - contact.particles[1].GetPosition();
        normal.Normalize();
        contact.contactNormal = normal;
        contact.penetration = length - maxLength;
        contact.restitution = restitution;

        contact.Resolve(Time.fixedDeltaTime);
    }
}
