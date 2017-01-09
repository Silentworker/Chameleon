using UnityEngine;

namespace Assets.Script.PlayGround
{
    public class PlayGroundController : MonoBehaviour, IPlayGroundController
    {
        public ConstantSpeedMoving Clouds;
        public ConstantSpeedMoving Water;
        public ConstantSpeedMoving Fog;
        public ConstantSpeedMoving Leafs;
        public ConstantSpeedMoving Liana;

        public void Move()
        {
            Clouds.Move();
            Water.Move();
            Fog.Move();
            Leafs.Move();
            Liana.Move();
        }

        public void Stop()
        {
            Clouds.Stop();
            Water.Stop();
            Fog.Stop();
            Leafs.Stop();
            Liana.Stop();
        }
    }
}
