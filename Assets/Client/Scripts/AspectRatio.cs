using UnityEngine;
using System;

public class AspectRatio : MonoBehaviour//Стабилизация позиций
{
    public Cut cut;
    public Camera cam;
    public RectTransform canvas;
    [Header("Game")]
    public RectTransform levelHunting;
    public RectTransform textScore;
    public RectTransform money;
    public RectTransform pauseBut;
    [Header("Pause")]
    public RectTransform continie;
    public RectTransform restart;
    public RectTransform exit;
    [Header("StartMenuUI")]
    public RectTransform settings;
    public RectTransform copy;
    public RectTransform market;
    public RectTransform cup;
    public RectTransform textScore2;
    public RectTransform money2;
    public RectTransform lockIn;
    [Header("Market")]
    public RectTransform back1;
    public RectTransform colorsets;
    public RectTransform content;
    public RectTransform advertisement;
    public RectTransform money3;
    public RectTransform buy;
    [Header("Settings")]
    public RectTransform back2;
    public RectTransform sound;
    public RectTransform music;
    public RectTransform rate;
    public RectTransform authors;
    public RectTransform hand;
    public RectTransform notifications;
    [Header("YouLose")]
    public RectTransform restart2;
    public RectTransform exit2;
    public RectTransform close;
    private float main = 0.005208333f;
    private float step = 0.0002900335f;

    public void Awake()
    {
        //Математические операции
        float x = (float)Math.Round(1f / cam.aspect * 9f - 16f, 1);
        if (x == 0f) { return; }
        float math = -0.0000030685f * (x * x * x) + 0.0000152255f * (x * x) - 0.0003187546f * x + 0.0052294214f;
        Vector3 mod = new Vector3(0f, x * 60f);
        canvas.localScale = new Vector3(math, math, 1f);
        cut.c = 1f / math;

        //Основной скрипт стабилизации позиций
        levelHunting.localPosition += mod;
        textScore.localPosition += mod;
        money.localPosition += mod;
        pauseBut.localPosition -= mod;

        continie.localPosition -= mod;
        restart.localPosition -= mod;
        exit.localPosition -= mod;

        cup.localPosition += mod;
        textScore2.localPosition += mod;
        money2.localPosition += mod;
        lockIn.localPosition += mod;
        settings.localPosition -= mod;
        copy.localPosition -= mod;
        market.localPosition -= mod;

        back1.localPosition -= mod;
        colorsets.sizeDelta += new Vector2(0f, x * 120f);
        advertisement.localPosition -= mod;
        money3.localPosition -= mod;
        buy.localPosition -= mod;
        
        sound.localPosition += mod;
        music.localPosition += mod;
        hand.localPosition += mod;
        notifications.localPosition += mod;
        back2.localPosition -= mod;
        rate.localPosition -= mod;
        authors.localPosition -= mod;

        restart2.localPosition -= mod;
        exit2.localPosition -= mod;
        close.localPosition -= mod;
    }
}