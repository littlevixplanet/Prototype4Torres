using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PowerUp;

public class PlayerController : MonoBehaviour
{
    public bool hasPowerup;
    public GameObject powerupIndicator;
    private float powerupStrength = 15.0f;
    private GameObject focalPoint;
    public float speed = 3.0f;
    public Rigidbody rb;
    public PowerUpType currentPowerUp = PowerUpType.None; 
    public GameObject rocketPrefab; 
    private GameObject tmpRocket;
    public float hangTime; 
    public float smashSpeed; 
    public float explosionForce; 
    public float explosionRadius; 
    bool smashing =false; 
    float floorY;
    private Coroutine powerupCountdown;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Destroy(other.gameObject);
            if (powerupCountdown != null) 
            { 
                StopCoroutine(powerupCountdown); 
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        currentPowerUp = PowerUpType.None;
        powerupIndicator.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + currentPowerUp.ToString());
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
    void LaunchRockets() 
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        { 
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity); 
            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        } 
    }
    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        rb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, 2.0f, 0);
        if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F)) 
        { 
            LaunchRockets(); 
        }
    }
}
