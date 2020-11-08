using UnityEngine;
using UnityEngine.AI;

public class RobotMoveCommand : Command<Robot>
{
    private Vector3 target;
    private bool locationSet;
    private float unstickingTime;

    public RobotMoveCommand(Vector3 target)
    {
        this.target = target;
        unstickingTime = 0;
    }

    public bool isComplete(Robot robot)
    {
        var navMeshAgent = robot.gameObject.GetComponent<NavMeshAgent>();
        var robotLocation = navMeshAgent.destination;

        var distance =  Vector3.Distance(navMeshAgent.gameObject.transform.position, robotLocation);
        return distance < .1;
    }

    public void onTerminate(float deltaTime, Robot robot)
    {
        robot.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
    }

    public void update(float deltaTime, Robot robot)
    {
        unstickingTime += Time.deltaTime;
        if (!locationSet)
        {
            robot.gameObject.GetComponent<NavMeshAgent>().SetDestination(target);
            locationSet = true;
        }
    }
}
