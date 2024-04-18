using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    //��, �ڸ�
    public GameObject front;
    public GameObject back;

    //�ո� �̹���
    public SpriteRenderer frontImage;

    //�޸� �̹���
    public SpriteRenderer backImage;

    //�ִϸ�����
    public Animator anim;

    //����� ����
    AudioSource audioSource; //����� �ҽ�
    public AudioClip audioClip; //����� Ŭ��

    public int teamIndex = 0;

    void Start()
    {
        //����� �ҽ��� ����
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenCard()
    {
        // �� ī�尡 �������� �ʾҴٸ� ����
        if (GameManager.Instance.selectedTeamCard == null)
        {
            return;
        }

        //ī�� �ִϸ��̼ǿ� �ִ� IsOpen���� true�� ����
        anim.SetBool("IsOpen", true);

        //����� Ŭ���� ���
        audioSource.PlayOneShot(audioClip);

        //ī�尡 ������ ��, ���������� �Լ�
        StartCoroutine(CoroutineOpenCard());

        //ī�带 1���̶� ����������, ���� ȸ������ �� ����
        if(backImage.color == Color.white)
        {
            backImage.color = new Color(200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f);
        }

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
    public void Setting(int teamIdx, int imgIdx)
    {
        teamIndex = teamIdx;

        // �̹��� ������ ���Ǵ� ���� �̸�
        string imageName = $"team{teamIndex}_img{imgIdx}";

        // ī�� �ո� ��������Ʈ�� �̹��� �Ҵ�
        frontImage.sprite = Resources.Load<Sprite>(imageName);
    }
}
