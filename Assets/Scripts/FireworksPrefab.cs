using UnityEngine;

public class FireworksPrefab : MonoBehaviour
{
    public Fireworks fireworks;

    public void SetStartPosition(Fireworks fireworks)
    {
        this.fireworks = fireworks;
        transform.position = fireworks.GetPosition().CycloneToUnity();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fireworks.Integrate(Time.fixedDeltaTime);
        transform.position = fireworks.GetPosition().CycloneToUnity();
    }
}
