using UnityEngine;
using cyclone;
using Vector3 = cyclone.Vector3;

public class FireworksLauncher : MonoBehaviour
{
    private static int maxFireworks = 512;
    private const int ruleCount = 9;
    public Fireworks[] fireworks = new Fireworks[maxFireworks];
    public FireworksPrefab[] fireworksPrefab = new FireworksPrefab[maxFireworks];
    public Fireworks.FireworksRule[] rules = new Fireworks.FireworksRule[ruleCount];
    private int nextFirework;
    [SerializeField]
    private FireworksPrefab prefab;

    private void Start()
    {
        InitFireworksRules();
        nextFirework = 0;
    }

    void InitFireworksRules()
    {
        rules[0].init(2);
        rules[0].setParameters(
            1, // type
            0.5f, 1.4f, // age range
            new Vector3(-5, 25, -5), // min velocity
            new Vector3(5, 28, 5), // max velocity
            0.1f // damping
            );
        rules[0].payloads[0].set(3, 5);
        rules[0].payloads[1].set(5, 5);

        rules[1].init(1);
        rules[1].setParameters(
            2, // type
            0.5f, 1.0f, // age range
            new Vector3(-5, 10, -5), // min velocity
            new Vector3(5, 20, 5), // max velocity
            0.8f // damping
            );
        rules[1].payloads[0].set(4, 2);

        rules[2].init(0);
        rules[2].setParameters(
            3, // type
            0.5f, 1.5f, // age range
            new Vector3(-5, -5, -5), // min velocity
            new Vector3(5, 5, 5), // max velocity
            0.1f // damping
            );

        rules[3].init(0);
        rules[3].setParameters(
            4, // type
            0.25f, 0.5f, // age range
            new Vector3(-20, 5, -5), // min velocity
            new Vector3(20, 5, 5), // max velocity
            0.2f // damping
            );

        rules[4].init(1);
        rules[4].setParameters(
            5, // type
            0.5f, 1.0f, // age range
            new Vector3(-20, 2, -5), // min velocity
            new Vector3(20, 18, 5), // max velocity
            0.01f // damping
            );
        rules[4].payloads[0].set(3, 5);

        rules[5].init(0);
        rules[5].setParameters(
            6, // type
            3, 5, // age range
            new Vector3(-5, 5, -5), // min velocity
            new Vector3(5, 10, 5), // max velocity
            0.95f // damping
            );

        rules[6].init(1);
        rules[6].setParameters(
            7, // type
            4, 5, // age range
            new Vector3(-5, 50, -5), // min velocity
            new Vector3(5, 60, 5), // max velocity
            0.01f // damping
            );
        rules[6].payloads[0].set(8, 10);

        rules[7].init(0);
        rules[7].setParameters(
            8, // type
            0.25f, 0.5f, // age range
            new Vector3(-1, -1, -1), // min velocity
            new Vector3(1, 1, 1), // max velocity
            0.01f // damping
            );

        rules[8].init(0);
        rules[8].setParameters(
            9, // type
            3, 5, // age range
            new Vector3(-15, 10, -5), // min velocity
            new Vector3(15, 15, 5), // max velocity
            0.95f // damping
            );
    }

    void Create(int type, Fireworks fireworksParent)
    {
        Fireworks newFireworks = new Fireworks();
        fireworks[nextFirework] = newFireworks;
        Fireworks.FireworksRule rule = rules[type - 1];
        rule.Create(fireworks[nextFirework], fireworksParent);
        FireworksPrefab pre = Instantiate(prefab);
        fireworksPrefab[nextFirework] = pre;
        pre.SetStartPosition(fireworks[nextFirework]);
        nextFirework = (nextFirework + 1) % maxFireworks;
    }

    void Create(int type, int number, Fireworks fireworksParent)
    {
        for(int i=0; i<number; i++)
        {
            Create(type, fireworksParent);
        }
    }

    private void FixedUpdate()
    {
        for(int i=0; i<fireworks.Length; i++)
        {
            if(fireworks[i] != null && fireworks[i].type > 0)
            {
                if (fireworks[i].Update(Time.fixedDeltaTime))
                {
                    Fireworks.FireworksRule rule = rules[fireworks[i].type - 1];
                    fireworks[i].type = 0;
                    for(int j=0; j<rule.payloadCount; j++)
                    {
                        Fireworks.FireworksRule.Payload payload = rule.payloads[j];
                        Create(payload.type, payload.count, fireworks[i]);
                    }
                    Destroy(fireworksPrefab[i].gameObject);
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Create(1, 1, null);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Create(2, 1, null);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Create(3, 1, null);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Create(4, 1, null);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Create(5, 1, null);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Create(6, 1, null);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Create(7, 1, null);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Create(8, 1, null);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Create(9, 1, null);
        }
    }
}
