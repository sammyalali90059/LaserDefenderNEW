using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float health = 100;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float laserDelay = 0.3f;

    [SerializeField] GameObject laserPrefab;

    [SerializeField] AudioClip playerDeathSound;
    [SerializeField] [Range (0, 1)] float playerDeathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range (0, 1)] float shootSoundVolume = 0.75f;


    Coroutine fireCoroutine;
    float xMin;
    float yMin;
    float xMax;
    float yMax;
    [SerializeField] float padding = 0.5f;
    //Serialize Field makes the variable accessible from Unity Editor without making it public.

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
       // StartCoroutine(PrintAndWait());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    public void Move()
    {
        //var is used as a generic variable. VS allows us to use var and it will set its type depending on the value it will have.
        // deltaX will be updated with the input that will happend on the x-axis, left and right

        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        //the x-position will be updated according to the newXPost - whether left or right. Y position will be the same

        transform.position = new Vector2(newXPos, newYPos);

        
    }

    public void SetUpMoveBoundaries()
    {
        //setup the boundaries of the movement according to the camera
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    public void Fire()
    {
        //if a button linked to Fire1 is pressed
        if (Input.GetButtonDown("Fire1")) 
        {
           fireCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    IEnumerator PrintAndWait()
    {
        print("Message 1 sent!");

        yield return new WaitForSeconds(3f);

        print("Message 2 sent!");
    }
    IEnumerator FireContinuously()
    {
        while(true)
        {
            /*create an instance of the laserPrefab,
             at the position of the spaceship (this)
            with default rotation.. */
            GameObject laserClone = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laserClone.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(laserDelay);
        
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
            return;
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();

        if (health <= 0)
        {
            Die();

        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(playerDeathSound,Camera.main.transform.position,playerDeathSoundVolume);
        Destroy(gameObject);
    }

}
