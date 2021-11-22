using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public enum CharacterType
	{
		Player,
		AI,
	}

	[SerializeField] private CharacterType characterType;
	[SerializeField] private SpriteRenderer characterSprite = null;
	[SerializeField] private Animator animator = null;

	public CharacterType Type => characterType;
	public SpriteRenderer CharacterSprite => characterSprite;
	public Animator Animator => animator;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
