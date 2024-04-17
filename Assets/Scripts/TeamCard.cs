using UnityEngine;
using UnityEngine.UI;

public class TeamCard : MonoBehaviour
{
    public TeamCard activeTeamCard;

    public int teamCardIndex;

    void Start()
    {
        // 카드의 인덱스를 부모 오브젝트 내 순서로 설정
        teamCardIndex = transform.GetSiblingIndex();

        // 버튼 컴포넌트를 가져오고
        Button button = GetComponent<Button>();

        if (button != null)
        {
            // 버튼 클릭 이벤트 리스너 설정
            button.onClick.AddListener(SelectTeamCard);
        }
    }

    // 팀 카드 클릭 시 실행되는 함수: 게임 매니저를 통해 팀 카드 선택 처리
    void SelectTeamCard()
    {
        // 게임 매니저가 존재하면
        if (GameManager.Instance != null)
        {
            // 현재 활성화된 팀 카드를 선택 처리
            GameManager.Instance.SelectTeamCard(activeTeamCard);
        }
    }
}
