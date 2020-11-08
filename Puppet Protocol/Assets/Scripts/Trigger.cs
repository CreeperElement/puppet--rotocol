using UnityEngine;

public class Trigger : MonoBehaviour
{

    public GameObject barrier;
    public Vector3 Delta;
    public float ActivationTimeLimit;

    private bool activated;
    private float pressTime;

    private Vector3 startLocation;
    private Vector3 endLocation;

    // Start is called before the first frame update
    void Start()
    {
        startLocation = barrier.transform.position;
        endLocation = startLocation + Delta;
        pressTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            pressTime += Time.deltaTime;
            if (pressTime >= ActivationTimeLimit)
                pressTime = ActivationTimeLimit;
        } else
        {
            pressTime -= Time.deltaTime;
            if (pressTime <= 0)
                pressTime = 0;
        }

        barrier.transform.position = Vector3.Lerp(startLocation, endLocation, pressTime / ActivationTimeLimit);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Robot"))
            activated = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Robot"))
            activated = false;
    }
}
