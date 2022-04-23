using UnityEngine;
using UniRx;
public class BossGate : MonoBehaviour
{
    void Start()
    {
		// �X�C�b�`���ɐG���ƃQ�[�g���J��
		MessageBroker.Default.Receive<GameEvent.EventData>()
			.Where(data => data.Type == GameEvent.EventType.BossGateGimmick)
			.FirstOrDefault()
			.Subscribe(_ =>
			{
				this.gameObject.SetActive(false);
				UIManager.Instance.ShowDialog("�{�X�������J������܂���", 3f);
			}).AddTo(this);
	}
}
