using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GlobalQueue : MonoBehaviour
{
    List<GameObject> robots;
    List<GameObject> cameras;
    GameObject activeCamera;
    GameObject mapCam;
    ObjectiveTracker objectiveTraker;

    // Start is called before the first frame update
    void Start()
    {
        robots = GameObject.FindGameObjectsWithTag("Robot").ToList<GameObject>();
        cameras = GameObject.FindGameObjectsWithTag("Camera").ToList<GameObject>();
        foreach(var cam in cameras) { cam.SetActive(false);  }
        activeCamera = cameras.Find(cam => cam.name.Equals("001"));
        activeCamera.SetActive(true);

        mapCam = GameObject.Find("Map Camera");
        objectiveTraker = GameObject.Find("Exit").GetComponent<ObjectiveTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Returns a response to the terminal.
     */
    public string push(string command)
    {
        var commandComponents = command.Split(' ');
        switch(commandComponents[0])
        {
            case "move":
                return parseMove(commandComponents);
            case "help":
                return getHelpText();
            case "cam":
                return parseCamera(commandComponents);
            case "scan":
                return scan(commandComponents);
            case "clear":
                return clear(commandComponents);
            case "snap":
                return snapRobot(commandComponents);
            case "exit":
                Application.Quit();
                return "Closing";
            case "next":
                return parseNextLevel(commandComponents);
            default:
                return "Invalid Command: " + commandComponents[0] + "\nType: \"help\" for a list of commands";
        }
    }

    private string parseMove(string[] commandArgs)
    {
        if (commandArgs.Length < 4)
            return "Invalid Syntax:\nmove (rx) (direction) (x) (y)";

        var robotGameObject = robots.Find(robot =>
        {
            return robot.GetComponent<Robot>().queue.Name.Equals(commandArgs[1]);
        });

        if (robotGameObject == null)
            return $"No Robot Named: {commandArgs[1]}";

        RaycastHit hit;
        var x = float.Parse(commandArgs[2]);
        var y = float.Parse(commandArgs[3]);

        if (Physics.Raycast(new Vector3(x, 0, y) + mapCam.transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            robotGameObject.GetComponent<Robot>().queue.enqueue(new RobotMoveCommand(hit.point));
        } else
        {
            return $"Location: ({x}, {y}) is not a valid location.";
        }

        return $"{commandArgs[0]} {commandArgs[1]} {commandArgs[2]} {commandArgs[3]}";
    }

    private string getHelpText()
    {
        return
            "move (robot) (x) (y)\n" +
            "cam (camera name)\n" +
            "help\n"+ 
            "scan\n" + 
            "snap (robot)\n"+
            "next (Move on to next level)\n" +
            "exit !Closes The Program!";
    }

    private string parseCamera(string[] args)
    {
        if (args.Length < 2)
        {
            return "Invalid Syntax: cam (camera name)";
        }

        var camName = args[1];
        var camera = cameras.Find(cam => cam.name.Equals(args[1]));

        if (camera == null)
        {
            return $"Camera \"{args[1]}\" does not exist. Try scanning to find all cameras.";
        }

        activeCamera.SetActive(false);
        activeCamera = camera;
        activeCamera.SetActive(true);

        return $"{args[0]} {args[1]}";
    }

    private string scan(string[] args)
    {
        string output = "Cameras: ";
        foreach(var cam in cameras)
        {
            output += cam.name + ", ";
        }

        output = output.Substring(0, output.Length - 2);
        return output;
    }

    private string clear(string[] args)
    {
        var logGO = GameObject.Find("Canvas/Terminal/Terminal/Log");
        logGO.GetComponent<Text>().text = "";
        return "";
    }

    private string snapRobot(string[] args)
    {
        if (args.Length < 2)
            return "Invalid Syntax: move (robot)";

        var result = mapCam.GetComponent<MapCamera>().setTarget(args[1]);

        if (result != null)
            return result;

        return $"{args[0]} {args[1]}";
    }

    private string parseNextLevel(string[] args)
    {
        if (objectiveTraker.ObjectiveCompleted)
        {

            return "Thanks for playing the demo!";
        } else
        {
            return $"Must have {objectiveTraker.RequiredRobotCount} robots at the end to continue.";
        }
    }

}
