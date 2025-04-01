using System;
using UnityEngine;
using UI;
using ConstValues;

namespace PlayerSystem
{
    public class UpgradePlayer
    {
        private static UpgradePlayer _instance;
        private int _startMoney = 1500;
        private float _multyplieSpeed = 0.1f;
        private int _startCountBag = 5;

        public static UpgradePlayer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UpgradePlayer();
                }

                return _instance;
            }
        }

        private UpgradePlayer()
        {
            LevelSpeed = 1f;
            LevelMoney = 1;
            Money = PlayerPrefs.GetInt(StringConstValues.Money, _startMoney);
            StatisticMoney = PlayerPrefs.GetInt(StringConstValues.StatisticMoney);
            Score = PlayerPrefs.GetInt(StringConstValues.Score);
            StatisticScore = PlayerPrefs.GetInt(StringConstValues.StatisticScore);
            UpgradeCountLevel = PlayerPrefs.GetInt(nameof(UpgradeCountLevel));
            UpgradeSpeedLevel = PlayerPrefs.GetInt(nameof(UpgradeSpeedLevel));
            UpgradeMoneyLevel = PlayerPrefs.GetInt(nameof(UpgradeMoneyLevel));
            LevelSpeed += UpgradeSpeedLevel * _multyplieSpeed;
            LevelMoney += UpgradeMoneyLevel;
        }

        public event Action Upgraded;
        public event Action<int> ChangedMoney;
        public event Action<int> ChangedScore;

        public int Money { get; private set; }
        public int UpgradeCountLevel { get; private set; }
        public int UpgradeSpeedLevel { get; private set; }
        public int UpgradeMoneyLevel { get; private set; }
        public int MaxCount => _startCountBag + UpgradeCountLevel;
        public float LevelSpeed { get; private set; }
        public int LevelMoney { get; private set; }
        public int StatisticMoney { get; set; }
        public int StatisticScore { get; set; }
        public int Score { get; private set; }
        public bool IsPay { get; private set; }

        public void ApplayUpgrade(Upgrade upgrade, int cost)
        {
            switch (upgrade)
            {
                case Upgrade.Count:
                    UpgradeCount(cost);
                    break;

                case Upgrade.Speed:
                    UpgradeSpeed(cost);
                    break;
                case Upgrade.Cost:
                    UpgradeMoney(cost);
                    break;
            }
        }

        public void ChangeMoney(int moneyDelta)
        {
            Money += moneyDelta;
            PlayerPrefs.SetInt(StringConstValues.Money, Money);
            ChangedMoney?.Invoke(Money);

            if (moneyDelta > 0)
            {
                StatisticMoney += moneyDelta;
                PlayerPrefs.SetInt(StringConstValues.StatisticMoney, StatisticMoney);
            }
        }

        public bool CheckMoney(int cost)
        {
            return Money >= cost;
        }

        public void AddScore()
        {
            Score++;
            StatisticScore++;
            PlayerPrefs.SetInt(StringConstValues.Score, Score);
            PlayerPrefs.SetInt(StringConstValues.StatisticScore, StatisticScore);
            ChangedScore?.Invoke(Score);
        }

        public void Refresh()
        {
            ChangedScore?.Invoke(Score);
            ChangedMoney?.Invoke(Money);
        }

        private void UpgradeCount(int cost)
        {
            if (Money >= cost)
            {
                UpgradeCountLevel++;
                Upgraded?.Invoke();
                ChangeMoney(-cost);
                IsPay = true;
            }
            else
            {
                IsPay = false;
            }

            PlayerPrefs.SetInt(nameof(UpgradeCountLevel), UpgradeCountLevel);
        }

        private void UpgradeSpeed(int cost)
        {
            if (Money >= cost)
            {
                UpgradeSpeedLevel++;
                LevelSpeed += _multyplieSpeed;
                ChangeMoney(-cost);
                IsPay = true;
            }
            else
            {
                IsPay = false;
            }

            PlayerPrefs.SetInt(nameof(UpgradeSpeedLevel), UpgradeSpeedLevel);
        }

        private void UpgradeMoney(int cost)
        {
            if (Money >= cost)
            {
                UpgradeMoneyLevel++;
                LevelMoney++;
                ChangeMoney(-cost);
                IsPay = true;
            }
            else
            {
                IsPay = false;
            }

            PlayerPrefs.SetInt(nameof(UpgradeMoneyLevel), UpgradeMoneyLevel);
        }
    }
}