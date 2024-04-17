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
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
        }

        // 시간 설정 초기화
        Time.timeScale = 1.0f;
    }

    void Start()
    {
        //오디오 소스를 얻어옴
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 타이머 감소 및 시간 표시 업데이트
        totalTime -= Time.deltaTime;
        timeTxt.text = totalTime.ToString("N2");

        // 시간이 0 이하면 게임 종료 호출
        if (totalTime <= 0.0f)
        {
            GameOver();
        }

        // TODO: 시간이 촉박 할 때 게이머에게 경고 기능
    }

    // 게임 종료 로직
    void GameOver()
    {
        totalTime = 0.0f;
        Invoke("TimeStop", 0.5f);
        retryButton.SetActive(true);
    }

    // 게임 시간 멈춤
    void TimeStop()
    {
        Time.timeScale = 0.0f;
    }

    // 팀 카드를 선택하는 함수
    public void SelectTeamCard(TeamCard teamCard)
    {
        // 이미 선택된 팀 카드가 있으면 추가 선택 불가
        if (this.teamCard != null)
            return;

        // 새 팀 카드 선택
        this.teamCard = teamCard;
    }

    // 선택된 카드와 팀 카드의 일치 여부 검사
    public void CheckMatch()
    {
        // 팀 카드와 하단 카드 모두 선택했고, 두 카드의 인덱스가 같을 때
        if (selectedCard != null && teamCard != null && selectedCard.teamIdx == teamCard.teamCardIndex)
        {
            // 매칭 성공 시 팀원의 이름 표시
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

            //오디오 클립을 재생
            audioSource.PlayOneShot(matchClip);

            // 카드 일치 시 파괴
            selectedCard.DestroyCard();
            cardCount -= 1;

            // 모든 카드가 매칭되면 게임 종료
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

            //오디오 클립을 재생
            audioSource.PlayOneShot(failClip);

            // 일치하지 않으면 카드 닫기
            selectedCard.CloseCard();
        }

        // 선택된 카드와 팀 카드 초기화
        selectedCard = null;
        teamCard = null;
    }

    //표시된 이름 및 실패 표시 비활성화
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
