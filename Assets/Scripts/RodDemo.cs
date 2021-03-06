﻿using cyclone;
using UnityEngine;
using Vector3 = cyclone.Vector3;

public class RodDemo : MonoBehaviour
{
    private ParticleRod rod;

    [SerializeField]
    private GameObject movableObject;
    [SerializeField]
    private GameObject fixedObject;
    [SerializeField]
    private bool up = false;
    [SerializeField]
    private bool right = false;
    [SerializeField]
    private bool left = false;
    [SerializeField]
    private bool down = false;

    public Particle movableParticle;
    public Particle fixedParticle;

    private void Awake()
    {
        movableParticle = new Particle();
        Vector3 position = new Vector3(movableObject.transform.position.x, movableObject.transform.position.y, movableObject.transform.position.z);
        movableParticle.SetPosition(position.x, position.y, position.z);
        movableParticle.SetMass(1f);
        movableParticle.SetDamping(0.9f);

        fixedParticle = new Particle();
        position = new Vector3(fixedObject.transform.position.x, fixedObject.transform.position.y, fixedObject.transform.position.z);
        fixedParticle.SetPosition(position.x, position.y, position.z);
        fixedParticle.SetMass(0f);
        fixedParticle.SetDamping(0.9f);

        rod = new ParticleRod();
        rod.maxLength = (fixedParticle.GetPosition() - movableParticle.GetPosition()).Magnitude();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            up = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            left = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            right = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            down = true;
        }
    }

    private void FixedUpdate()
    {
        ParticleContact particleContact = new ParticleContact(movableParticle, fixedParticle, 0f, new Vector3());
        rod.fillContact(particleContact);

        Vector3 accVector = new Vector3();

        if (right == true)
        {
            accVector += new Vector3(20f, 0, 0);
            right = false;
        }
        if (left == true)
        {
            accVector += new Vector3(-20f, 0, 0);
            left = false;
        }
        if (up == true)
        {
            accVector += new Vector3(0, 20f, 0);
            up = false;
        }
        if (down == true)
        {
            accVector += new Vector3(0, -20f, 0);
            down = false;
        }

        movableParticle.SetForceAccum(accVector.x, accVector.y, accVector.z);
        movableParticle.Integrate(Time.fixedDeltaTime);
        movableObject.transform.position = movableParticle.GetPosition().CycloneToUnity();
    }
}
