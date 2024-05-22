using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private float enemyMinRate = 1f;
    [SerializeField] private float enemyMaxRate = 2f;
    [SerializeField] private GameObject enemy1;

    private float enemyRate = 0f;

    [SerializeField] private Transform[] cratePoints;
    public GameObject crate;

    private int score = -1;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyRate <= 0)
        {
            GameObject enemy = Instantiate(enemy1);
            enemy.transform.position = transform.position;
            if(Random.Range(0, 2) == 1) enemy.GetComponent<EnemyMovement_1>().Flip();
            enemyRate = Random.Range(enemyMinRate, enemyMaxRate);
        }
        else enemyRate -= Time.deltaTime;

        if (!GameObject.FindGameObjectWithTag("Crate")) {
            SpawnCrate();
        }
        text.text = score.ToString();
    }

    void SpawnCrate() {
        score++;
        Instantiate(crate).transform.position = cratePoints[Random.Range(0, cratePoints.Length)].position;
    }
}
