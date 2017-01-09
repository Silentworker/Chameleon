using Assets.Script.Chameleon;
using Assets.Script.PlayGround;
using UnityEngine;
using Zenject;

namespace Assets.Script
{
    public class ApplicationInit : MonoBehaviour
    {
        [Inject]
        IPlayGroundController playGroundController;
        [Inject]
        IChameleonController chameleonController;

        void Start()
        {
            playGroundController.Move();
            chameleonController.Walk();
        }
    }
}
