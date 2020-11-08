using UnityEngine;

public class RobotForwardCommand : Command<Robot>
{
    private float totalTime = 0;

    public void update(float deltaTime, Robot robot)
    {
        robot.GetComponent<Rigidbody>().velocity = robot.transform.forward * robot.Speed;
        totalTime += deltaTime;
    }

    public bool isComplete(Robot robot)
    {
        return totalTime >= 1;
    }

    public void onTerminate(float deltaTime, Robot robot)
    {
        robot.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
