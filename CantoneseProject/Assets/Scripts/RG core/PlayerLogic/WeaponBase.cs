using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [Header("武器数据")]
    public WeaponData weaponData;      // 武器信息
    public GameObject bulletPrefab;    // 子弹预制体
    protected Transform muzzlePos;     // 枪口位置
    protected Transform shellPos;      // 弹仓位置
    protected Vector2 mousePos;        // 鼠标位置
    protected Vector2 direction;       // 发射方向
    protected float timer;             // A冷却计时器
    protected float flipY;             // 角色转身，武器翻转
    protected Animator animator;       // 武器动画机
    
    protected virtual void Start()
    {
        weaponData = GameManager.Instance.currentWeapon;   // 获取当前所选武器信息
        animator = GetComponent<Animator>();
        muzzlePos = transform.Find("Muzzle");
        shellPos = transform.Find("BulletShell");
        flipY = transform.localScale.y;
    }
    
    protected virtual void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x < transform.position.x)
            transform.localScale = new Vector3(flipY, -flipY, 1);
        else
            transform.localScale = new Vector3(flipY, flipY, 1);

        Shoot();
    }
    
    protected virtual void Shoot()
    {
        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = direction;

        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
            }
        }

        if (Input.GetButton("Fire1"))   // 按下鼠标左键，开火
        {
            if (timer == 0)
            {
                Fire();
                timer = weaponData.cooling;
            }
        }
    }

    // 武器开火
    protected virtual void Fire()
    {
        animator.SetTrigger("Shoot");
        
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = muzzlePos.position;

        float angel = Random.Range(-5f, 5f);
        bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(angel, Vector3.forward) * direction);
        
    }
}
