using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask enemyLayer;
    public LayerMask crateLayer;
    public bool isDead = false;

    public GameObject[] guns;
    private int activeGun = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (Physics2D.OverlapCircle(transform.position, 0.1f, enemyLayer))
            {
                Died();
            }
        }
        else transform.Rotate(new Vector3(0, 0, 2f * transform.localScale.x));

        Collider2D crate = Physics2D.OverlapCircle(transform.position, 0.25f, crateLayer);
        if (crate) {
            SwitchGun();
            Destroy(crate.gameObject);
        }
    }
    void SwitchGun() {
        guns[activeGun].SetActive(false);
        activeGun = Random.Range(0, guns.Length);
        guns[activeGun].SetActive(true);
    }
    void Died() {
        isDead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity += new Vector2(0f, 15);
    }
}
