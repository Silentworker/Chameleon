using UnityEngine;

namespace Assets.Script.PlayGround.Shot
{
    public interface IShotFactory
    {
        void ReturnShotToPool(GameObject shot);
    }
}