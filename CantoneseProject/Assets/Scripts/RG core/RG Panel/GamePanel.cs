using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePanel : Singleton<GamePanel>
{
    public Slider hpSlider;
    public Slider expSlider;
    public TMP_Text moneyCount;       // 金币
    public TMP_Text expCount;         // 等级 LV.0
    public TMP_Text hpCount;          // 生命值 10/15
    public TMP_Text countDown;        // 关卡倒计时 15
    public TMP_Text waveCount;        // 当前波次 15

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
        // 更新经验值
        RenewExp();  // TEST 经验值
        
        // 更新生命值
        RenewHp();
        
        // 更新金币
        RenewMoney();
    }

    /// <summary>
    /// 更新经验值 UI
    /// </summary>
    public void RenewExp()   // TEST 经验值
    {
        // 25, 12 2级 ,1   1/12 = 0.1
        expSlider.value = Player.Instance.exp % 12 / 12;
        expCount.text = "LV." + (int)(Player.Instance.exp / 12);
    }

    /// <summary>
    /// 更新生命值 UI
    /// </summary>
    public void RenewHp()
    {
        hpCount.text = Player.Instance.hp + "/" + Player.Instance.maxHp;
        hpSlider.value = Player.Instance.hp / Player.Instance.maxHp;
    }
    
    /// <summary>
    /// 更新金币 UI
    /// </summary>
    public void RenewMoney()
    {
        moneyCount.text = Player.Instance.money.ToString();
    }
}
