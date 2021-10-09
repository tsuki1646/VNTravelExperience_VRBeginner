using UnityEngine;
using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;

namespace CompleteProject
{
    public class GameOverManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;

        Animator anim;

        void Start()
        {
            anim = GetComponent<Animator>();
            this.UpdateAsObservable()
                .Where(_ => playerHealth.currentHealth <= 0)
                .Subscribe(_ => GameOver());
        }

        async Task GameOver()
        {
            await Task.Delay(2000);
            anim.SetTrigger("GameOver");
        }
    }
}
