using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerDamage : MonoBehaviour
{
	[SerializeField] private Character.CharacterType damageType = Character.CharacterType.AI;
	[SerializeField] private LayerMask damagelayer;
	[SerializeField] private int damageToApply = 1;
	[SerializeField] private new SpriteRenderer renderer = null;

	private readonly float InvincibleTime = 1.0f; // 被ダメージ後の無敵時間

	private Health playerHealth;
	private SingleAssignmentDisposable animationDisposable;

	public bool IsInvincible { get; private set; } = false;

	private void Start()
	{
		playerHealth = this.gameObject.GetComponent<Health>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (IsInvincible) return;
		if (IsEnemyBullet(collision) || MyLibrary.CheckLayer(collision.gameObject.layer, damagelayer))
		{
			TakeDamage();
			// 点滅
			Flash();
			// 一定時間無敵
			IsInvincible = true;
			Observable.Timer(System.TimeSpan.FromSeconds(InvincibleTime))
				.Subscribe(_ => 
				{
					IsInvincible = false;
				}).AddTo(this);
		}
	}

	private bool IsEnemyBullet(Collider2D collision)
	{
		return collision.gameObject.tag == "Bullet"
			&& collision.gameObject.GetComponent<ProjectileBase>().WeaponOwner.Type == damageType;
	}

	private void Flash()
	{
		animationDisposable?.Dispose();
		animationDisposable = new SingleAssignmentDisposable();
		animationDisposable.Disposable = 
			Observable.Interval(System.TimeSpan.FromSeconds(0.1f))
			.Subscribe(_ => 
			{
				renderer.enabled = !renderer.enabled;
			}).AddTo(this);
		Observable.Timer(System.TimeSpan.FromSeconds(InvincibleTime))
			.Subscribe(_ => 
			{
				animationDisposable?.Dispose();
				renderer.enabled = true;
			}).AddTo(this);
	}

	private void TakeDamage()
	{
		playerHealth.TakeDamage(damageToApply);
	}
}
