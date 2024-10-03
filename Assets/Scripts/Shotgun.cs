using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public Transform gunPoint;
    public GameObject bullet;
    [SerializeField] private float fireMaxRate = 1f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float bulletSpd = 40;
    [SerializeField] private float spread = 20;
    [SerializeField] private int bullets = 7;
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
        if (Input.GetButtonDown("Shoot") && fireRate <= 0)
        {
            for (int i = 0; i < bullets; i++) {
                GameObject _bullet = Instantiate(bullet);
                _bullet.transform.position = gunPoint.position;
                _bullet.transform.rotation = Quaternion.Euler(0, 0, (transform.parent.localScale.x > 0 ? 0f : 180f) + Random.Range(-spread, spread));
                ShotgunBullet _bulletComp = _bullet.GetComponent<ShotgunBullet>();
                _bulletComp.spd = bulletSpd;
                _bulletComp.damage = damage;
            }

            fireRate = fireMaxRate;
            StartCoroutine(camShake.Shake(shakeDur, shakeStr));
        }
        if (fireRate > 0) fireRate -= Time.deltaTime;
    }
}
