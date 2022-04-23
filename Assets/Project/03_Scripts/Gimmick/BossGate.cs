using UnityEngine;
using UniRx;
public class BossGate : MonoBehaviour
{
    void Start()
    {
		// スイッチ等に触れるとゲートが開く
		MessageBroker.Default.Receive<GameEvent.EventData>()
			.Where(data => data.Type == GameEvent.EventType.BossGateGimmick)
			.FirstOrDefault()
			.Subscribe(_ =>
			{
				this.gameObject.SetActive(false);
				UIManager.Instance.ShowDialog("ボス部屋が開放されました", 3f);
			}).AddTo(this);
	}
}
