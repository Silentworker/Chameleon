using System.Runtime.InteropServices;
using Assets.Script.Consts;
using Assets.Script.PlayGround.Enemy;
using Assets.Script.PlayGround.Shot;
using Assets.Script.Score;
using UnityEngine;
using Zenject;

namespace Assets.Script.Boundary
{
    public class DestroyOnLeave : MonoBehaviour
    {
        [Inject]
        private IShotFactory shotFactory;
        [Inject]
        private IScoreManager scoreManager;
        [Inject]
        private IEnemyFactory enemyFactory;

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == Tag.Shot)
            {
                shotFactory.ReturnShotToPool(other.gameObject);
            }
            else if (other.tag == Tag.Enemy)
            {
                enemyFactory.ReturnToPool(other.gameObject);

                Enemy enemyComponent = other.GetComponent<Enemy>();
                if (!enemyComponent.Dead)
                {
                    scoreManager.AddScore(Consts.Score.MissEnemy);
                }
            }
        }
    }
}
