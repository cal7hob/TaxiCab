using UnityEngine;
using System.Collections;

namespace TinHead_Developer
{
    public class Preferences
    {

        static Preferences instance = null;
        private int _level = 1;
        private int _coins = 0;
        private int _rateUs = 1;
        private int _ads = 0;
        private int _isRate = 0;
        private bool _FirstTime =true;
        public static Preferences Instance
        {
            get
            {
                if (instance == null)
                    instance = new Preferences();
                return instance;
            }
        }

        private Preferences()
        {
            Load();
            
        }

        void Load()
        {
            _isRate = PlayerPrefs.GetInt("israte", 0);
            _level = PlayerPrefs.GetInt("level", 1);
            _coins = PlayerPrefs.GetInt("coins", 0);
            _rateUs = PlayerPrefs.GetInt("rateUs", 1);
            _ads = PlayerPrefs.GetInt("ads", 0);
            _FirstTime = PlayerPrefsX.GetBool("FirstTime",true);
        }

        void Save()
        {
            PlayerPrefs.SetInt("israte", _isRate);
            PlayerPrefs.SetInt("level", _level);
            PlayerPrefs.SetInt("coins", _coins);
            PlayerPrefs.SetInt("rateUs", _rateUs);
            PlayerPrefs.SetInt("ads", _ads);
            PlayerPrefsX.SetBool("FirstTime", _FirstTime);
            PlayerPrefs.Save();
        }

        public void Reset()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Load();
        }
        public bool firsttime
        {
            get
            {
                return _FirstTime;
            }
            set
            {
                _FirstTime = value; Save();
            }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; Save(); }
        }

        public int Coins
        {
            get { return _coins; }
            set { _coins = value; Save(); }
        }

        public int RateUs
        {
            get { return _rateUs; }
            set { _rateUs = value; Save(); }
        }

        public int isRate
        {
            get { return _isRate; }
            set { _isRate = value; Save(); }
        }

        public int RemoveAds
        {
            get { return _ads; }
            set { _ads = value; Save(); }
        }
    }
}