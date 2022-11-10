using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private GameObject boom;
    [SerializeField] private Transform _bulletPosition;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Walls"))
        {
            GameObject bulletFlash =Instantiate(boom, _bulletPosition.position, _bulletPosition.rotation);
            Destroy(bulletFlash, 0.5f);
        }
    }
}
