using UnityEngine;

public class ObjectiveTracker : MonoBehaviour
{
    public int RequiredRobotCount;
    private int robotCount;

    // Start is called before the first frame update
    void Start()
    {
        robotCount = 0;
    }

    public bool ObjectiveCompleted {
        get {
            return robotCount >= RequiredRobotCount;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Robot"))
            robotCount++;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Robot"))
            robotCount--;
    }
}
