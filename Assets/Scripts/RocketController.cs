using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public AudioSource aSource;
    public AudioClip explosion;
    public GameObject pExplosion;

    private Rigidbody rigid;
    private Vector3 shootDir;
    private bool HitGround;
    public void Setup(Vector3 shootDir, float RocketForce)
    {
        this.shootDir = shootDir;
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = shootDir * RocketForce;
        Destroy(gameObject, 5f);
        HitGround = false;
    }
    private void Update()
    {
        //transform.rotation = Quaternion.LookRotation(rigid.velocity) * initialRotation;
        transform.rotation = Quaternion.LookRotation(shootDir) * Quaternion.AngleAxis(90, Vector3.right);
        //For Straight line:
        /*if(!StopMotion)
        {
            float moveSpeed = 100f;
            transform.position += shootDir * moveSpeed * Time.deltaTime;
        }*/
    }

    /// <summary>
    /// OnTriggerEnter: This checks if the rocket hits, or lands close to, an enemy and if so, call the hit function on that enemy.
    /// </summary>
    /// <param name="other">other: the collider that the Rocket collided with.</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Physics.IgnoreCollision(this.gameObject.GetComponent<BoxCollider>(), other);
            return;
        }
        if (!HitGround)
        {
            rigid.velocity = Vector3.one;
            HitGround = true;
            if (other.CompareTag("Enemy")) //if we directly hit an enemy
            {
                other.GetComponent<EnemyController>().Hit();
            }
            else //if we hit near an enemey
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // all explosions currently happening
                GameObject Nearest = null;
                float distance = 21;
                foreach (GameObject enemy in enemies)
                {
                    Vector3 dist = enemy.transform.position - transform.position; // distance
                    float curDist = dist.sqrMagnitude;
                    if (curDist < distance)
                    {
                        Nearest = enemy;
                        distance = curDist;
                    }
                }

                if (distance < 20 && Nearest != null)
                {
                    Nearest.GetComponent<EnemyController>().Hit();
                }
            }
            this.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(RocketHit(aSource, explosion));
        }
    }

    /// <summary>
    /// RocketHit: This is called when the rocket hits anything, it plays an audio clip and creates an explosion then destroys itself.
    /// </summary>
    /// <param name="Source">Source: the Audio Source needed to play the clip.</param>
    /// <param name="Clip">Clip: The Audio Clip to play on impact.</param>
    /// <returns>N/A</returns>
    IEnumerator RocketHit(AudioSource Source, AudioClip Clip)
    {
        Source.PlayOneShot(Clip);
        GameObject explosionObject = Instantiate(pExplosion, transform.position, Quaternion.identity);
        explosionObject.transform.SetParent(this.gameObject.transform);
        yield return new WaitWhile (() => aSource.isPlaying);
        Destroy(this.gameObject);
    }
}
