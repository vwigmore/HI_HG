public interface IMoveGesture
{
    void movePlayer(GameObject player);
}

class Forward : IMoveGesture
{
    public void movePlayer(GameObject player)
    {
        player.GetComponent<PlayerController>().walkForward();
    }
}

class Backward : IMoveGesture
{
    public void movePlayer(GameObject player)
    {
        player.GetComponent<PlayerController>().walkBackwards();
    }
}

class RotateRight : IMoveGesture
{
    public void movePlayer(GameObject player)
    {
        player.GetComponent<PlayerController>().rotateRight();
    }
}