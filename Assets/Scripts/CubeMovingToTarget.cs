using Unity.Mathematics;
using UnityEngine;

public class CubeMovingToTarget : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField, Range(0, 10)] private float speed;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
         target.transform.position, speed * Time.deltaTime);
    }
}
