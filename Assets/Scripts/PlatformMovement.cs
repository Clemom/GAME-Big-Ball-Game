using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private float speedRotation = 30f;
    [SerializeField] private float maxRotation = 30f;

    private float currentRotationX = 0f;
    private float currentRotationZ = 0f;

    void Update()
    {
        float inputX = Input.GetAxis("Vertical");
        float inputZ = -Input.GetAxis("Horizontal");

        currentRotationX += inputX * speedRotation * Time.deltaTime;
        currentRotationZ += inputZ * speedRotation * Time.deltaTime;

        currentRotationX = Mathf.Clamp(currentRotationX, -maxRotation, maxRotation);
        currentRotationZ = Mathf.Clamp(currentRotationZ, -maxRotation, maxRotation);

        transform.rotation = Quaternion.Euler(currentRotationX, 0f, currentRotationZ);
    }
}
