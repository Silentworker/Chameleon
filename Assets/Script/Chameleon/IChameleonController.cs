namespace Assets.Script.Chameleon
{
    public interface IChameleonController
    {
        void Walk();
        void Idle();
        void Attack(float angle);
    }
}