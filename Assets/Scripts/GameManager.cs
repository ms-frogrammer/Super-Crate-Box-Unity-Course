using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private float enemyMinRate = 1f;
    [SerializeField] private float enemyMaxRate = 2f;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy_big;
    [SerializeField] private GameObject enemySkull;
    [SerializeField] private float enemySkullChance = 0.25f;
    [SerializeField] private float bigEnemyChance = 1/10f;
   [SerializeField] private bool isGameOver = false;

    private float enemyRate = 0f;

    [SerializeField] private Transform[] cratePoints;
    public GameObject crate;

    private int score = -1;
    public Text text;
    public GameObject gameOverPanel;
    public Text gameOverScoretext;

    private int crateIndex = 0;
    ScreenShake camShake;
    // Start is called before the first frame update
    void Start()
    {
        crateIndex = Random.Range(0, cratePoints.Length);
        SpawnCrate();

        camShake = FindObjectOfType<ScreenShake>().GetComponent<ScreenShake>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            if (enemyRate <= 0)
            {
                GameObject enemy;
                if (Random.Range(0f, 1f) <= bigEnemyChance) {
                    enemy = Instantiate(enemy_big);
                }
                else if (Random.Range(0f, 1f) <= enemySkullChance)
                {
                    enemy = Instantiate(enemySkull);
                }
                else enemy = Instantiate(enemy1);

                enemy.transform.position = transform.position;
                if (Random.Range(0, 2) == 1) {
                    if (enemy.TryGetComponent(out EnemyMovement_1 movement)) {
                        movement.Flip();
                    }
                }
                enemyRate = Random.Range(enemyMinRate, enemyMaxRate);
            }
            else enemyRate -= Time.deltaTime;

            text.text = score.ToString();
        }
        else {
            if (Input.GetButtonDown("Restart")) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

    }

    public void SpawnCrate() {

        score++;
        List<int> leftsIndexes = new List<int>();
        for (int i = 0; i < cratePoints.Length; i++)
        {
            if (i != crateIndex)
            {
                leftsIndexes.Add(i);
            }
            else continue;
        }
        crateIndex = leftsIndexes[Random.Range(0, leftsIndexes.Count)];
        Instantiate(crate).transform.position = cratePoints[crateIndex].position;
    }

    public void GameOver() {
        isGameOver = true;
        text.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
        gameOverScoretext.text = "Collected " + score.ToString() + " Crates";

        StartCoroutine(camShake.Shake(.15f, .4f));
    }
}
