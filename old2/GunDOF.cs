using UnityEngine;

public class GunDOF : MonoBehaviour
{
    public int damagePerShot;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    float timer;

    Ray shootRay;
    RaycastHit shootHit;
    public AudioSource shootSound;
    public AudioClip laserImpactSound;

    public ParticleSystem shootParticles;

    Light gunLight;
    float effectsDisplayTime = 0.2f;

    public GameObject ImpactFX;

    public float throwForce;
    public float grabDistance;

    public GameObject Emitter;
    public GameObject Bullet1;
    public float BulletForce;




    void Awake()
    {
        shootParticles = GetComponent<ParticleSystem>();
        shootSound = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    void Update()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = true;


        timer += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && timer >= timeBetweenBullets)
        {
            ShootPlus();
        }

        if (Input.GetButtonDown("Fire2") && timer >= timeBetweenBullets)
        {
            ShootMinus();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLight.enabled = false;
    }

    public void ShootPlus()
    {
        timer = 0f;
        shootSound.Play();
        shootSound.volume = Random.Range(0.8f, 1);
        shootSound.pitch = Random.Range(0.8f, 1.1f);
        gunLight.enabled = true;
        shootParticles.Stop();
        shootParticles.Play();
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;


        if (Physics.Raycast(shootRay, out shootHit, range))
        {
            StructureHealth structureHealth = shootHit.collider.GetComponent<StructureHealth>();

            if (structureHealth != null)
            {
                structureHealth.addDegreeOfFreedom(damagePerShot, shootHit.point);
            }

            GameObject impactGO = Instantiate(ImpactFX, shootHit.point, Quaternion.LookRotation(shootHit.normal));
            Destroy(impactGO, 1f);
            AudioSource.PlayClipAtPoint(laserImpactSound, shootHit.point);
        }

        else
        {
            //gunLine.SetPosition(0, shootRay.origin + shootRay.direction * range);
        }
    }

    public void ShootMinus()
    {
        timer = 0f;
        shootSound.Play();
        shootSound.volume = Random.Range(0.8f, 1);
        shootSound.pitch = Random.Range(0.6f, 1.1f);
        gunLight.enabled = true;
        shootParticles.Stop();
        shootParticles.Play();
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range))
        {
            StructureHealth structureHealth = shootHit.collider.GetComponent<StructureHealth>();

            if (structureHealth != null)
            {
                structureHealth.deleteDegreeOfFreedom(damagePerShot, shootHit.point);
            }

            GameObject impactGO = Instantiate(ImpactFX, shootHit.point, Quaternion.LookRotation(shootHit.normal));
            Destroy(impactGO, 1f);
            AudioSource.PlayClipAtPoint(laserImpactSound, shootHit.point);
        }

        else
        {
            //gunLine.SetPosition(0, shootRay.origin + shootRay.direction * range);
        }
    }
}

/*
void Shoot1()
{
    GameObject TempBullet;
    TempBullet = Instantiate(Bullet1, Emitter.transform.position, Emitter.transform.rotation) as GameObject;
    TempBullet.transform.Rotate(Vector3.forward * 90);
    Rigidbody Temporary_RigidBody;
    Temporary_RigidBody = TempBullet.GetComponent<Rigidbody>();
    //Temporary_RigidBody.AddForce(transform.right * BulletForce);
    Temporary_RigidBody.AddForce(Emitter.transform.forward * BulletForce);

    Destroy(TempBullet, 3.0f);

}
*/
