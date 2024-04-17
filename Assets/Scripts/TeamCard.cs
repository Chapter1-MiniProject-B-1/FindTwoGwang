using UnityEngine;
using UnityEngine.UI;

public class TeamCard : MonoBehaviour
{
    public TeamCard activeTeamCard;

    public int teamCardIndex;

    void Start()
    {
        // ī���� �ε����� �θ� ������Ʈ �� ������ ����
        teamCardIndex = transform.GetSiblingIndex();

        // ��ư ������Ʈ�� ��������
        Button button = GetComponent<Button>();

        if (button != null)
        {
            // ��ư Ŭ�� �̺�Ʈ ������ ����
            button.onClick.AddListener(SelectTeamCard);
        }
    }

    // �� ī�� Ŭ�� �� ����Ǵ� �Լ�: ���� �Ŵ����� ���� �� ī�� ���� ó��
    void SelectTeamCard()
    {
        // ���� �Ŵ����� �����ϸ�
        if (GameManager.Instance != null)
        {
            // ���� Ȱ��ȭ�� �� ī�带 ���� ó��
            GameManager.Instance.SelectTeamCard(activeTeamCard);
        }
    }
}
