using UnityEngine;

public class TimerShake : MonoBehaviour
{
    private Animator anim;

    //�ִϸ��̼� ���۳�Ʈ ����
    private void Awake()
    {
        if (!TryGetComponent(out anim))
        {
            Debug.Log("TimerShake.cs - Awake() - anim ���� ����");
        }
    }

    public void AlertTime()
    {
        anim.SetTrigger("TimerShake");
    }
}
