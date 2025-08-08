using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    public float waveTimer;             // �ؿ�����ʱ��
    public GameObject successPanel;     // ͨ�����
    public GameObject failPanel;        // ʧ�����


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
    
    // ���ɵ���
    
    // ��Ϸʤ��
    public void SuccessGame()
    {
        successPanel.GetComponent<CanvasGroup>().alpha = 1;
        
        // TODO go menuЯ��    ��Ƶλ�ã�021 18��00
    }
    // ��Ϸʧ��
    public void FailGame()
    {
        failPanel.GetComponent<CanvasGroup>().alpha = 1;
    }
    
    // �������
    
}
