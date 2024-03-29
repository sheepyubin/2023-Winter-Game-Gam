using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public float atkSpeed = 1;
    public bool attacked = false;
    public float moveSpeed;
    public float atkRange;
    public float fieldOfVision;
    private void SetEnemyStatus(string _enemyName, int _maxHp, int _atkDmg, float _atkSpeed, float _moveSpeed, float _atkRange, float _fieldOfVision)
    {
        enemyName = _enemyName;
        maxHp = _maxHp;
        nowHp = _maxHp;
        atkDmg = _atkDmg;
        atkSpeed = _atkSpeed;
        moveSpeed = _moveSpeed;
        atkRange = _atkRange;
        fieldOfVision = _fieldOfVision;
    }

    public Image nowHpbar;
    public GameObject prfHpBar;
    public GameObject canvas;

    RectTransform hpBar;
    public float height = 1.7f;

    public MoveScript Player;
    

    void Start()
    {
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        if (name.Equals("DarkDragon"))
        {
            SetEnemyStatus("DarkDragon", 100, 10, 1.5f, 2, 1.5f, 20f);
        }
        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
        SetAttackSpeed(atkSpeed);
    }
    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint
            (new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Player.attacked)
            {
                nowHp -= Player.atkDmg;
                Player.attacked = false;
                if (nowHp <= 0) // 적 사망
                {
                    Die();
                }
            }
        }
    }
    void Die()
    {
        //enemyAnimator.SetTrigger("die");            // die 애니메이션 실행
        GetComponent<EnemyAI>().enabled = false;    // 추적 비활성화
        GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
        Destroy(GetComponent<Rigidbody2D>());       // 중력 비활성화
        Destroy(gameObject, 3);                     // 3초후 제거
        Destroy(hpBar.gameObject, 3);               // 3초후 체력바 제거
    }
    void AttackTrue()
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }
    void SetAttackSpeed(float speed)
    {
    //  animator.SetFloat("attackSpeed", speed);
        atkSpeed = speed;
    }

    private void SetEnemyStatus(string _enemyName, int _maxHp, int _atkDmg, int _atkSpeed)
    {
        enemyName = _enemyName;
        maxHp = _maxHp;
        nowHp = _maxHp;
        atkDmg = _atkDmg;
        atkSpeed = _atkSpeed;
    }

    //public Sword_Man sword_man;
    //Image nowHpbar;

}
