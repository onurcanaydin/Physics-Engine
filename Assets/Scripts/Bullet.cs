using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Particle = cyclone.Particle;

public class Bullet : MonoBehaviour
{
    public Particle bulletParticle;
    // Start is called before the first frame update
    void Awake()
    {
        bulletParticle = new Particle();
        bulletParticle.SetPosition(transform.position.x, transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        bulletParticle.Integrate(Time.fixedDeltaTime);
        transform.position = bulletParticle.GetPosition().CycloneToUnity();
    }
}
