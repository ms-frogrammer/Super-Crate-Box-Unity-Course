using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask enemyLayer;
    public LayerMask crateLayer;
    public bool isDead = false;
    public GameManager gameManager;
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

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crate") {
            //SwitchGun();
            Destroy(collision.gameObject);
            gameManager.SpawnCrate();
        }
    }

    void SwitchGun() {
        guns[activeGun].SetActive(false);
        List<int> leftGuns = new List<int>();
        for (int i = 0; i < guns.Length; i++)
        {
            if (i != activeGun) {
                leftGuns.Add(i);
            } 
            else continue;
        }
        activeGun = leftGuns[Random.Range(0, leftGuns.Count)];
        guns[activeGun].SetActive(true);

    }
    void Died() {
        isDead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity += new Vector2(0f, 15);
        gameManager.GameOver();
    }
}
