using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicMob : MobsController {
    public float hp, damage, attackDst, attackSpeed, moveSpeed, maxMoveVelocity, maxAngularVelocity;
    public Image hpBar;

    private Transform canv, img;
    private float maxHp;
    // Use this for initialization
    void Start ()
    {
        HP = hp;
        Damage = damage;
        AttackDst = attackDst;
        AttackSpeed = attackSpeed;
        MoveSpeed = moveSpeed;
        MaxMoveVelocity = maxMoveVelocity;
        MaxAngularVelocity = maxAngularVelocity;

        maxHp = hp;
        canv = transform.parent;
        hpBar = canv.GetChild(2).GetChild(1).GetComponent<Image>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        MoveToPlayer();
        AttackPlayer();
	}

    public override void TakeDamage(float damage)
    {
        HP -= Mathf.Abs(damage) / 10;
        HP = Mathf.Clamp(HP, 0, maxHp);
        hpBar.fillAmount = HP / maxHp;

        if (HP == 0)
            transform.parent.GetComponent<Destroy>().DestroyThis();
    }
}
