public class ReadyMoveNextPointState : AbstractPlayerStates
{
    public override bool LevelEndReached() => false;

    public override bool ReadyToKillEnemies() => false;

    public override bool ReadyToMove() => true;
}
