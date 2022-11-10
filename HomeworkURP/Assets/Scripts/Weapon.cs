using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject mine;
    [SerializeField] private GameObject flash;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform _mineSpawnPosition;
    [SerializeField] private Transform _bulletSpawnPosition;
    [SerializeField] bool mineOn;
    [SerializeField] bool shootOn;
    [SerializeField] float cooldownTime;
    [SerializeField] float launchVelocity = 600f;
    private float nextFireTime = 0;



    void Start()
    {
        mineOn = true;
        shootOn = true;

    }

    void Update()
    {
      if(Time.time > nextFireTime)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                if (!mineOn)
                {
                    Debug.Log("No Weapon");
                }
                else if (mineOn = true)
                {
                    Instantiate(mine, _mineSpawnPosition.position, _mineSpawnPosition.rotation);
                    nextFireTime = Time.time + cooldownTime;
                }

            }
            else if (Input.GetButtonDown("Fire2"))
            {
                if (!shootOn)
                {
                    Debug.Log("No Shooty stuff");
                }
                else if (shootOn = true)
                {
                    GameObject ball = Instantiate(bullet, _bulletSpawnPosition.position, _bulletSpawnPosition.rotation);
                    GameObject muzzleFlash = Instantiate(flash, _bulletSpawnPosition.position, _bulletSpawnPosition.rotation);
                    Destroy(muzzleFlash, 0.3f);
                    ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchVelocity, 0));
                    Destroy(ball, 2.0f);
                    nextFireTime = Time.time + cooldownTime;
                }

            }
        }
      


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Power"))
        {
            mineOn = true;
            shootOn = true;

        }
    }
}
