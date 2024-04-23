using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public Configs configs;
    public List<DataConfig> dataConfigs => configs.dataConfigs;
    [SerializeField]
    private int _currentIndex;
    public int currentIndex
    {
        get { return _currentIndex; }
        set
        {
            _currentIndex = value % dataConfigs.Count;
            _currentIndex = _currentIndex < 0 ? dataConfigs.Count - 1 : _currentIndex;
        }
    }
    public DataConfig currentData => dataConfigs[_currentIndex];
    public UnityEvent onRefresh;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        configs.Sort();
        currentIndex = 0;
        onRefresh?.Invoke();
    }
    public void Next()
    {
        currentIndex++;
        onRefresh?.Invoke();
    }
    public void Prev()
    {
        currentIndex--;
        onRefresh?.Invoke();
    }
}
