using Assets.Script.Chameleon;
using Assets.Script.PlayGround;
using Assets.Script.PlayGround.Enemy;
using Assets.Script.PlayGround.Shot;
using Assets.Script.Score;
using Zenject;

namespace Assets.Script
{
    public class ApplicationInstaller : MonoInstaller<ApplicationInstaller>
    {
        public ChameleonController chameleonControllerInstance;
        public PlayGroundController playGroundControllerInstance;
        public ScoreManager scoreManagerInstance;
        public ShotFactory shotFactoryInstance;
        public EnemyFactory enemyFactoryInstance;

        public override void InstallBindings()
        {
            Container.Bind<IChameleonController>().FromInstance(chameleonControllerInstance);
            Container.Bind<IPlayGroundController>().FromInstance(playGroundControllerInstance);
            Container.Bind<IScoreManager>().FromInstance(scoreManagerInstance);
            Container.Bind<IShotFactory>().FromInstance(shotFactoryInstance);
            Container.Bind<IEnemyFactory>().FromInstance(enemyFactoryInstance);
        }
    }
}
