namespace _Scripts.BuffLogic.Base
{
    public interface IIdentifiedBuff : IBuff
    {
        string Id { get; }
    }
}