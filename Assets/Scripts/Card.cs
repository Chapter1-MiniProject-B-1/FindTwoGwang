using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    //��, �ڸ�
    public GameObject front;
    public GameObject back;

    //�ո� �̹���
    public SpriteRenderer frontImage;

    //�ִϸ�����
    public Animator anim;

    //����� ����
    AudioSource audioSource; //����� �ҽ�
    public AudioClip audioClip; //����� Ŭ��

    public int idx = 0;
    public int teamIdx = 0;

    void Start()
    {
        //����� �ҽ��� ����
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenCard()
    {
        // �� ī�尡 �������� �ʾҴٸ� ����
        if (GameManager.Instance.teamCard == null)
        {
            return;
        }

        //ī�� �ִϸ��̼ǿ� �ִ� IsOpen���� true�� ����
        anim.SetBool("IsOpen", true);

        //����� Ŭ���� ���
        audioSource.PlayOneShot(audioClip);

        //ī�尡 ������ ��, ���������� �Լ�
        StartCoroutine(CoroutineOpenCard());

        // ���õ� ī�尡 ������
        if (GameManager.Instance.selectedCard == null) 
        {
            // ���� ī�带 ���õ� ī��� �����ϰ�
            GameManager.Instance.selectedCard = this;

            // ���õ� ī��� �� ī���� ��ġ ���� �˻�
            GameManager.Instance.CheckMatch(); 
        }
    }

    public void CloseCard()
    {
        //ī�尡 �ݾ��� ��, ���������� �Լ�
        StartCoroutine(CoroutineCloseCard()); 
    }

    public void DestroyCard()
    {
        // ī�� ������Ʈ �ı�
        Destroy(gameObject, 0.5f);
    }

    IEnumerator CoroutineOpenCard()
    {
        //0.1�� �Ŀ� �ؿ� �ִ� ������� ������ (������ ������ ���� �ð� ���)
        yield return new WaitForSeconds(0.1f);

        //�ո� Ȱ��ȭ
        front.SetActive(true);

        //�޸� ��Ȱ��ȭ
        back.SetActive(false);
    }

    IEnumerator CoroutineCloseCard()
    {
        //1�� ������ ��, ī�尡 ����
        yield return new WaitForSeconds(1.0f);

        //ī�� �ִϸ��̼ǿ� �ִ� IsOpen���� false ����
        anim.SetBool("IsOpen", false);

        //�ո� ��Ȱ��ȭ
        front.SetActive(false);

        //�޸� Ȱ��ȭ
        back.SetActive(true); 
    }

    // �ε����� ���� �̹��� ����
    public void Setting(int number, int teamCardIndex, int imageIndex)
    {
        idx = number;
        teamIdx = teamCardIndex;

        // �̹��� ������ ���Ǵ� ���� �̸�
        string imageName = $"team{teamIdx}_img{imageIndex}";

        // ī�� �ո� ��������Ʈ�� �̹��� �Ҵ�
        frontImage.sprite = Resources.Load<Sprite>(imageName);
    }
}
