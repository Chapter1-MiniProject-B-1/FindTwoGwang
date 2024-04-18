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
        gameOverText.SetActive(true);
    }

    // 팀 카드를 선택하는 함수
    public void SelectTeamCard(TeamCard teamCard)
    {
        if (this.selectedTeamCard != null && this.selectedTeamCard != teamCard)
        {
            // 이미 다른 카드가 선택되어 있으면 해제 후 새 카드 선택
            this.selectedTeamCard.ToggleSelection();  // 기존 카드의 선택 상태 해제
        }
        this.selectedTeamCard = teamCard;
    }

    // 팀 카드를 선택 해제하는 함수
    public void DeselectTeamCard(TeamCard teamCard)
    {
        if (this.selectedTeamCard == teamCard)
        {
            this.selectedTeamCard = null;  // 선택된 팀 카드 해제
        }
    }

    // 선택된 카드와 팀 카드의 일치 여부 검사
    public void CheckMatch()
    {
        if (selectedCard != null && selectedTeamCard != null)
        {
            if (selectedCard.teamIndex == selectedTeamCard.teamCardIndex)
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
            selectedCard = null;
        }
    }

    // 매칭 성공 시 수행할 로직
    private void MatchSuccess()
    {
        // 매칭 성공 시 팀원의 이름 표시
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

        //오디오 클립을 재생
        audioSource.PlayOneShot(matchClip);

        // 카드 일치 시 파괴
        selectedCard.DestroyCard();
        cardCount -= 1;

        //자신의 카드를 다 찾으면 팀 카드를 삭제 해주는 코드
        selectedTeamCard.life -= 1;
        if(selectedTeamCard.life ==0)
        {
            selectedTeamCard.DestroyTeamCard();
        }

        // 모든 카드가 매칭되면 게임 종료
        if (cardCount == 0)
        {
            Invoke("GameClear", 0.5f);
            //Time.timeScale = 0.0f;
            //retryButton.SetActive(true);
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
    /*private void ResetSelectionAndColor()
    {
        if (selectedTeamCard != null)
        {
            // 선택된 팀 카드 상태 리셋
            selectedTeamCard.ResetSelection();
        }

        // 선택된 카드와 팀 카드 리셋
        selectedCard = null;
        selectedTeamCard = null;
    }*/

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

    void GameClear()
    {
        Time.timeScale = 0.0f;
        retryButton.SetActive(true);
        gameClearText.SetActive(true);
    }
}
