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
        particle.position.x = transform.position.x;
        particle.position.y = transform.position.y;
        particle.position.z = transform.position.z;
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
        particle.forceAccum = movementVector;
        particle.Integrate(Time.fixedDeltaTime);
        transform.position = particle.position.CycloneToUnity();
    }
}
