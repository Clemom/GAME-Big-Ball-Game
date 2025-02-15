using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BallMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private float winVolume = 1.0f;

    [SerializeField] private AudioClip loseSound;
    [SerializeField] private float loseVolume = 1.0f;

    [SerializeField] private float growthFactor = 0.5f;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinWall"))
        {
            StartCoroutine(PlayWinSoundAndRestart());
        }
        else if (other.CompareTag("Trap"))
        {
            StartCoroutine(PlayLoseSoundAndRestart());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject hitObject = collision.collider.gameObject;
        if (hitObject.CompareTag("Wall"))
        {
            GrowBall();
        }
    }

    void GrowBall()
    {
        transform.localScale *= growthFactor;
    }

    IEnumerator PlayWinSoundAndRestart()
    {
        if (!rb.isKinematic)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        rb.isKinematic = true;

        if (winSound != null)
        {
            audioSource.PlayOneShot(winSound, winVolume);
            Material ballMaterial = GetComponent<Renderer>().material;
            Color originalColor = ballMaterial.color;
            ballMaterial.color = Color.green;

            yield return new WaitForSeconds(0.5f);

            ballMaterial.color = originalColor;
            yield return new WaitForSeconds(winSound.length);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator PlayLoseSoundAndRestart()
    {
        if (!rb.isKinematic)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        rb.isKinematic = true;

        if (loseSound != null)
        {
            audioSource.PlayOneShot(loseSound, loseVolume);
            Material ballMaterial = GetComponent<Renderer>().material;
            Color originalColor = ballMaterial.color;
            ballMaterial.color = Color.red;

            yield return new WaitForSeconds(0.5f);

            ballMaterial.color = originalColor;
            yield return new WaitForSeconds(loseSound.length);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
