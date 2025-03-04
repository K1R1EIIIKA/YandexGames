namespace _Scripts.BuffLogic.Base
{
    public interface IBuffable
    {
        public void AddBuff(IBuff buff);
        public void RemoveBuff(IBuff buff);
    }
}