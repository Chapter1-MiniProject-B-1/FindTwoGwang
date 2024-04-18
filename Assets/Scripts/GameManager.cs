using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TeamCard selectedTeamCard;
    public Card selectedCard;

    public Text timeTxt;
    public GameObject retryButton;
    public GameObject gameOverText;
    public GameObject gameClearText;

    private TimerShake timershake;
    private GameObject obj;

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

        //Ÿ�̸� �ؽ�Ʈ ������Ʈ�� Ÿ�̸� ��ũ��Ʈ ����
        if (obj = GameObject.Find("TimeTxt"))
        {
            if (!obj.TryGetComponent(out timershake))
            {
                Debug.Log("GameManager.cs - Awake() - alert ���� ����");
            }
        }
    }

    void Start()
    {
        // AudioSource ������Ʈ ����
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

        // �ð��� �˹� �� �� ���̸ӿ��� ��� ���
        if (totalTime <= 10.0f && totalTime > 0.0f)
        {
            //10�� ������ �Ӱ� ���ϴ� �ִϸ��̼� Ʈ���� �۵� �Լ� ȣ��
            timershake.AlertTime();
        }
    }

    // ���� ���� ����
    void GameOver()
    {
        totalTime = 0.0f;
        Time.timeScale = 0.0f;
        retryButton.SetActive(true);
        gameOverText.SetActive(true);
    }

    // �� ī�带 �����ϴ� �Լ�
    public void SelectTeamCard(TeamCard teamCard)
    {
        if (this.selectedTeamCard != null && this.selectedTeamCard != teamCard)
        {
            // �̹� �ٸ� ī�尡 ���õǾ� ������ ���� �� �� ī�� ����
            this.selectedTeamCard.ToggleSelection();  // ���� ī���� ���� ���� ����
        }
        this.selectedTeamCard = teamCard;
    }

    // �� ī�带 ���� �����ϴ� �Լ�
    public void DeselectTeamCard(TeamCard teamCard)
    {
        if (this.selectedTeamCard == teamCard)
        {
            this.selectedTeamCard = null;  // ���õ� �� ī�� ����
        }
    }

    // ���õ� ī��� �� ī���� ��ġ ���� �˻�
    public void CheckMatch()
    {
        if (selectedCard != null && selectedTeamCard != null)
        {
            if (selectedCard.teamIndex == selectedTeamCard.teamCardIndex)
            {
                // ��Ī ����
                MatchSuccess();
            }
            else
            {
                // ��Ī ����
                MatchFail();
            }

            // ���� ���� �� ������ ����
            selectedCard = null;
        }
    }

    // ��Ī ���� �� ������ ����
    private void MatchSuccess()
    {
        // ��Ī ���� �� ������ �̸� ǥ��
        if (selectedTeamCard.teamCardIndex == 0)
        {
            Kim.SetActive(true);
            Invoke("KimInvoke", 0.5f);
        }
        if (selectedTeamCard.teamCardIndex == 1)
        {
            Park.SetActive(true);
            Invoke("ParkInvoke", 0.5f);
        }
        if (selectedTeamCard.teamCardIndex == 2)
        {
            Bae.SetActive(true);
            Invoke("BaeInvoke", 0.5f);
        }
        if (selectedTeamCard.teamCardIndex == 3)
        {
            Jeong.SetActive(true);
            Invoke("JeongInvoke", 0.5f);
        }
        if (selectedTeamCard.teamCardIndex == 4)
        {
            Jin.SetActive(true);
            Invoke("JinInvoke", 0.5f);
        }

        //����� Ŭ���� ���
        audioSource.PlayOneShot(matchClip);

        // ī�� ��ġ �� �ı�
        selectedCard.DestroyCard();
        cardCount -= 1;

        //�ڽ��� ī�带 �� ã���� �� ī�带 ���� ���ִ� �ڵ�
        selectedTeamCard.life -= 1;
        if(selectedTeamCard.life ==0)
        {
            selectedTeamCard.DestroyTeamCard();
        }

        // ��� ī�尡 ��Ī�Ǹ� ���� ����
        if (cardCount == 0)
        {
            Invoke("GameClear", 0.5f);
            //Time.timeScale = 0.0f;
            //retryButton.SetActive(true);
        }
    }

    // ��Ī ���� �� ������ ����
    private void MatchFail()
    {
        Fail.gameObject.SetActive(true);
        Invoke("FailInvoke", 0.5f);

        //����� Ŭ���� ���
        audioSource.PlayOneShot(failClip);

        // ��ġ���� ������ ī�� �ݱ�
        selectedCard.CloseCard();
    }

    // ���õ� ī��� �� ī���� ���� �� ���� ����
    /*private void ResetSelectionAndColor()
    {
        if (selectedTeamCard != null)
        {
            // ���õ� �� ī�� ���� ����
            selectedTeamCard.ResetSelection();
        }

        // ���õ� ī��� �� ī�� ����
        selectedCard = null;
        selectedTeamCard = null;
    }*/

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

    void GameClear()
    {
        Time.timeScale = 0.0f;
        retryButton.SetActive(true);
        gameClearText.SetActive(true);
    }
}
