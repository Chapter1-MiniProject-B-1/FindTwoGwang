using UnityEngine;
using UnityEngine.UI;

public class TeamCard : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public int teamCardIndex;
    private bool isSelected = false;

    void Start()
    {
        // ī���� �ε����� �θ� ������Ʈ �� ������ ����
        teamCardIndex = transform.GetSiblingIndex();

        // Sprite Renderer ������Ʈ ����
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // �� ī�� ���� ���¸� ��ȯ�ϰ� ���� �Ŵ����� ���� ������Ʈ
    public void ToggleSelection()
    {
        // ���� ���� ��ȯ
        isSelected = !isSelected;

        // ī�� ���� ������Ʈ
        UpdateCardColor();

        // ���õ� ���¸� GameManager�� ī�带 ���õ� ������ ���, �ƴϸ� ���� ����
        if (isSelected)
        {
            GameManager.Instance.SelectTeamCard(this);
        }
        else
        {
            GameManager.Instance.DeselectTeamCard(this);
        }
    }

    // ���� ���¿� ���� ī�� ������ ������Ʈ
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

    // ī���� ���� ���� �� ���� ����
    public void ResetSelection()
    {
        isSelected = false;
        spriteRenderer.color = Color.white;
    }
}
