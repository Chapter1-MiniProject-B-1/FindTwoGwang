using UnityEngine;
using UnityEngine.UI;

public class TeamCard : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public int teamCardIndex;
    private bool isSelected = false;

    void Start()
    {
        // 카드의 인덱스를 부모 오브젝트 내 순서로 설정
        teamCardIndex = transform.GetSiblingIndex();

        // Sprite Renderer 컴포넌트 참조
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // 팀 카드 선택 상태를 전환하고 게임 매니저에 상태 업데이트
    public void ToggleSelection()
    {
        // 선택 상태 전환
        isSelected = !isSelected;

        // 카드 색상 업데이트
        UpdateCardColor();

        // 선택된 상태면 GameManager에 카드를 선택된 것으로 등록, 아니면 선택 해제
        if (isSelected)
        {
            GameManager.Instance.SelectTeamCard(this);
        }
        else
        {
            GameManager.Instance.DeselectTeamCard(this);
        }
    }

    // 선택 상태에 따라 카드 색상을 업데이트
    private void UpdateCardColor()
    {
        if (isSelected)
        {
            spriteRenderer.color = Color.gray;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    // 카드의 선택 상태 및 색상 리셋
    public void ResetSelection()
    {
        isSelected = false;
        spriteRenderer.color = Color.white;
    }
}
