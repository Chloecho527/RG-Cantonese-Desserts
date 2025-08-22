using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : Singleton<WaveManager>
{
	[System.Serializable]
	private class SpawnEntry { public string name; public int count; }                                // 单个刷怪条目
	private class SubWave { public float delayAfterPrev; public List<SpawnEntry> entries = new(); }   // 频次 （子波次）
	private class Wave { public bool isBoss; public List<SubWave> subs = new(); }                     // 波次（共5波）

	[Header("地图边界")]
	[SerializeField] private float mapMinX = -10f;  // 地图左边界  TODO 后续修改
	[SerializeField] private float mapMaxX = 10f;   // 地图右边界
	[SerializeField] private float mapMinY = -5f;   // 地图下边界
	[SerializeField] private float mapMaxY = 5f;    // 地图上边界
	
	[Header("canvas和组件")]
	[SerializeField] private GameObject successPanel;	// 胜利面板
	[SerializeField] private GameObject shopPanel;		// 商店面板
	[SerializeField] private GameObject failPanel;		// 失败面板
	[SerializeField] private Button continueButton;	// 商店面板按钮，点击继续下一波

	private readonly List<Wave> waves = new();   // readonly表示列表初始化后不能再指向新的列表对象，但可以修改列表内容（添加 / 移除元素）。
	
	[Header("关卡波次状态")]
	[SerializeField]private int waveIndex = 0;        // 波次索引    0-based: 0..4
	[SerializeField]private int subIndex = 0;         // 子波次索引
	[SerializeField]private int aliveCount = 0;       // 怪物当前存活数量，用于判断次波次是否结束
	[SerializeField]private int pendingToSpawn = 0;   // 记录当前波次中尚未生成的敌人数量，用于控制生成节奏
	
	[Header("游戏进行状态")]
	[SerializeField]private bool bossAlive = false;
	[SerializeField]private bool bossSmallCleared = false;

	protected override void Awake()
	{
		base.Awake();
		EventHandler.EnemyDiedEvent += OnEnemyDied;

		// 获取canvas和组件
		if (successPanel == null) successPanel = GameObject.Find("SuccessPanel");
		if (failPanel == null) failPanel = GameObject.Find("FailPanel");
		if (shopPanel == null) shopPanel = GameObject.Find("ShopPanel");
		if (continueButton == null && shopPanel != null)
		{
			var btn = shopPanel.GetComponentsInChildren<Button>(true).FirstOrDefault(b => b.gameObject.name.Contains("Continue"));
			if (btn != null) continueButton = btn; // TODO ///////////修改////////////
		}
		
		// 绑定 button 事件
		if (continueButton != null)
		{
		    continueButton.onClick.AddListener(OnContinue);
		}
		
		// 初始，panel不显示
		successPanel.GetComponent<CanvasGroup>().alpha = 0;
		shopPanel.GetComponent<CanvasGroup>().alpha = 0;;
	}

	private void Start()
	{
		BuildWavesFromTable();
		StartWave(0);
	}

	private void OnDestroy()
	{
		EventHandler.EnemyDiedEvent -= OnEnemyDied;
	}
	
	// 波次配置
	private void BuildWavesFromTable()
	{
		// 1：四面八方一次性出完
		waves.Add(new Wave {
			subs = new List<SubWave> {
				new SubWave {
					delayAfterPrev = 0,
					entries = new List<SpawnEntry>{
						new SpawnEntry{ name="红豆怪", count=3},
						new SpawnEntry{ name="绿豆怪", count=2},
						new SpawnEntry{ name="西米潜行者", count=2},
						new SpawnEntry{ name="芝麻散兵", count=3},
					}
				}
			}
		});

		// 2：分两频次，间隔 3s
		waves.Add(new Wave {
			subs = new List<SubWave> {
				new SubWave {
					delayAfterPrev = 0,
					entries = new List<SpawnEntry>{
						new SpawnEntry{ name="红豆怪", count=4},
						new SpawnEntry{ name="绿豆怪", count=4},
					}
				},
				new SubWave {
					delayAfterPrev = 3,
					entries = new List<SpawnEntry>{
						new SpawnEntry{ name="精英·糯米年糕妖", count=2},
						new SpawnEntry{ name="西米潜行者", count=2},
						new SpawnEntry{ name="芝麻散兵", count=2},
					}
				}
			}
		});

		// 3：平均分两波出完（数量按表均分）
		waves.Add(new Wave {
			subs = new List<SubWave> {
				new SubWave { delayAfterPrev = 0, entries = new List<SpawnEntry>{
					new SpawnEntry{ name="精英·薏米祭司", count=1},
					new SpawnEntry{ name="精英·糯米年糕妖", count=1},
					new SpawnEntry{ name="红豆怪", count=2},
					new SpawnEntry{ name="绿豆怪", count=2},
					new SpawnEntry{ name="西米潜行者", count=1},
					new SpawnEntry{ name="芝麻散兵", count=1},
				}},
				new SubWave { delayAfterPrev = 3, entries = new List<SpawnEntry>{
					new SpawnEntry{ name="精英·薏米祭司", count=1},
					new SpawnEntry{ name="精英·糯米年糕妖", count=1},
					new SpawnEntry{ name="红豆怪", count=2},
					new SpawnEntry{ name="绿豆怪", count=2},
					new SpawnEntry{ name="西米潜行者", count=1},
					new SpawnEntry{ name="芝麻散兵", count=1},
				}},
			}
		});

		// 4：平均分两波出完
		waves.Add(new Wave {
			subs = new List<SubWave> {
				new SubWave { delayAfterPrev = 0, entries = new List<SpawnEntry>{
					new SpawnEntry{ name="精英·薏米祭司", count=2},
					new SpawnEntry{ name="精英·糯米年糕妖", count=2},
					new SpawnEntry{ name="红豆怪", count=2},
					new SpawnEntry{ name="绿豆怪", count=2},
					new SpawnEntry{ name="西米潜行者", count=2},
					new SpawnEntry{ name="芝麻散兵", count=2},
				}},
				new SubWave { delayAfterPrev = 3, entries = new List<SpawnEntry>{
					new SpawnEntry{ name="精英·薏米祭司", count=2},
					new SpawnEntry{ name="精英·糯米年糕妖", count=2},
					new SpawnEntry{ name="红豆怪", count=2},
					new SpawnEntry{ name="绿豆怪", count=2},
					new SpawnEntry{ name="西米潜行者", count=2},
					new SpawnEntry{ name="芝麻散兵", count=2},
				}},
			}
		});

		// 5：Boss 波
		waves.Add(new Wave {
			isBoss = true,
			subs = new List<SubWave> {
				new SubWave { delayAfterPrev = 0, entries = new List<SpawnEntry>{
					new SpawnEntry{ name="芸豆巨像", count=1},
				}},
				new SubWave { delayAfterPrev = 5, entries = new List<SpawnEntry>{
					new SpawnEntry{ name="红豆怪", count=2},
					new SpawnEntry{ name="绿豆怪", count=2},
					new SpawnEntry{ name="西米潜行者", count=2},
					new SpawnEntry{ name="芝麻散兵", count=2},
				}},
				// 击败小怪的7s后
				new SubWave { delayAfterPrev = 0, entries = new List<SpawnEntry>{
					new SpawnEntry{ name="精英·薏米祭司", count=2},
					new SpawnEntry{ name="精英·糯米年糕妖", count=2},
				}},
			}
		});
	}

	// 开始第 x 波
	private void StartWave(int index)
	{
		waveIndex = index;
		subIndex = 0;
		aliveCount = 0;
		pendingToSpawn = 0;
		bossAlive = false;
		bossSmallCleared = false;

		GameManager.Instance.currentWave = index + 1;
		GamePanel.Instance.RenewWaveCount();

		StartCoroutine(RunWaveCoroutine());
	}

	private IEnumerator RunWaveCoroutine()
	{
		var wave = waves[waveIndex];

		for (int i = 0; i < wave.subs.Count; i++)
		{
			subIndex = i;
			var sub = wave.subs[i];

			// Boss 特殊第三子波：等待“第二子波小怪清光后7s”
			if (wave.isBoss && i == 2)
			{
				yield return new WaitUntil(() => bossSmallCleared);
				yield return new WaitForSeconds(7f);
			}
			else
			{
				if (i > 0)
				{
					// 若定义了 delay 用之；否则“清光后 +3s”
					if (sub.delayAfterPrev > 0)
						yield return new WaitForSeconds(sub.delayAfterPrev);
					else
						yield return new WaitUntil(() => aliveCount + pendingToSpawn == 0);
					yield return new WaitForSeconds(3f);
				}
				else if (sub.delayAfterPrev > 0) yield return new WaitForSeconds(sub.delayAfterPrev);
			}

			SpawnSubWave(sub, wave.isBoss);

			// Boss 补充召唤（示例）
			if (wave.isBoss && i == 0) StartCoroutine(BossSummonLoop());
		}

		// 等待整波清空 → 胜利 → 3s → 商店
		yield return new WaitUntil(() => aliveCount + pendingToSpawn == 0);
		yield return ShowSuccessThenShop();
	}

	/// <summary>
	/// 获取子波敌人信息
	/// </summary>
	/// <param name="sub"></param>
	/// <param name="isBossWave"></param>
	private void SpawnSubWave(SubWave sub, bool isBossWave)
	{
		int total = sub.entries.Sum(e => e.count);
		pendingToSpawn += total;

		foreach (var e in sub.entries)
		{
			var data = GameManager.Instance.GetEnemyByName(e.name);
			if (data == null)
			{
				Debug.LogWarning($"未找到敌人数据: {e.name}");
				pendingToSpawn -= e.count; continue;
			}

			var prefab = Resources.Load<GameObject>(data.prefabPath);
			if (prefab == null) 
			{
				Debug.LogWarning($"未找到预制体: {data.prefabPath}");
				pendingToSpawn -= e.count; continue;
			}

			StartCoroutine(SpawnBatch(prefab, e.count, isBossWave && e.name.Contains("芸豆巨像")));
		}
	}

	private IEnumerator SpawnBatch(GameObject prefab, int count, bool markBoss)
	{
		for (int i = 0; i < count; i++)
		{
			Vector3 pos = GetSpawnPosAroundPlayer();
			var go = Instantiate(prefab, pos, Quaternion.identity);
			if (markBoss) bossAlive = true;
			aliveCount++;
			pendingToSpawn--;
			yield return null;
		}
	}

	/// <summary>
	/// 获取敌人生成位置
	/// </summary>
	/// <returns></returns>
	private Vector3 GetSpawnPosAroundPlayer()
	{
		// var p = Player.Instance.transform.position;
		// // 四面八方：在玩家周围环形随机
		// float r = Random.Range(8f, 14f);
		// float a = Random.Range(0f, Mathf.PI * 2f);
		// return new Vector3(p.x + Mathf.Cos(a) * r, p.y + Mathf.Sin(a) * r, 0);
		
		var playerPos = Player.Instance.transform.position;
    
		// 计算原始生成位置（围绕玩家的环形随机点）
		float radius = Random.Range(8f, 14f);  // 生成半径范围
		float angle = Random.Range(0f, Mathf.PI * 2f);  // 随机角度
		float rawX = playerPos.x + Mathf.Cos(angle) * radius;
		float rawY = playerPos.y + Mathf.Sin(angle) * radius;

		// 核心：将生成位置限制在地图边界内
		float clampedX = Mathf.Clamp(rawX, mapMinX, mapMaxX);  // X坐标不超出左右边界
		float clampedY = Mathf.Clamp(rawY, mapMinY, mapMaxY);  // Y坐标不超出上下边界

		// 返回约束后的位置
		return new Vector3(clampedX, clampedY, 0);
	}

	private IEnumerator BossSummonLoop()
	{
		// 召唤补充：红豆怪×3/波（示例：每10秒补充，直到Boss死亡）
		while (bossAlive)
		{
			yield return new WaitForSeconds(10f);
			if (!bossAlive) break;
			var data = GameManager.Instance.GetEnemyByName("红豆怪");
			if (data == null) continue;
			var prefab = Resources.Load<GameObject>(data.prefabPath);
			if (prefab == null) continue;
			for (int i = 0; i < 3; i++)
			{
				Instantiate(prefab, GetSpawnPosAroundPlayer(), Quaternion.identity);
				aliveCount++;
			}
		}
	}

	private void OnEnemyDied(EnemyBase e)
	{
		aliveCount = Mathf.Max(0, aliveCount - 1);

		// Boss 波第二子波（小怪）清空判定：bossAlive=true 且 仅剩Boss
		if (waves[waveIndex].isBoss)
		{
			if (bossAlive && !bossSmallCleared)
			{
				// 简化：若 aliveCount==1 近似只剩Boss
				if (aliveCount == 1) bossSmallCleared = true;
			}

			// Boss 死亡
			if (e.name.Contains("芸豆") || e.gameObject.name.Contains("芸豆"))
				bossAlive = false;
		}
	}

	private IEnumerator ShowSuccessThenShop()
	{
		if (successPanel != null)
		{
			successPanel.GetComponent<CanvasGroup>().alpha = 1;
			yield return new WaitForSeconds(3f);
		}
		if (shopPanel != null)
		{
			successPanel.GetComponent<CanvasGroup>().alpha = 0;
			shopPanel.GetComponent<CanvasGroup>().alpha = 1;
		}
	}

	private void OnContinue()
	{
		if (shopPanel != null) shopPanel.gameObject.SetActive(false);

		if (waveIndex + 1 < waves.Count)
		{
			StartWave(waveIndex + 1);
		}
		else
		{
			// 全部5波完成，可在此进入结算或返回主页
			Debug.Log("全地图5波完成！");
		}
	}

	// 玩家死亡
	public void FailGame()
	{
		if (failPanel != null)
		{
			failPanel.GetComponent<CanvasGroup>().alpha = 1;
		}
	}
}
