using System;
using Scripts.HealthSystem;
using Scripts.LevelSystem;
using UnityEngine;

using Scripts.Player;
using Scripts.Player.Stats;

namespace Scripts.Player.Manager
{
    
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    
    public Player player;
    public PlayerStats playerStats;
    public WeaponSlotManager weaponSlotManager;
    public GameObject damagePopupPrefab;

    private LevelSystem levelSystem;
    private LevelSystemAnimated levelSystemAnimated;
    private HealthSystem healthSystem;
    
    public LevelWindow levelWindow;
    public HealthBar healthWindow;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        player = GetComponent<Player>();
        playerStats = GetComponentInChildren<PlayerStats>();
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        levelWindow = GameObject.Find("LevelWindow").GetComponent<LevelWindow>();
        healthWindow = GameObject.Find("HealthWindow").GetComponent<HealthBar>();
    }

    private void Start()
    {
        levelSystem = new LevelSystem();
        levelWindow.SetLevelSystem(levelSystem);
        
        levelSystemAnimated = new LevelSystemAnimated(levelSystem);
        levelWindow.SetLevelSystemAnimated(levelSystemAnimated);

        healthSystem = new HealthSystem(100);
        healthWindow.Setup(healthSystem);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            levelSystem.AddExperience(100);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            healthSystem.Damage(10);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            healthSystem.Heal(15);
        }
        
        levelSystemAnimated.Update();
    }
}

