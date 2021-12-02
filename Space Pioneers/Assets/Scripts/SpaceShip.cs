using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShip : MonoBehaviour
{
    private Scene scene;

    public GameObject explosion;
    private ParticleSystem boom;
    bool exploded; // variavel de controle para garantir que a nave so vai explodir uma vez

    public GameObject spaceShipModel;
    private MeshRenderer spaceShipMesh;

    public GameObject smallFlame1;
    public GameObject smallFlame2;
    private ParticleSystem flames1;
    private ParticleSystem flames2;

    public AudioSource winningSound;
    public AudioSource boomSound;

    public GameObject levelEnd;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();

        boom = explosion.GetComponent<ParticleSystem>();
        spaceShipMesh = spaceShipModel.GetComponent<MeshRenderer>();
        flames1 = smallFlame1.GetComponent<ParticleSystem>();
        flames2 = smallFlame2.GetComponent<ParticleSystem>();

        spaceShipMesh.enabled = true;

        exploded = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        // Se colidir com o objetivo/final da fase, não explode
        if (other.gameObject == levelEnd)
        {
            winningSound.Play();
            return;
        }
        if(boom == null)
        {
            return;
        }

        if (!exploded)
        { 
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            boom.Play();
            boomSound.Play();
            exploded = true;
            flames1.Stop();
            flames2.Stop();
            spaceShipMesh.enabled = false;
            StartCoroutine(GameOver());
        }

      
    }

    IEnumerator GameOver() 
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(scene.name);
    }

}
