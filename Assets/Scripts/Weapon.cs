using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cyclone;
using Vector3 = cyclone.Vector3;
using Particle = cyclone.Particle;

public class Weapon : MonoBehaviour
{

    enum WeaponType
    {
        FIREBALL, LASER, PISTOL, ARTILLERY
    };

    [SerializeField]
    private WeaponType type;
    public Bullet bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            Fire();
        }
    }

    public virtual void Fire()
    {
        Bullet bul = Instantiate(bullet, transform.position, Quaternion.identity);

        if (type == WeaponType.FIREBALL)
        {
            bul.bulletParticle.SetMass(8.0f);
            bul.bulletParticle.SetVelocity(-80.0f, 0.0f, 0.0f);
            bul.bulletParticle.SetAcceleration(0.0f, 4.8f, 0.0f);
            bul.bulletParticle.SetDamping(0.9f);
        }

        else if(type == WeaponType.LASER)
        {
            bul.bulletParticle.SetMass(0.8f);
            bul.bulletParticle.SetVelocity(-800.0f, 0.0f, 0.0f);
            bul.bulletParticle.SetAcceleration(0.0f, 0.0f, 0.0f);
            bul.bulletParticle.SetDamping(0.99f);
        }

        else if (type == WeaponType.PISTOL)
        {
            bul.bulletParticle.SetMass(16.0f);
            bul.bulletParticle.SetVelocity(-280.0f, 0.0f, 0.0f);
            bul.bulletParticle.SetAcceleration(0.0f, -8.0f, 0.0f);
            bul.bulletParticle.SetDamping(0.99f);
        }

        else if (type == WeaponType.ARTILLERY)
        {
            bul.bulletParticle.SetMass(1600.0f);
            bul.bulletParticle.SetVelocity(-320.0f, 240.0f, 0.0f);
            bul.bulletParticle.SetAcceleration(0.0f, -160.0f, 0.0f);
            bul.bulletParticle.SetDamping(0.99f);
        }
    }
}
