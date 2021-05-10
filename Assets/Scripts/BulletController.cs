using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public AudioSource aSource;
    public AudioClip hit;
    public GameObject pExplosion;

    private float BulletSpeed;
    private bool StopMotion;
    private Vector3 shootDir;
    public void Setup(Vector3 shootDir, float BulletSpeed)
    {
        this.BulletSpeed = BulletSpeed;
        this.shootDir = shootDir;
        Destroy(gameObject, 5f);
        StopMotion = false;
        transform.localScale = new Vector3(2f, 2f, 2f);
    }
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(shootDir) * Quaternion.AngleAxis(90, Vector3.right);
        //For Straight line:
        if(!StopMotion)
        {
            float moveSpeed = BulletSpeed;
            transform.position += shootDir * moveSpeed * Time.deltaTime;
        }
    }

    /// <summary>
    /// OnTriggerEnter: Checks if the Enemies bullet collided with the player, if so, then call the players TankHit function to decrease it's health.
    /// </summary>
    /// <param name="other">other: the collider that the Bullet collided with.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Physics.IgnoreCollision(this.gameObject.GetComponent<BoxCollider>(), other);
            return;
        }
 
        if (other.CompareTag("Player")) //if we directly hit an enemy
        {
            StopMotion = true;
            other.GetComponent<TankController>().TankHit();
        }
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f); //can't make it inactive so we have to make it so small that no one can see it when it stops.
        StartCoroutine(BulletHit(aSource, hit));
    }

    /// <summary>
    /// BulletHit: This is called when the Bullet hits anything, it plays an audio clip and creates an explosion then destroys itself.
    /// </summary>
    /// <param name="Source">Source: the Audio Source needed to play the clip.</param>
    /// <param name="Clip">Clip: The Audio Clip to play on impact.</param>
    /// <returns>N/A</returns>
    IEnumerator BulletHit(AudioSource Source, AudioClip Clip)
    {
        Source.PlayOneShot(Clip);
        GameObject explosionObject = Instantiate(pExplosion, transform.position, Quaternion.identity);
        explosionObject.transform.SetParent(this.gameObject.transform);
        yield return new WaitWhile(() => aSource.isPlaying);
        Destroy(this.gameObject);
    }
}
