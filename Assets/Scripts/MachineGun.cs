using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public Transform gunPoint;
    public GameObject bullet;
    [SerializeField] private float fireMaxRate = 0.5f;
    [SerializeField] private int damage = 3;
    [SerializeField] private float bulletSpd = 40;
    [SerializeField] private float spread = 10;
    private float fireRate = 0;

    [SerializeField] private float shakeDur = .15f;
    [SerializeField] private float shakeStr = .4f;
    ScreenShake camShake;
    // Start is called before the first frame update
    void Start()
    {
        camShake = FindObjectOfType<ScreenShake>().GetComponent<ScreenShake>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Shoot") && fireRate <= 0)
        {
            GameObject _bullet = Instantiate(bullet);
            _bullet.transform.position = gunPoint.position;
            _bullet.transform.rotation = Quaternion.Euler(0, 0, (transform.parent.localScale.x > 0 ? 0f : 180f) + Random.Range(-spread, spread));
            Bullet _bulletComp = _bullet.GetComponent<Bullet>();
            _bulletComp.spd = bulletSpd;
            _bulletComp.damage = damage;

            fireRate = fireMaxRate;
            StartCoroutine(camShake.Shake(shakeDur, shakeStr));
        }
        if (fireRate > 0) fireRate -= Time.deltaTime;
    }
}
