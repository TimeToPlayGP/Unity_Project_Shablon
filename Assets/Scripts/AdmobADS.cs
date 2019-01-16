using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;

public class AdmobADS : MonoBehaviour
{
    public bool isTest = false;
    public float TimeNoBannerShow = 20;

    static RewardBasedVideoAd rewardBasedVideo;
    static BannerView bannerView;
    static InterstitialAd interstitial;
    string adUnitIdBanner = "";
    string adUnitIdInterstitial = "";
    string adUnitIdVideo = "";

    void Start()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        isShowBanner = true;
        RequestBanner();
        RequestInterstitial();
        RequestRewardBasedVideo();

        DontDestroyOnLoad(this);
    }

    private float EndTime = 0;
    private static bool isShowTimeBanner = true;

    //Проверка загрузки баннера
    static bool isLoadedBanner
    {
        get;
        set;
    }

    //Проверка загрузки межстраничного объявления
    static bool isLoadedIntersteller
    {
        get;
        set;
    }

    //Проверка загрузки видео
    static bool isLoadedVideo
    {
        get;
        set;
    }

    //Показан ли банер?
    static bool isShowBanner
    {
        get;
        set;
    }

    void Update()
    {
        if (isShowTimeBanner) return;
        if (EndTime > TimeNoBannerShow) { isShowTimeBanner = true; ShowBanner(); EndTime = 0; return; }
        EndTime += Time.deltaTime;
    }

    /// <summary>
    /// Регистрация баннера
    /// </summary>
    private void RequestBanner()
    {
        bannerView = new BannerView(adUnitIdBanner, AdSize.Banner, AdPosition.Bottom);

        // Загрузился
        bannerView.OnAdLoaded += HandleOnAdLoaded;
        // Не загрузился
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Нажали
        bannerView.OnAdOpening += HandleOnAdOpened;

        AdRequest request;
        if (isTest)
        {
            request = new AdRequest.Builder()           //Тестовый
                 .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
                 .AddTestDevice("865971023062298")  // My test device.
                 .Build();
        }//2077ef9a63d2b398840261c8221a0c9b
        else
        {
            request = new AdRequest.Builder().Build();
        }

        bannerView.LoadAd(request);
    }

    private void HandleOnAdOpened(object sender, EventArgs e)
    {
        isShowTimeBanner = false;
        bannerView.Hide();
    }

    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        isLoadedBanner = false;
        throw new NotImplementedException();
    }

    private void HandleOnAdLoaded(object sender, EventArgs e)
    {
        isLoadedBanner = true;
        isShowTimeBanner = true;
        isShowBanner = true;
        if (!Camera.main.GetComponent<WorkAdmobADS>().HideBanner)
            bannerView.Show();
    }

    /// <summary>
    /// Регистрация межстраничного объявления
    /// </summary>
    private void RequestInterstitial()
    {
        interstitial = new InterstitialAd(adUnitIdInterstitial);

        // Загрузился
        interstitial.OnAdLoaded += Interstitial_OnAdLoaded;
        // Не загрузился
        interstitial.OnAdFailedToLoad += Interstitial_OnAdFailedToLoad;

        AdRequest request;
        if (isTest)
        {
            request = new AdRequest.Builder()           //Тестовый
                 .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
                 .AddTestDevice("865971023062298")  // My test device.
                 .Build();
        }
        else
        {
            request = new AdRequest.Builder().Build();
        }

        interstitial.LoadAd(request);
    }

    private void Interstitial_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        isLoadedIntersteller = false;
    }

    private void Interstitial_OnAdLoaded(object sender, EventArgs e)
    {
        isLoadedIntersteller = true;
    }

    /// <summary>
    /// Регистрация видео
    /// </summary>
    private void RequestRewardBasedVideo()
    {

        rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Загрузился
        rewardBasedVideo.OnAdLoaded += RewardBasedVideo_OnAdLoaded;
        // Не загрузился
        rewardBasedVideo.OnAdFailedToLoad += RewardBasedVideo_OnAdFailedToLoad;

        // has rewarded the user.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;


        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideo.LoadAd(request, adUnitIdVideo);
    }

    private void RewardBasedVideo_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        isLoadedVideo = false;
    }

    private void RewardBasedVideo_OnAdLoaded(object sender, EventArgs e)
    {
        isLoadedVideo = true;
    }

    /// <summary>
    /// Показать полноэкранное объявление
    /// </summary>
    public static void ShowInterstitial()
    {
        if (!isLoadedIntersteller) return;
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

    /// <summary>
    /// Показать баннер
    /// </summary>
    public static void ShowBanner()
    {
        if (!isLoadedBanner) return;
        if (!isShowBanner)
            if (isShowTimeBanner && bannerView != null) { isShowBanner = true; bannerView.Show(); }
    }

    /// <summary>
    /// Скрыть баннер
    /// </summary>
    public static void HideBanner()
    {
        if (!isLoadedBanner) return;
        if (bannerView != null && isShowBanner) { isShowBanner = false; bannerView.Hide(); }
    }

    /// <summary>
    /// Показать видео
    /// </summary>
    static int NumberTrump;
    public static void ShowVideo(int numberTrumb)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable) { Camera.main.GetComponent<Shop>().OnInternet(); return; }
        NumberTrump = numberTrumb;
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
        else { Camera.main.GetComponent<Shop>().OnReclamy(); }
    }

    /// <summary>
    /// Вознаграждение
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        //Выполнять действия
    }

    //Нет сети
    //if(Application.internetReachability == NetworkReachability.NotReachable
    //Мобильные сети
    //if(Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
    //Wi-fi
    //if(Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
}
