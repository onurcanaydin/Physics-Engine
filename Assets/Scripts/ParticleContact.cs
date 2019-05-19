using UnityEngine;
using cyclone;
using Vector3 = cyclone.Vector3;

public class ParticleContact
{
    public Particle[] particles;
    public float restitution;
    public Vector3 contactNormal = new Vector3(0, 0, 0);
    public float penetration;

    public ParticleContact(Particle particle, Particle secondParticle, float restitution, Vector3 contactNormal)
    {
        particles = new Particle[] {particle, secondParticle};
        this.restitution = restitution;
        this.contactNormal = contactNormal;
    }

    public void Resolve(float duration)
    {
        ResolveVelocity(duration);
        ResolveInterpenetration(duration);
    }

    private float CalculateSeparatingVelocity()
    {
        Vector3 relativeVelocity = particles[0].GetVelocity() - particles[1].GetVelocity();
        return relativeVelocity.ScalarProduct(contactNormal);
    }

    private void ResolveVelocity(float duration)
    {
        float separatingVelocity = CalculateSeparatingVelocity();

        if (-separatingVelocity > 0)
        {
            return;
        }

        float newSeparatingVelocity = -separatingVelocity * restitution;

        Vector3 accelerationCausedVelocity = particles[0].GetAcceleration() - particles[1].GetAcceleration();

        float accelerationCausedSeparatingVelocity = accelerationCausedVelocity.ScalarProduct(contactNormal) * duration;

        if (accelerationCausedSeparatingVelocity < 0)
        {
            newSeparatingVelocity += restitution * accelerationCausedSeparatingVelocity;
            if (newSeparatingVelocity < 0)
            {
                newSeparatingVelocity = 0;
            }
        }

        float deltaVelocity = newSeparatingVelocity - separatingVelocity;
        float totalInverseMass = particles[0].GetInverseMass() + particles[1].GetInverseMass();
        if (totalInverseMass <= 0)
        {
            return;
        }

        float impulse = deltaVelocity / totalInverseMass;
        Vector3 impulsePerInverseMass = contactNormal * impulse;
        Vector3 totalVelocity = particles[0].GetVelocity() + impulsePerInverseMass * particles[0].GetInverseMass();
        particles[0].SetVelocity(totalVelocity.x, totalVelocity.y, totalVelocity.z);
        totalVelocity = particles[1].GetVelocity() + impulsePerInverseMass * particles[1].GetInverseMass();
        particles[1].SetVelocity(totalVelocity.x, totalVelocity.y, totalVelocity.z);
    }

    private void ResolveInterpenetration(float duration)
    {
        if (penetration <= 0)
        {
            return;
        }

        float totalInverseMass = particles[0].GetInverseMass() + particles[1].GetInverseMass();
        if(totalInverseMass <= 0)
        {
            return;
        }
        Vector3 movePerInverseMass = contactNormal * (-penetration / totalInverseMass);

        Vector3 finalPosition = particles[0].GetPosition() + movePerInverseMass * particles[0].GetInverseMass();
        particles[0].SetPosition(finalPosition.x, finalPosition.y, finalPosition.z);
        finalPosition = particles[1].GetPosition() + movePerInverseMass * particles[1].GetInverseMass();
        particles[1].SetPosition(finalPosition.x, finalPosition.y, finalPosition.z);
    }
}
