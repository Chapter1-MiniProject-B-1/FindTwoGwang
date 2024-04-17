using UnityEngine;

public class TimerShake : MonoBehaviour
{
    private Animator anim;

    //애니메이션 컴퍼넌트 참조
    private void Awake()
    {
        if (!TryGetComponent(out anim))
        {
            Debug.Log("TimerShake.cs - Awake() - anim 참조 실패");
        }
    }

    public void AlertTime()
    {
        anim.SetTrigger("TimerShake");
    }
}
