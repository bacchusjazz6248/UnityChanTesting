using UnityEngine;

public abstract class MobStatus : MonoBehaviour
{
    protected enum StateEnum
    {
        Normal, // 通常
        Attack, // 攻撃中
        Die // 死亡
    }

    /// <summary>
    /// 移動可能かどうか
    /// </summary>
    public bool IsMovable => StateEnum.Normal == state;

    /// <summary>
    /// 攻撃可能かどうか
    /// </summary>
    public bool IsAttackable => StateEnum.Normal == state;

    /// <summary>
    /// ライフ最大値を返します
    /// </summary>
    public float LifeMax => lifeMax;

    /// <summary>
    /// ライフの値を返します
    /// </summary>
    public float Life => life;

    [SerializeField] private float lifeMax = 10; // ライフ最大値
    protected Animator animator;
    protected StateEnum state = StateEnum.Normal; // Mob状態
    private float life; // 現在のライフ値（ヒットポイント）

    protected virtual void Start()
    {
        life = lifeMax; // 初期状態はライフ満タン
        animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// キャラが倒れた時の処理を記述します。
    /// </summary>
    protected virtual void OnDie()
    {
    }

    /// <summary>
    /// 指定値のダメージを受けます。
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        if (state == StateEnum.Die) return;

        life -= damage;
        if (life > 0) return;

        state = StateEnum.Die;
        animator.SetTrigger("Die");

        OnDie();
    }

    /// <summary>
    /// 可能であれば攻撃中の状態に遷移します。
    /// </summary>
    public void GoToAttackStateIfPossible()
    {
        if (!IsAttackable) return;

        state = StateEnum.Attack;
        animator.SetTrigger("Attack");
    }

    /// <summary>
    /// 可能であればNormalの状態に遷移します。
    /// </summary>
    public void GoToNormalStateIfPossible()
    {
        if (state == StateEnum.Die) return;

        state = StateEnum.Normal;
    }
}