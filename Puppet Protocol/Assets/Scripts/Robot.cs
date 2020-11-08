using UnityEngine;

public class Robot : MonoBehaviour
{
    public CommandQueue<Robot> queue { get; private set; }
    public float Speed = 10;
    public string Name;
    public bool Collision { get; private set; }

    private Command<Robot> currentCommand;

    // Start is called before the first frame update
    void Start()
    {
        queue = new CommandQueue<Robot>(Name);
        currentCommand = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCommand != null && currentCommand.isComplete(this))
            currentCommand.onTerminate(Time.deltaTime, this);
        if (queue.isEmpty())
        {
            currentCommand = null;
            return;
        } else
        {
            currentCommand = queue.dequeue();
        }
        currentCommand.update(Time.deltaTime, this);
    }
}
