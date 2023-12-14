using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Target : MonoBehaviour
{

    private GameManager gameManager;
    private Rigidbody targetRb;
    [SerializeField]
    private AudioClip clip;
    
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;

    public int valuePoint;
    public bool isSpecial;
    public bool isBoom;
    public int minPoint;
    public int maxPoint;
    public float lifeTime;

    public ParticleSystem explosionParticle;

    private CameraShake cameraShake;


    void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();

        Invoke("DestroyByTime", lifeTime);

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        /*audioSource = GetComponent<AudioSource>();*/

        if (!gameManager.changedSpawnMethode)
        {


            targetRb = GetComponent<Rigidbody>();

            //Ajouter une force ¨¤ faire sauter les objets
            targetRb.AddForce(RandomForce(), ForceMode.Impulse);

            //Ajouter une rotation al¨¦atoire
            targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

            //Trouver une position ¨¤ g¨¦n¨¦rer les objets
            transform.position = RandomSpawnPos();
        }


        else
        {
            targetRb = GetComponent<Rigidbody>();

            //Ajouter une force ¨¤ faire sauter les objets
            targetRb.AddForce(RandomForce(), ForceMode.Impulse);

            //Ajouter une rotation al¨¦atoire
            targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

            transform.position = new Vector3(gameManager.xRange, ySpawnPos, 0);
            if (gameManager.xRange <= 4)
            {
                gameManager.xRange++;
            }
            else {
                gameManager.xRange = -4f;
            }
            
        }
        
    }
    void Update()
    {
    }

    //On destruit un objet par cliquer le souris
    private void OnMouseDown() {
        SoundsManager.Instance.PlaySound(clip);
        if (!gameManager.isOver)
        {
            
            Destroy(gameObject);
            if (isSpecial)
            {
                int randomPoint = Random.Range(minPoint, maxPoint);
                gameManager.UpdateScore(randomPoint);
            }
            else if (isBoom) 
            {
                cameraShake.StartShake(0.5f, 0.5f);
                gameManager.UpdateScore(valuePoint);
                gameManager.UpdateLife();
            }
            else
            {
                gameManager.UpdateScore(valuePoint);
            }
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);


        }

    }
    private void OnTriggerEnter(Collider collision)
    {
        SoundsManager.Instance.PlaySound(clip);
        /*Debug.Log("Cut");*/
        if (!gameManager.isOver && collision.gameObject.tag == "Trail")
        {
            
            Destroy(gameObject);
            if (isSpecial)
            {
                int randomPoint = Random.Range(minPoint, maxPoint);
                gameManager.UpdateScore(randomPoint);
            }
            else if (isBoom)
            {
                gameManager.UpdateScore(valuePoint);
                gameManager.UpdateLife();
                gameManager.initialShake = 0.02F;
                gameManager.combo = 0;
                gameManager.comboText.text = "Combo: " + 0;
                gameManager.comboText.fontSize = 36;
                cameraShake.StartShake(0.5f, 0.5f);
                
            }
            else
            {
                gameManager.UpdateScore(valuePoint);
            }
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);


        }
        
    }
    



    void DestroyByTime()
    {
        Destroy(gameObject);
    }

    public Vector3 RandomForce() 
    {

        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    
    }

    public float RandomTorque()
    {

        return Random.Range(-maxTorque, maxTorque);

    }

    public Vector3 RandomSpawnPos()
    {

        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos, 0);

    }

    //Vector3 ConstantSpawnPos()
    //{

    //    return new Vector3(Random.Range(-xRange, xRange), ySpawnPos, 0);

    //}
}
