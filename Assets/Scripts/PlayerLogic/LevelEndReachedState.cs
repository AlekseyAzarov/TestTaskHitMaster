public class LevelEndReachedState : AbstractPlayerStates
{
    public override bool LevelEndReached() => true;

    public override bool ReadyToKillEnemies() => false;

    public override bool ReadyToMove() => false;
}
