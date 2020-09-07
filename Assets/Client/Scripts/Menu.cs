using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour//Управляет всеми компонентами меню
{
    public Animator menu;
    public Animator startGame;
    public GameObject moneyGame;
    AdsManager adsManager;
    public Cut cut;
    public Data data;
    public Privacy p;
    public Lines lines;
    public Market market;
    public Music msc;
    public Teach tch;
    public GameObject teach;
    public GameObject graphsAsset;
    public GameObject levelHunting;
    public GameObject lockgame;
    public GameObject pauseBut;
    public GameObject textScore;
    public Text maxTextScore;
    public Text moneyScore;
    public Text moneyScore2;
    public Text moneyScore3;
    public Text levelNow;
    public Text levelFuture;
    public Text version;
    public Image progressBar;
    public Image sliderSoundBackGround;
    public Image sliderMusicBackGround;
    public Image notifications;
    public GameObject pause;
    public GameObject adsReward;
    public GameObject prelose;
    public GameObject youlose;
    public GameObject menuUI;
    public GameObject author1;
    public GameObject author2;
    public GameObject author3;
    public GameObject hand;
    public GameObject left;
    public GameObject right;
    public Sprite audioOn;
    public Sprite audioOff;
    public Sprite musicOn;
    public Sprite musicOff;
    public Sprite notificationsOn;
    public Sprite notificationsOff;
    private bool lockIsGame = false;
    public bool forTeach { get; set; }
    private bool isStartOffRenderGameUI = true;
    public int count { get; private set; }
    public int countMoney { get; private set; }
    public int addReward { get; private set; }
    public int addRestart { get; private set; }
    [Header("Anim")]
    public Image timer;
    public Text levelNow;
    public Text levelFuture;
    public Image[] levelhunt;
    [Header("ColorChange")]
    public Material graph;
    public Material circleBackGroundAction;
    public Material error;
    public GameObject[] buts;
    public Image[] images;
    public Image[] imagesUni;
    public Text[] texts;
    [Header("Sound")]
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioClip changeSound;
    public AudioClip errorEndGame;
    public AudioClip cutSound;
    public AudioClip coinCollectSound;
    public AudioClip butActive;
    public AudioClip buy;
    public AudioClip newLevel;
    public AudioClip prlose;
    [Header("Settings")]
    public Slider sliderSound;
    public Image sound;
    float lastSound;
    bool actSliderSound = true;
    public Slider sliderMusic;
    public Image music;
    float lastMusic;
    bool actSliderMusic = true;

    private void Awake()
    {
        adsManager = GetComponent<AdsManager>();
    }

    public void Start()
    {
        Time.timeScale = 1f;
        if (!PlayerPrefs.HasKey("version"))
        {
            if (!PlayerPrefs.HasKey("MaxScore"))
            {
                PlayerPrefs.SetInt("MaxScore", 0);
                PlayerPrefs.SetInt("Money", 0);
                PlayerPrefs.SetInt("version", 2);
                PlayerPrefs.SetInt("Experience", 0);
                PlayerPrefs.SetInt("Level", 0);
                PlayerPrefs.SetFloat("Sound", 0.5f);
                PlayerPrefs.SetFloat("Music", 0f);
                PlayerPrefs.SetString("Teach", "true");

                PlayerPrefs.SetString("Hand", "right");
                PlayerPrefs.SetString("Notifications", "on");
                PlayerPrefs.SetString("FirstGame", "");
                PlayerPrefs.SetFloat("MiddleScoreInGame", 0f);
                PlayerPrefs.SetInt("MaxCutSimpleScore", 0);
                PlayerPrefs.SetInt("MaxCutSimpleMoney", 0);
                PlayerPrefs.SetInt("MaxCutGameMoney", 0);
                PlayerPrefs.SetInt("RemoveMoney", 0);
                PlayerPrefs.SetInt("AllMoney", 0);
                PlayerPrefs.SetInt("AllGames", 0);
                PlayerPrefs.SetInt("AdsReward", 3);
                PlayerPrefs.SetInt("AdsRestart", 3);
                PlayerPrefs.SetInt("Day", DateTime.Now.DayOfYear);
            }
            else
            {
                int maxScore = p.EnCrypt(PlayerPrefs.GetString("MaxScore"));
                int money = p.EnCrypt(PlayerPrefs.GetString("Money"));
                PlayerPrefs.DeleteKey("MaxScore");
                PlayerPrefs.DeleteKey("Money");
                p.DeletePassword();
                PlayerPrefs.SetInt("MaxScore", maxScore);
                PlayerPrefs.SetInt("Money", money);
                PlayerPrefs.SetInt("version", 2);
                PlayerPrefs.SetInt("Experience", 0);
                PlayerPrefs.SetInt("Level", 0);
                PlayerPrefs.SetFloat("Sound", 0.5f);
                PlayerPrefs.SetFloat("Music", 0f);
                PlayerPrefs.SetString("Teach", "false");

                PlayerPrefs.SetString("Hand", "right");
                PlayerPrefs.SetString("Notifications", "on");
                PlayerPrefs.SetString("FirstGame", "false");
                PlayerPrefs.SetFloat("MiddleScoreInGame", 0f);
                PlayerPrefs.SetInt("MaxCutSimpleScore", 0);
                PlayerPrefs.SetInt("MaxCutSimpleMoney", 0);
                PlayerPrefs.SetInt("MaxCutGameMoney", 0);
                PlayerPrefs.SetInt("RemoveMoney", 0);
                PlayerPrefs.SetInt("AllMoney", 0);
                PlayerPrefs.SetInt("AllGames", 0);
                PlayerPrefs.SetInt("AdsReward", 3);
                PlayerPrefs.SetInt("AdsRestart", 3);
                PlayerPrefs.SetInt("Day", DateTime.Now.DayOfYear);
            }
        }
        else
        {
            switch (PlayerPrefs.GetInt("version"))
            {
                case 1:
                    PlayerPrefs.SetInt("version", 2);
                    PlayerPrefs.SetString("Hand", "right");
                    PlayerPrefs.SetString("Notifications", "on");
                    PlayerPrefs.SetString("FirstGame", "false");
                    PlayerPrefs.SetFloat("MiddleScoreInGame", 0f);
                    PlayerPrefs.SetInt("MaxCutSimpleScore", 0);
                    PlayerPrefs.SetInt("MaxCutSimpleMoney", 0);
                    PlayerPrefs.SetInt("MaxCutGameMoney", 0);
                    PlayerPrefs.SetInt("RemoveMoney", 0);
                    PlayerPrefs.SetInt("AllMoney", 0);
                    PlayerPrefs.SetInt("AllGames", 0);
                    PlayerPrefs.SetInt("AdsReward", 3);
                    PlayerPrefs.SetInt("AdsRestart", 3);
                    PlayerPrefs.SetInt("Day", DateTime.Now.DayOfYear);
                    break;
            }
        }
        forTeach = PlayerPrefs.GetString("Teach") == "true";
        PlayerPrefs.SetString("FirstGame", forTeach.ToString().ToLower());
        teach.SetActive(forTeach);
        if (forTeach)
        {
            tch.StartTeach();
        }

        UpdateLevelHunting();
        cut.isActiveActivity = false;
        count = 0; countMoney = PlayerPrefs.GetInt("Money");
        textScore.GetComponent<Text>().text = count.ToString();
        if (PlayerPrefs.GetInt("Day") != DateTime.Now.DayOfYear)
        {
            PlayerPrefs.SetInt("Day", DateTime.Now.DayOfYear);
            PlayerPrefs.SetInt("AdsReward", 3);
            PlayerPrefs.SetInt("AdsRestart", 3);
        }
        addReward = PlayerPrefs.GetInt("AdsReward");
        addRestart = PlayerPrefs.GetInt("AdsRestart");
        maxTextScore.text = PlayerPrefs.GetInt("MaxScore").ToString();
        moneyScore.text = countMoney.ToString();
        moneyScore2.text = countMoney.ToString();
        moneyScore3.text = countMoney.ToString();
        version.text = "v: " + Application.version;
        float exp = PlayerPrefs.GetInt("Experience");
        float alg = PlayerPrefs.GetInt("AllGames");
        if (alg == 0) { PlayerPrefs.SetFloat("MiddleScoreInGame", 0); }
        else { PlayerPrefs.SetFloat("MiddleScoreInGame", exp / alg); }
        lockIsGame = false;
        cut.isTeaching = forTeach;
        cut.isGame = forTeach;
        menuUI.SetActive(!forTeach);
        lines.speedGraph = 75f;
        levelHunting.SetActive(!forTeach && PlayerPrefs.GetString("FirstGame") == "false");
        youlose.SetActive(false);
        prelose.SetActive(false);
        pauseBut.SetActive(false);
        moneyGame.SetActive(false);
        textScore.SetActive(false);
        pause.SetActive(false);
        author1.SetActive(true);
        author2.SetActive(false);
        author3.SetActive(false);

        bool ad = PlayerPrefs.GetInt("AdsReward") > 0 && adsManager.CheckMonetizationReward();
        adsReward.SetActive(ad);
        UpdateNotifications();
        UpdateHand();
        if (isStartOffRenderGameUI == true)
        {
            isStartOffRenderGameUI = false;
            StartCoroutine(OffRenderGameUI());
        }

        if (PlayerPrefs.GetFloat("Sound") == 0f)
        {
            sound.sprite = audioOff;
        }
        else
        {
            sound.sprite = audioOn;
        }
        sliderSound.value = PlayerPrefs.GetFloat("Sound");
        SoundActive(PlayerPrefs.GetFloat("Sound"));

        if (PlayerPrefs.GetFloat("Music") == 0f)
        {
            music.sprite = musicOff;
        }
        else
        {
            music.sprite = musicOn;
        }
        sliderMusic.value = PlayerPrefs.GetFloat("Music");
        MusicActive(PlayerPrefs.GetFloat("Music"));
        
        if (PlayerPrefs.GetString("FirstGame") == "false")
        {
            StartCoroutine(StartGameNext(0.5f));
        }
        else
        {
            startGame.SetBool("isActive", true);
            StartCoroutine(StartGameNext(0.3f));
            Restart();
        }
        
        levelHunt.SetBool("isActive", true);
        return;
    }

    public static string clipboard//Буфер обмена
    {
        get { return GUIUtility.systemCopyBuffer; }
        set { GUIUtility.systemCopyBuffer = value; }
    }
    private string urlPM = "https://play.google.com/store/apps/details?id=com.LiMiDyFy.GraphCut";

    public void PlayerPrefsStartGenerate2()//Сохранения в магазине
    {
        for (int i = 0; i < market.locks.Length; i++)
        {
            PlayerPrefs.SetString("Lock" + i, "locked");
        }
        return;
    }

    public IEnumerator StartGameNext(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        startGame.SetBool("isActive", false);
    }

    public void Restart()//Рестарт
    {
        bool bActGame = PlayerPrefs.GetString("FirstGame") == "false";
        levelHunt.SetBool("isActive", false);
        pauseBut.SetActive(bActGame);
        moneyGame.SetActive(bActGame);
        textScore.SetActive(bActGame);
        lines.speedGraph = 75f;
        cut.isGame = true;
        inGame = true;
        menuUI.SetActive(false);
        cut.isActiveActivity = true;
        EnableOffRenderGameUI();
        GameObject.FindWithTag("Graphs").GetComponent<Lines>().StartCoin();
        return;
    }

    private bool isBack = true;
    public void Update()//Работа кнопки назад (Android)
    {
        if (Input.GetKey(KeyCode.Escape) && isBack == true)
        {
            isBack = false;
            StartCoroutine(IsBackRetarted());
            if (cut.isGame == false)
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
            else
            {
                if (lockIsGame == false)
                {
                    PauseButtons(1);
                }
                else
                {
                    PauseButtons(3);
                }
            }
            return;
        }
    }

    public IEnumerator IsBackRetarted()//Включить функцию назад (Android)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        isBack = true;
    }

    public void MusicActive(float sen)//Реализация громкости музыки
    {
        sen = sen * 0.25f;
        msc.musicSource1.volume = sen;
        msc.musicSource2.volume = sen;
        msc.musicSource3.volume = sen;
        msc.musicSourceBackground1.volume = sen;
        msc.musicSourceBackground2.volume = sen;
        msc.musicSourceBackground3.volume = sen;
        return;
    }

    public void SoundActive(float sen)//Реализация громкости звуков
    {
        audioSource.volume = sen;
        audioSource2.volume = sen;
        audioSource3.volume = sen;
        return;
    }

    public void PlaySound(int id)//Все базовые игровые звуки
    {
        switch (id)
        {
            case 1:
                audioSource.clip = changeSound;
                audioSource.Play();
                break;
            case 2:
                audioSource.clip = errorEndGame;
                audioSource.Play();
                break;
            case 3:
                audioSource.clip = cutSound;
                audioSource.Play();
                break;
            case 4:
                audioSource2.clip = coinCollectSound;
                audioSource2.Play();
                break;
            case 5:
                audioSource3.clip = buy;
                audioSource3.Play();
                break;
            case 6:
                audioSource2.clip = butActive;
                audioSource2.Play();
                break;
            case 7:
                audioSource3.clip = newLevel;
                audioSource3.Play();
                break;
            case 8:
                audioSource3.clip = prlose;
                audioSource3.Play();
                break;
        }
    }

    private Color c;
    public void ColorUpdate(ColorsGame color)//Обновление цветов в меню
    {
        c = color.colorBackGround;
        Color altColor = new Color(1f - c.r, 1f - c.g, 1f - c.b, 0.1960784f);
        Color clb = color.colorLinesBackground;
        Color cii = color.colorInfoIcons;
        for (int i = 0; i < buts.Length; i++)
        {
            buts[i].GetComponent<Image>().color = clb;
            buts[i].transform.GetChild(0).GetComponent<Image>().color = c;
            buts[i].transform.GetChild(0).GetChild(0).GetComponent<Image>().color = cii;
        }
        pauseBut.GetComponent<Image>().color = new Color(clb.r, clb.g, clb.b, 0.4f);
        pauseBut.transform.GetChild(0).GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0.4f);
        pauseBut.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(cii.r, cii.g, cii.b, 0.4f);

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = clb;
        }
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = clb;
        }
        for (int i = 0; i < imagesUni.Length; i++)
        {
            imagesUni[i].color = color.colorBackGroundBackGround;
        }
        hand.GetComponent<Image>().color = clb;
        hand.transform.GetChild(0).GetComponent<Image>().color = c;
        left.transform.GetChild(0).GetComponent<Image>().color = clb;
        left.transform.GetChild(1).GetComponent<Image>().color = color.colorTouches;
        right.transform.GetChild(0).GetComponent<Image>().color = clb;
        right.transform.GetChild(1).GetComponent<Image>().color = color.colorTouches;

        progressBar.color = color.colorTouches;
        sliderSoundBackGround.color = color.colorTouches;
        sliderMusicBackGround.color = color.colorTouches;
        levelNow.color = color.colorBackGround;
        levelFuture.color = color.colorBackGround;
        moneyScore3.color = clb;
        circleBackGroundAction.color = altColor;
    }

    public void MenuButtons(int id)
    {
        switch (id)
        {
            case 0://Первый порез
                if (lockIsGame == false)
                {
                    if (cut.isGame == false && forTeach == false)
                    {
                        Restart();
                    }
                    cut.DownCut();
                }
                break;
            case 1://Кнопка открытия маркета
                PlaySound(6);
                lockIsGame = true;
                menu.SetBool("isMarket", true);
                levelHunt.SetBool("isMarket", true);
                break;
            case 2://Кнопка по центру (начало игры)
                if (!lockIsGame && !cut.isGame && !forTeach)
                {
                    Restart();
                }
                break;
            case 3://Кнопка открытия настроек
                PlaySound(6);
                lockIsGame = true;
                menu.SetBool("isSettings", true);
                levelHunt.SetBool("isSettings", true);
                break;
        }
    }

    public void MarketButtons(int id)
    {
        PlaySound(6);
        switch (id)
        {
            case 1://Кнопка "назад"
                lockIsGame = false;
                menu.SetBool("isMarket", false);
                levelHunt.SetBool("isMarket", false);
                StartCoroutine(SetCloseBuy());
                break;
        }
    }

    public IEnumerator SetCloseBuy()//Отключает select функции
    {
        yield return new WaitForSeconds(0.5f);
        market.Select(PlayerPrefs.GetInt("ColorSelectTheme"));
        market.buy.SetActive(false);
    }

    public void SettingsButtons(int id)
    {
        switch (id)
        {
            case 1://Кнопка "назад"
                PlaySound(6);
                lockIsGame = false;
                menu.SetBool("isSettings", false);
                levelHunt.SetBool("isSettings", false);
                StartCoroutine(SettingsClose());
                break;
            case 2://Кнопка слайдера звука
                actSliderSound = false;
                if (PlayerPrefs.GetFloat("Sound") == 0f)
                {
                    if (lastSound == 0f)
                    {
                        PlayerPrefs.SetFloat("Sound", 0.5f);
                        sliderSound.value = 0.5f;
                        SoundActive(0.5f);
                        MusicActive(PlayerPrefs.GetFloat("Music"));
                    }
                    else
                    {
                        PlayerPrefs.SetFloat("Sound", lastSound);
                        sliderSound.value = lastSound;
                        SoundActive(lastSound);
                        MusicActive(PlayerPrefs.GetFloat("Music"));
                    }
                    sound.sprite = audioOn;
                    PlaySound(1);
                }
                else
                {
                    PlayerPrefs.SetFloat("Sound", 0f);
                    sliderSound.value = 0f;
                    SoundActive(0f);
                    MusicActive(PlayerPrefs.GetFloat("Music"));
                    sound.sprite = audioOff;
                }
                actSliderSound = true;
                break;
            case 3://Кнопка слайдера музыки
                actSliderMusic = false;
                if (PlayerPrefs.GetFloat("Music") == 0f)
                {
                    if (lastMusic == 0f)
                    {
                        PlayerPrefs.SetFloat("Music", 0.5f);
                        sliderMusic.value = 0.5f;
                        SoundActive(PlayerPrefs.GetFloat("Sound"));
                        MusicActive(0.5f);
                    }
                    else
                    {
                        PlayerPrefs.SetFloat("Music", lastMusic);
                        sliderMusic.value = lastMusic;
                        SoundActive(PlayerPrefs.GetFloat("Sound"));
                        MusicActive(lastMusic);
                    }
                    music.sprite = musicOn;
                }
                else
                {
                    PlayerPrefs.SetFloat("Music", 0f);
                    sliderMusic.value = 0f;
                    SoundActive(PlayerPrefs.GetFloat("Music"));
                    MusicActive(0f);
                    music.sprite = musicOff;
                }
                actSliderMusic = true;
                break;
            case 4://Оценивание
                PlaySound(6);
                Application.OpenURL(urlPM);
                break;
            case 5://Кнопка author
                PlaySound(6);
                if (author1.activeSelf == true)
                {
                    author1.SetActive(false);
                    author2.SetActive(true);
                    author3.SetActive(false);
                }
                else if (author2.activeSelf == true)
                {
                    author1.SetActive(false);
                    author2.SetActive(false);
                    author3.SetActive(true);
                }
                else
                {
                    author1.SetActive(true);
                    author2.SetActive(false);
                    author3.SetActive(false);
                }
                break;
            case 6://Кнопка выбора руки
                PlaySound(6);
                if (PlayerPrefs.GetString("Hand") == "right")
                {
                    PlayerPrefs.SetString("Hand", "left");
                }
                else
                {
                    PlayerPrefs.SetString("Hand", "right");
                }
                UpdateHand();
                break;
            case 7://Кнопка уведомления
                if (PlayerPrefs.GetString("Notifications") == "on")
                {
                    PlayerPrefs.SetString("Notifications", "off");
                }
                else
                {
                    PlayerPrefs.SetString("Notifications", "on");
                }
                PlaySound(6);
                UpdateNotifications();
                break;
        }
    }

    public void UpdateHand()//Графика кнопки выбора руки
    {
        if (PlayerPrefs.GetString("Hand") == "right")
        {
            Vector2 pos = pauseBut.transform.position;
            left.SetActive(false);
            right.SetActive(true);
            pauseBut.transform.position = new Vector2(-Mathf.Abs(pos.x), pos.y);
        }
        else
        {
            Vector2 pos = pauseBut.transform.position;
            left.SetActive(true);
            right.SetActive(false);
            pauseBut.transform.position = new Vector2(Mathf.Abs(pos.x), pos.y);
        }
    }

    public void UpdateNotifications()//Графика кнопки уведомлений
    {
        if (PlayerPrefs.GetString("Notifications") == "on")
        {
            notifications.sprite = notificationsOn;
        }
        else
        {
            notifications.sprite = notificationsOff;
        }
    }

    public IEnumerator SettingsClose()//Возвращение кнопки author в стандартное положение
    {
        yield return new WaitForSeconds(0.5f);
        author1.SetActive(true);
        author2.SetActive(false);
        author3.SetActive(false);
    }

    public void SoundSlider()//Слайдер звука
    {
        if (actSliderSound == true)
        {
            PlayerPrefs.SetFloat("Sound", sliderSound.value);
            lastSound = sliderSound.value;
            if (sliderSound.value == 0f)
            {
                sound.sprite = audioOff;
            }
            else
            {
                sound.sprite = audioOn;
            }
            SoundActive(sliderSound.value);
        }
    }

    public void MusicSlider()//Слайдер музыки
    {
        if (actSliderMusic == true)
        {
            PlayerPrefs.SetFloat("Music", sliderMusic.value);
            lastMusic = sliderMusic.value;
            if (sliderMusic.value == 0f)
            {
                music.sprite = musicOff;
            }
            else
            {
                music.sprite = musicOn;
            }
            MusicActive(sliderMusic.value);
        }
    }

    public void PauseButtons(int id)
    {
        PlaySound(6);
        switch (id)
        {
            case 1://Пауза
                Time.timeScale = 0f;
                pause.SetActive(true);
                pauseBut.SetActive(false);
                lockIsGame = true;
                break;
            case 2://Продолжить
                Time.timeScale = 1f;
                lockIsGame = false;
                pause.SetActive(false);
                pauseBut.SetActive(true);
                EnableOffRenderGameUI();
                break;
            case 3://Меню
                startGame.SetBool("isActive", true);
                StartCoroutine(StartTimer());
                break;
            case 4://Перезапуск
                startGame.SetBool("isActive", true);
                StartCoroutine(RestartTimer());
                break;
            case 5://Рекламное воскрешение
                actContiniePreLose = false;
                prelose.SetActive(false);
                timer.fillAmount = 1f;
                YouLose();
                break;
            case 6://Если не воскреснуть
                actContiniePreLose = false;
                //add.CheckMonetizationRestart();
                break;
        }

    }

    public IEnumerator StartTimer()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Destroy(GameObject.FindWithTag("Graphs"));
        Instantiate(graphsAsset);
        Start();
        if (inGame == true && oneTimeGame == false && count < 10)
        {
            inGame = false;
            oneTimeGame = true;
            PlayerPrefs.SetInt("AllGames", PlayerPrefs.GetInt("AllGames") - 1);
        }
        singleAddRestart = true;
        NewRecord();
        DisableOffRenderGameUI();
        StartCoroutine(LocalMaxCutGameMoneyLow());
        startGame.SetBool("isActive", false);
    }

    public IEnumerator RestartTimer()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Destroy(GameObject.FindWithTag("Graphs"));
        Instantiate(graphsAsset);
        Start();
        Restart();
        if (inGame == true && oneTimeGame == false && count < 10)
        {
            inGame = false;
            oneTimeGame = true;
            PlayerPrefs.SetInt("AllGames", PlayerPrefs.GetInt("AllGames") - 1);
        }
        singleAddRestart = true;
        StartCoroutine(LocalMaxCutGameMoneyLow());
        startGame.SetBool("isActive", false);
    }

    private bool singleAddRestart = true;
    private bool oneTimeGame = true;
    private bool inGame = false;
    public void PreLose(MeshRenderer mr, Transform tr)//Если произошло рекламное воскрешение
    {
        StartCoroutine(GraphMaterialStandard(mr, tr));
        float ms = PlayerPrefs.GetFloat("MiddleScoreInGame") / 2f;
        if (ms < 25f) { ms = 25f; }
        
        if (oneTimeGame == true)
        {
            oneTimeGame = false;
            string fg = PlayerPrefs.GetString("FirstGame");
            string t = PlayerPrefs.GetString("Teach");
            if (fg == "false" && t == "false")
            {
                if (count >= ms && addRestart > 0 && singleAddRestart == true)
                {
                    singleAddRestart = false;
                    Time.timeScale = 0f;
                    msc.activity = 0f;
                    if (adsManager.CheckInternet())
                    {
                        actContiniePreLose = true;
                        prelose.SetActive(true);
                        StartCoroutine(AnimTimerPreLose(5f));
                        StartCoroutine(ContiniePreLose());
                    }
                    else
                    {
                        YouLose();
                    }
                }
                else
                {
                    YouLose();
                }
            }
            else if (PlayerPrefs.GetInt("Experience") >= 8)
            {
                startGame.SetBool("isActive", true);
                PlayerPrefs.SetString("FirstGame", "false");
                StartCoroutine(Falser());
            }
        }
    }

    private IEnumerator Falser()//Перезапуск игры после обучения
    {
        yield return new WaitForSecondsRealtime(0.2f);
        SceneManager.LoadScene(0);
    }
    
    //Исправление материала в графе на стандартный (рекламное воскрешение)
    private IEnumerator GraphMaterialStandard(MeshRenderer mr, Transform tr)
    {
        yield return new WaitForSeconds(0.05f);
        oneTimeGame = true;
        try
        {
            tr.localScale = new Vector3(1f, 1f, 1f);
            mr.material = graph;
        } catch { }
    }

    private bool actContiniePreLose = false;
    public IEnumerator ContiniePreLose()//Рекламное воскрешение
    {
        yield return new WaitForSecondsRealtime(5.1f);
        if (actContiniePreLose == true)
        {
            actContiniePreLose = false;
            prelose.SetActive(false);
            timer.fillAmount = 1f;
            YouLose();
        }
    }

    public void YouLose()//Проигрыш
    {
        PlaySound(2);
        inGame = false;
        oneTimeGame2 = true;
        youlose.SetActive(true);
        pauseBut.SetActive(false);
        pause.SetActive(false);
        NewRecord();
        StartCoroutine(LocalMaxCutGameMoneyLow());
    }

    public IEnumerator LocalMaxCutGameMoneyLow()//Обнуление переменной о порезанных монетах
    {
        yield return new WaitForSecondsRealtime(0.2f);
        localMaxCutGameMoney = 0;
    }

    public void NewRecord()//Новый рекорд
    {
        if (PlayerPrefs.GetInt("MaxScore") < count)
        {
            PlayerPrefs.SetInt("MaxScore", count);
            maxTextScore.GetComponent<Text>().text = count.ToString();
        }
        return;
    }

    private bool oneTimeGame2 = true;
    public void PlusCount(int plus)//Старт игры и первый порез
    {
        PlaySound(3);
        count = count + plus;
        cut.AddingActivity(count, plus);
        textScore.GetComponent<Text>().text = count.ToString();
        msc.activity += 0.5f;
        if (oneTimeGame2 == true)
        {
            oneTimeGame2 = false;
            PlayerPrefs.SetInt("AllGames", PlayerPrefs.GetInt("AllGames") + 1);
        }
        UpLevelHunting(plus);
    }

    private int localMoney = 0;
    private bool oneTimeMoney = true;
    private int localMaxCutGameMoney = 0;
    public void PlusMoney()//Получение монеты за порез
    {
        localMoney = localMoney + 1;
        if (oneTimeMoney == true)
        {
            oneTimeMoney = false;
            StartCoroutine(ActivePlusMoney());
        }
    }

    public IEnumerator ActivePlusMoney()//Общая запись о количестве полученных монет за порез
    {
        yield return new WaitForSecondsRealtime(0.1f);
        PlaySound(4);
        localMaxCutGameMoney = localMaxCutGameMoney + localMoney;
        if (PlayerPrefs.GetInt("MaxCutGameMoney") < localMaxCutGameMoney)
        {
            PlayerPrefs.SetInt("MaxCutGameMoney", localMaxCutGameMoney);
        }
        if (PlayerPrefs.GetInt("MaxCutSimpleMoney") < localMoney)
        {
            PlayerPrefs.SetInt("MaxCutSimpleMoney", localMoney);
        }
        PlayerPrefs.SetInt("AllMoney", PlayerPrefs.GetInt("AllMoney") + localMoney);
        countMoney = countMoney + localMoney;
        oneTimeMoney = true;
        localMoney = 0;
        PlayerPrefs.SetInt("Money", countMoney);
        StartCoroutine(MoneyGameIsActiveFalse());
    }

    public void MinusBuying(int min)//Списывание монет
    {
        countMoney = countMoney - min;
        PlayerPrefs.SetInt("Money", countMoney);
        moneyScore.text = countMoney.ToString();
        moneyScore2.text = countMoney.ToString();
        moneyScore3.text = countMoney.ToString();
    }

    public IEnumerator MoneyGameIsActiveFalse()//Запись в текст на главном экране
    {
        yield return new WaitForSecondsRealtime(0.1f);
        moneyScore3.text = countMoney.ToString();
    }
    
    private int minOffRenderGameUI = 15;
    private bool renderOffBool = false;
    public void EnableOffRenderGameUISpecial()//Включение режима простоя
    {
        minOffRenderGameUI = 15;
        renderOffBool = true;
    }
    public void EnableRenderGameUI()//Включения ui
    {
        bool bActGame = PlayerPrefs.GetString("FirstGame") == "false";
        pauseBut.SetActive(bActGame);
        moneyGame.SetActive(bActGame);
        textScore.SetActive(bActGame);
        Color clr = data.coin.color;
        data.coin.color = new Color(clr.r, clr.g, clr.b, 1f);
        Color altColor = new Color(1f - c.r, 1f - c.g, 1f - c.b, 0.1960784f);
        circleBackGroundAction.color = altColor;
        minOffRenderGameUI = 15;
        renderOffBool = true;
        return;
    }
    public void EnableOffRenderGameUI()//Поддерживание активности ui
    {
        if (minOffRenderGameUI == 0)
        {
            bool bActGame = PlayerPrefs.GetString("FirstGame") == "false";
            pauseBut.SetActive(bActGame);
            moneyGame.SetActive(bActGame);
            textScore.SetActive(bActGame);
            Color clr = data.coin.color;
            data.coin.color = new Color(clr.r, clr.g, clr.b, 1f);
        }
        minOffRenderGameUI = 15;
        renderOffBool = true;
        return;
    }
    public void DisableOffRenderGameUI()//Выключение режима простоя
    {
        renderOffBool = false;
        minOffRenderGameUI = 15;
        return;
    }
    public IEnumerator OffRenderGameUI()//Таймер работы с простоем
    {
        yield return new WaitForSeconds(1f);
        if (renderOffBool == true)
        {
            if (minOffRenderGameUI > 0)
            {
                minOffRenderGameUI -= 1;
            }
            else
            {
                renderOffBool = false;
                pauseBut.SetActive(false);
                moneyGame.SetActive(false);
                textScore.SetActive(false);
                Color clr = data.coin.color;
                data.coin.color = new Color(clr.r, clr.g, clr.b, 0f);
            }
        }
        StartCoroutine(OffRenderGameUI());
    }

    public void OnApplicationFocus(bool focus)//Контроль активности ui при потере фокуса
    {
        if (forTeach == false && cut.isGame == true)
        {
            if (focus == false)
            {
                DisableOffRenderGameUI();
            }
            else if (focus == true)
            {
                EnableRenderGameUI();
            }
        }
    }

    public void UpLevelHunting(int addExp)//Повышение опыта уровня
    {
        int experience = PlayerPrefs.GetInt("Experience");
        int level = PlayerPrefs.GetInt("Level");
        float endExperience = (7.5f + 2.5f * (level + 1)) * (level + 1);
        int editExp = experience + addExp;

        if (editExp < 25750)
        {
            if (editExp >= endExperience)
            {
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
                PlaySound(7);
            }
        }
        else
        {
            editExp = 25750;
            if (PlayerPrefs.GetInt("Level") == 99)
            {
                PlayerPrefs.SetInt("Level", 100);
                PlaySound(7);
            }
        }
        PlayerPrefs.SetInt("Experience", editExp);
        UpdateLevelHunting();
        return;
    }

    public void UpdateLevelHunting()//Обновление шкалы уровня
    {
        float experience = PlayerPrefs.GetInt("Experience");
        int level = PlayerPrefs.GetInt("Level");
        if (level < 100)
        {
            float startExperience = (7.5f + 2.5f * level) * level;
            float endExperience = (7.5f + 2.5f * (level + 1)) * (level + 1);
            float editExp = (experience - startExperience) / (endExperience - startExperience);
            levelNow.text = level.ToString();
            levelFuture.text = (level + 1).ToString();
            progressBar.fillAmount = editExp;
        }
        else
        {
            levelNow.text = level.ToString();
            levelFuture.text = level.ToString();
            progressBar.fillAmount = 1f;
        }
        return;
    }

    public void AdvertisementAdd()//Наградная реклама
    {
        if (adsManager.CheckInternet() && addReward > 0)
        {
            adsManager.CheckMonetizationReward();
        }
    }

    public void ResultRewardFinished()//Награда за рекламу
    {
        PlaySound(4);
        countMoney = countMoney + 10;
        PlayerPrefs.SetInt("Money", countMoney);
        moneyScore.text = countMoney.ToString();
        moneyScore2.text = countMoney.ToString();
        moneyScore3.text = countMoney.ToString();
        addReward = addReward - 1;
        PlayerPrefs.SetInt("AdsReward", addReward);
        adsReward.SetActive(false);
        return;
    }

    public void ResultRewardSkipped()//Награда за пропуск рекламы
    {
        PlaySound(4);
        countMoney += 3;
        PlayerPrefs.SetInt("Money", countMoney);
        moneyScore.text = countMoney.ToString();
        moneyScore2.text = countMoney.ToString();
        moneyScore3.text = countMoney.ToString();
        addReward -= 1;
        PlayerPrefs.SetInt("AdsReward", addReward);
        adsReward.SetActive(false);
        return;
    }

    public void ResultRestartFinished()//Игнор проигрыша за рекламу
    {
        PlaySound(8);
        addRestart -= 1;
        timer.fillAmount = 1f;
        prelose.SetActive(false);
        PlayerPrefs.SetInt("AdsRestart", addRestart);
        Time.timeScale = 1f;
        return;
    }

    ////////////////////////////////////////////////////////////////////////

    IEnumerator AnimTimerPreLose(float time)
    {
        for (float i = 1; i > 0; i -= Time.deltaTime / time)
        {
            timer.fillAmount = i;
            yield return null;
        }
        timer.fillAmount = 0f;
    }
    IEnumerator AnimLevelHuntClose(float time)
    {
        for (float i = 1; i > 0; i -= Time.deltaTime)
        {
            for (int x = 0; x < levelhunt.Length; x++)
            {
                levelhunt[i].color = new Color();
            }
            timer.fillAmount = i;
            yield return null;
        }
        timer.fillAmount = 0f;
    }
}