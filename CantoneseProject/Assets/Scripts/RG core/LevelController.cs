using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    public float waveTimer;             // 关卡倒计时器
    public GameObject successPanel;     // 通关面板
    public GameObject failPanel;        // 失败面板


    private void Awake()
    {
        successPanel = GameObject.Find("SuccessPanel");
        failPanel = GameObject.Find("FailPanel");
    }

    private void Start()
    {
        waveTimer = 15 + 5 * GameManager.Instance.currentWave;
    }

    private void Update()
    {
        if (waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
            
            if (waveTimer <= 0)
            {
                waveTimer = 0;
                SuccessGame();
            }
        }

        GamePanel.Instance.RenewCountDown(waveTimer);
    }
    
    // 生成敌人
    
    // 游戏胜利
    public void SuccessGame()
    {
        successPanel.GetComponent<CanvasGroup>().alpha = 1;
        
        // TODO go menu携程    视频位置：021 18：00
    }
    // 游戏失败
    public void FailGame()
    {
        failPanel.GetComponent<CanvasGroup>().alpha = 1;
    }
    
    // 波次完成
    
}
