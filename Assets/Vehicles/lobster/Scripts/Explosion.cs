using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Applies an explosion force to all nearby rigidbodies

public class Explosion : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;
    public float explosionLift = 1.0F;
   // public float explosionDelay = 0.5F;
   // public GameObject Explosions;


    void Start()
    {

        Invoke("BombGoesOff", 0F);

        Invoke("LoadFailScreen", 5.0F);



    }

    void BombGoesOff()
    {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, explosionLift);

            }
           

        }

    void LoadFailScreen()
    {
        SceneManager.LoadScene("gameOver");
    }
    }



    

/*
GameObject destroyedCarGameObject = (GameObject)Instantiate(destroyedCarPrefab, carGameObject.transform.position, carGameObject.transform.rotation);
 Destroy(carGameObject);
 Destroy(explosionGameObject);


void Start()
{

    // if (Input.GetKeyDown(KeyCode.I))

   // yield WaitForSeconds(explosionDelay);
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, explosionLift);

            }


    }

}
*/
