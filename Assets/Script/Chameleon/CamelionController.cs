using UnityEngine;

namespace Assets.Script
{
    public class CamelionController : MonoBehaviour
    {
        public Animator Head;
        public Animator Body;

        public void Walk()
        {
            Head.SetBool("Walk", true);
            Body.SetBool("Walk", true);
        }

        public void Idle()
        {
            Head.SetBool("Walk", false);
            Body.SetBool("Walk", false);
        }

        public void Attack(float angle)
        {
            Head.SetTrigger("Attack");
            Head.SetFloat("AttackAngle", angle);
            Body.SetTrigger("Attack");
        }
    }
}
