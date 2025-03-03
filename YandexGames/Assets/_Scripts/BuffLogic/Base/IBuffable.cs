namespace _Scripts.BuffLogic
{
    public interface IBuffable
    {
        public void AddBuff(IBuff buff);
        public void RemoveBuff(IBuff buff);
    }
}