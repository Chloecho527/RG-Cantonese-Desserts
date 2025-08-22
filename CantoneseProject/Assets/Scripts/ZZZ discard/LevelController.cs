using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    // public float waveTimer;             // 计时器
    public GameObject successPanel;     // 通关面板
    public GameObject failPanel;        // 死亡面板


    protected override void Awake()
    {
        successPanel = GameObject.Find("SuccessPanel");
        failPanel = GameObject.Find("FailPanel");
    }

    private void Start()
    {
        // waveTimer = 15 + 5 * GameManager.Instance.currentWave;
    }

    private void Update()
    {
        // if (waveTimer > 0)
        // {
        //     waveTimer -= Time.deltaTime;
        //     
        //     if (waveTimer <= 0)
        //     {
        //         waveTimer = 0;
        //         SuccessGame();
        //     }
        // }
        //
        // GamePanel.Instance.RenewCountDown(waveTimer);
    }
    
    
    public void SuccessGame()
    {
        successPanel.GetComponent<CanvasGroup>().alpha = 1;
        
        // TODO go menu
    }
   
    public void FailGame()
    {
        failPanel.GetComponent<CanvasGroup>().alpha = 1;
    }
    
    
    
}
