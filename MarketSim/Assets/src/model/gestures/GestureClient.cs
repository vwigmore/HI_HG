public class GestureClient
{
    private IMoveGesture gestureStrategy;

    public GestureClient(IMoveGesture strategy)
    {
        gestureStrategy = strategy;
    }

    public void movePlayer(GameObject player)
    {
        gestureStrategy.movePlayer(player);
    }
}