using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float movementSpeed;
    public cyclone.Particle particle;

    void Start()
    {
        particle = new cyclone.Particle();
        particle.SetMass(1f);
        particle.SetDamping(0.995f);
        particle.SetPosition(transform.position.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        cyclone.Vector3 movementVector = new cyclone.Vector3(horizontal, 0, vertical);
        movementVector *= movementSpeed;
        particle.SetForceAccum(movementVector.x, movementVector.y, movementVector.z);
        particle.Integrate(Time.fixedDeltaTime);
        transform.position = particle.GetPosition().CycloneToUnity();
    }
}
