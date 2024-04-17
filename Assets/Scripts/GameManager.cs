using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TeamCard teamCard;
    public Card selectedCard;

    public Text timeTxt;
    public GameObject retryButton;

    public GameObject Kim;
    public GameObject Park;
    public GameObject Bae;
    public GameObject Jeong;
    public GameObject Jin;
    public GameObject Fail;

    private AudioSource audioSource;
    public AudioClip matchClip;
    public AudioClip failClip;

    public int cardCount = 0;
    private float totalTime = 60.0f;

    private void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
        }

        // �ð� ���� �ʱ�ȭ
        Time.timeScale = 1.0f;
    }

    void Start()
    {
        //����� �ҽ��� ����
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Ÿ�̸� ���� �� �ð� ǥ�� ������Ʈ
        totalTime -= Time.deltaTime;
        timeTxt.text = totalTime.ToString("N2");

        // �ð��� 0 ���ϸ� ���� ���� ȣ��
        if (totalTime <= 0.0f)
        {
            GameOver();
        }

        // TODO: �ð��� �˹� �� �� ���̸ӿ��� ��� ���
    }

    // ���� ���� ����
    void GameOver()
    {
        totalTime = 0.0f;
        Invoke("TimeStop", 0.5f);
        retryButton.SetActive(true);
    }

    // ���� �ð� ����
    void TimeStop()
    {
        Time.timeScale = 0.0f;
    }

    // �� ī�带 �����ϴ� �Լ�
    public void SelectTeamCard(TeamCard teamCard)
    {
        // �̹� ���õ� �� ī�尡 ������ �߰� ���� �Ұ�
        if (this.teamCard != null)
            return;

        // �� �� ī�� ����
        this.teamCard = teamCard;
    }

    // ���õ� ī��� �� ī���� ��ġ ���� �˻�
    public void CheckMatch()
    {
        // �� ī��� �ϴ� ī�� ��� �����߰�, �� ī���� �ε����� ���� ��
        if (selectedCard != null && teamCard != null && selectedCard.teamIdx == teamCard.teamCardIndex)
        {
            // ��Ī ���� �� ������ �̸� ǥ��
            if (teamCard.teamCardIndex == 0)
            {
                Kim.SetActive(true);
                Invoke("KimInvoke", 0.5f);
            }
            if (teamCard.teamCardIndex == 1)
            {
                Park.SetActive(true);
                Invoke("ParkInvoke", 0.5f);
            }
            if (teamCard.teamCardIndex == 2)
            {
                Bae.SetActive(true);
                Invoke("BaeInvoke", 0.5f);
            }
            if (teamCard.teamCardIndex == 3)
            {
                Jeong.SetActive(true);
                Invoke("JeongInvoke", 0.5f);
            }
            if (teamCard.teamCardIndex == 4)
            {
                Jin.SetActive(true);
                Invoke("JinInvoke", 0.5f);
            }

            //����� Ŭ���� ���
            audioSource.PlayOneShot(matchClip);

            // ī�� ��ġ �� �ı�
            selectedCard.DestroyCard();
            cardCount -= 1;

            // ��� ī�尡 ��Ī�Ǹ� ���� ����
            if (cardCount == 0)
            {
                Invoke("TimeStop", 0.5f);
                retryButton.SetActive(true);
            }
        }
        else
        {
            Fail.gameObject.SetActive(true);
            Invoke("FailInvoke", 0.5f);

            //����� Ŭ���� ���
            audioSource.PlayOneShot(failClip);

            // ��ġ���� ������ ī�� �ݱ�
            selectedCard.CloseCard();
        }

        // ���õ� ī��� �� ī�� �ʱ�ȭ
        selectedCard = null;
        teamCard = null;
    }

    //ǥ�õ� �̸� �� ���� ǥ�� ��Ȱ��ȭ
    void KimInvoke()
    {
        Kim.SetActive(false);
    }
    void ParkInvoke()
    {
        Park.SetActive(false);
    }
    void BaeInvoke()
    {
        Bae.SetActive(false);
    }
    void JeongInvoke()
    {
        Jeong.SetActive(false);
    }
    void JinInvoke()
    {
        Jin.SetActive(false);
    }
    void FailInvoke()
    {
        Fail.SetActive(false);
    }
}
