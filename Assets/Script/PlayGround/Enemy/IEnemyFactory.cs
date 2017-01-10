using UnityEngine;
using Zenject;

namespace Assets.Script.PlayGround.Enemy
{
    public interface IEnemyFactory
    {
        void ReturnToPool(GameObject enemy);
    }
}