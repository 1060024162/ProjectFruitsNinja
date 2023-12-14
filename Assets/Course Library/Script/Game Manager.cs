using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public GameObject knife;
    [SerializeField]
    private AudioSource menuBgm;
    [SerializeField]
    private AudioSource playingBgm;

    const float LINE_POS_Z = 10;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI gameoverText;
    public TextMeshProUGUI timeText;
    public Image[] lifes;
    public Button restart;

    private int score;
    public int combo;
    private int indication;
    private int spawnIndication;
    private float time = 60.0f;
    public float xRange;
    public int lifeindex;

    private float spawnRate = 1.0f;

    public bool isOver = false;
    private bool isStarted = false;
    public bool changedSpawnMethode = false;
  

    public GameObject titleUI;
    private CameraShake cameraShake;
    public float initialShake;


    private void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        initialShake = 0.02f;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            knife.SetActive(true);
            Debug.Log(knife.active);
            var mousPos = Input.mousePosition;
            knife.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousPos.x, mousPos.y, LINE_POS_Z));
            return;
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            knife.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            
            var mousPos = Input.mousePosition;
            knife.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousPos.x, mousPos.y, LINE_POS_Z));
        }
        //Syst¨¨me de chronom¨¦trage
        if (isStarted && !isOver) 
        {
            UpdateTime(Time.deltaTime);
            SelectSpawnStrategy();

        }
        if(time <= 0) 
        { 
            isOver = true;
            GameOver();
        }

        //Syst¨¨me de selecter la strat¨¦gie de g¨¦n¨¦rer
        //SelectSpawnStrategy();

     }

    public void StartGame(int difficulty) 
    {
        knife.SetActive(false);

        isOver = false;
        isStarted = true;
        spawnIndication = 0;
        StartCoroutine(SpawnTarget());
        

        time = 60.0f;
        xRange = -4f;
        score = 0;
        lifeindex = 2;
        indication = 0;
        combo = 0;
        
        UpdateScore(0);
        UpdateTime(0);

        menuBgm.Stop();
        playingBgm.Play();
        

        for (int i = 0; i < lifes.Length; i++) 
        {
            lifes[i].enabled = true;
        }

        titleUI.SetActive(false);
        spawnRate = spawnRate / difficulty;

    }

    public void GameOver() 
    {
        gameoverText.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
        isOver = true;

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void UpdateScore(int scoreToAdd) 
    {
        score += scoreToAdd;
        indication += scoreToAdd;
        
        scoreText.text = "Score : " + score;
        if (scoreToAdd == 0)
        {
            comboText.text = "Combo: " + combo;
        }
        else {
            combo += 1;
            comboText.text = "Combo: " + combo;
            comboText.fontSize += 1;
        }
        
        
    }

    public void UpdateTime(float timeToUpdate)
    {
        time -= timeToUpdate;
        timeText.text = "Time : " + time.ToString("F2");
    }

    public void UpdateLife()
    {
        if (lifeindex <=2 && lifeindex>= 0)
        {
            lifes[lifeindex].enabled = false;
            lifeindex--;
        }
        if (lifeindex < 0)
        {
            GameOver();
        }
        
        
    }

    //Continuer ¨¤ g¨¦n¨¦rer les objets selon un taux fourni
    IEnumerator SpawnTarget()
    {
        while (!isOver) {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            int random = Random.Range(5,6);
            if (changedSpawnMethode && spawnIndication % 10 < 10 && spawnIndication % 10 > random)
            {
                Instantiate(targets[index]);
            }
            else if (!changedSpawnMethode) 
            {
                Instantiate(targets[index]);
            }
            spawnIndication++;

        }
       
    }

    

    void SelectSpawnStrategy() {
       
        if (indication > 20) {

            indication -= 20;
            cameraShake.StartShake(10,initialShake);
            initialShake += 0.02f;

            if (changedSpawnMethode)
            {   
               
                changedSpawnMethode = false;
                spawnRate *= 5;
            }
            else if (!changedSpawnMethode)
            {
                
                changedSpawnMethode = true;
                spawnRate /= 5;
            }
        }
    }


}
