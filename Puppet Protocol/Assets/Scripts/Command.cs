public interface Command <T>
{
    void update(float deltaTime, T obj);

    bool isComplete(T obj);

    void onTerminate(float deltaTime, T obj);
}
