public abstract class AbstractPlayerStates
{
    public abstract bool ReadyToMove();
    public abstract bool ReadyToKillEnemies();
    public abstract bool LevelEndReached();
}
