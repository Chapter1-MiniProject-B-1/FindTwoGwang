using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TeamCard teamCard;
    public Card selectedCard;

    public Text timeTxt;
    public GameObject retryButton;

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
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
        }

        // 시간 설정 초기화
        Time.timeScale = 1.0f;

        //타이머 텍스트 오브젝트의 타이머 스크립트 참조
        if (obj = GameObject.Find("TimeTxt"))
        {
            if (!obj.TryGetComponent(out timershake))
            {
                Debug.Log("GameManager.cs - Awake() - alert 참조 실패");
            }
        }
    }

    void Start()
    {
        // AudioSource 컴포넌트 참조
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

        // 시간이 촉박 할 때 게이머에게 경고 기능
        if (totalTime <= 10.0f && totalTime > 0.0f)
        {
            //10초 남으면 붉게 변하는 애니메이션 트리거 작동 함수 호출
            timershake.AlertTime();
        }
    }

    // 게임 종료 로직
    void GameOver()
    {
        totalTime = 0.0f;
        Time.timeScale = 0.0f;
        retryButton.SetActive(true);
    }

    // 팀 카드를 선택하는 함수
    public void SelectTeamCard(TeamCard teamCard)
    {
        if (this.teamCard != null && this.teamCard != teamCard)
        {
            // 이미 다른 카드가 선택되어 있으면 해제 후 새 카드 선택
            this.teamCard.ToggleSelection();  // 기존 카드의 선택 상태 해제
        }
        this.teamCard = teamCard;
    }

    // 팀 카드를 선택 해제하는 함수
    public void DeselectTeamCard(TeamCard teamCard)
    {
        if (this.teamCard == teamCard)
        {
            this.teamCard = null;  // 선택된 팀 카드 해제
        }
    }

    // 선택된 카드와 팀 카드의 일치 여부 검사
    public void CheckMatch()
    {
        if (selectedCard != null && teamCard != null)
        {
            if (selectedCard.teamIdx == teamCard.teamCardIndex)
            {
                // 매칭 성공
                MatchSuccess();
            }
            else
            {
                // 매칭 실패
                MatchFail();
            }

            // 선택 상태 및 색상을 리셋
            ResetSelectionAndColor();
        }
    }

    // 매칭 성공 시 수행할 로직
    private void MatchSuccess()
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
            Time.timeScale = 0.0f;
            retryButton.SetActive(true);
        }
    }

    // 매칭 실패 시 수행할 로직
    private void MatchFail()
    {
        Fail.gameObject.SetActive(true);
        Invoke("FailInvoke", 0.5f);

        //오디오 클립을 재생
        audioSource.PlayOneShot(failClip);

        // 일치하지 않으면 카드 닫기
        selectedCard.CloseCard();
    }

    // 선택된 카드와 팀 카드의 상태 및 색상 리셋
    private void ResetSelectionAndColor()
    {
        if (teamCard != null)
        {
            teamCard.ResetSelection();  // TeamCard 클래스에서 구현해야 함
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
