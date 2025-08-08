using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePanel : Singleton<GamePanel>
{
    public Slider hpSlider;
    public Slider expSlider;
    public TMP_Text moneyCount;       // ���
    public TMP_Text expCount;         // �ȼ� LV.0
    public TMP_Text hpCount;          // ����ֵ 10/15
    public TMP_Text countDown;        // �ؿ�����ʱ 15
    public TMP_Text waveCount;        // ��ǰ���� 15

    protected override void Awake()
    {
        hpSlider = GameObject.Find("HpSlider").GetComponent<Slider>();
        expSlider = GameObject.Find("ExpSlider").GetComponent<Slider>();
        moneyCount = GameObject.Find("MoneyCount").GetComponent<TMP_Text>();
        expCount = GameObject.Find("ExpCount").GetComponent<TMP_Text>();
        hpCount = GameObject.Find("HpCount").GetComponent<TMP_Text>();
        countDown = GameObject.Find("CountDown").GetComponent<TMP_Text>();
        waveCount = GameObject.Find("WaveCount").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        // ���¾���ֵ
        RenewExp();  // TEST ����ֵ
        
        // ��������ֵ
        RenewHp();
        
        // ���½��
        RenewMoney();
        
        // ���²�����Ϣ
        RenewWaveCount();
    }

    /// <summary>
    /// ���¾���ֵ UI
    /// </summary>
    public void RenewExp()   // TEST ����ֵ
    {
        // 25, 12 2�� ,1   1/12 = 0.1
        expSlider.value = Player.Instance.exp % 12 / 12;
        expCount.text = "LV." + (int)(Player.Instance.exp / 12);
    }

    /// <summary>
    /// ��������ֵ UI
    /// </summary>
    public void RenewHp()
    {
        hpCount.text = Player.Instance.hp + "/" + Player.Instance.maxHp;
        hpSlider.value = Player.Instance.hp / Player.Instance.maxHp;
    }
    
    /// <summary>
    /// ���½�� UI
    /// </summary>
    public void RenewMoney()
    {
        moneyCount.text = Player.Instance.money.ToString();
    }

    /// <summary>
    /// ���µ���ʱ
    /// </summary>
    /// <param name="time"></param>
    public void RenewCountDown(float time)
    {
        countDown.text = time.ToString("F0");
    }

    /// <summary>
    /// ���²���
    /// </summary>
    public void RenewWaveCount()
    {
        waveCount.text = "��" + GameManager.Instance.currentWave.ToString() + "��";
    }
}
