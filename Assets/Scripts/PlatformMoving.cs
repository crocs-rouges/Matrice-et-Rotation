using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject finishPoint;
    [SerializeField, Range(0, 10)] private float speed = 3f;
    private Rigidbody rb;

    private bool goIn = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (goIn)
            transform.position = Vector3.MoveTowards(transform.position,
             finishPoint.transform.position, speed * Time.deltaTime);
        else transform.position = Vector3.MoveTowards(transform.position,
        startPoint.transform.position, speed * Time.deltaTime);

        if (transform.position == finishPoint.transform.position)
            goIn = false;
        else if (transform.position == startPoint.transform.position)
            goIn = true;
    }
}
