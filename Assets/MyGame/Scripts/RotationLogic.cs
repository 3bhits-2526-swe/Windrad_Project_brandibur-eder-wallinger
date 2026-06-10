using UnityEngine;

public class TurbineRotation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100f;

    void Update()
    {
        transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
    }
}

