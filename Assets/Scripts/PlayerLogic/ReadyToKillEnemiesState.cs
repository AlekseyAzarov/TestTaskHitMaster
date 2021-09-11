public class ReadyToKillEnemiesState : AbstractPlayerStates
{
    public override bool LevelEndReached() => false;

    public override bool ReadyToKillEnemies() => true;

    public override bool ReadyToMove() => false;
}
