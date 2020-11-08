using System.Collections.Generic;

public class CommandQueue<T>
{
    public string Name { get; private set; }
    private Queue<Command<T>> commands;

    public CommandQueue(string name)
    {
        this.Name = name;
        commands = new Queue<Command<T>>();
    }

    public bool isEmpty()
    {
        return commands.Count == 0;
    }

    public Command<T> dequeue()
    {
        if (isEmpty())
            return null;
        return commands.Dequeue();
    }

    public void enqueue(Command<T> command)
    {
        commands.Enqueue(command);
    }

}
