using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Teach : MonoBehaviour
{
    public Menu menu;
    public Image touchIm;
    public Animator animator;
    public RectTransform touchRT;
    public Image circleIm;
    public RectTransform circleRT;
    private bool active = true;
    private int step = 1;
    private bool disabler = true;
    private bool disablertouchim = true;

    public void StartTeach()
    {
        Time.timeScale = 0f;
        active = true;
        step = 1;
        StartCoroutine(DisablerTouchIm());
    }

    public void Down(Vector2 pos)
    {
        if (active)
        {
            if (step == 1)
            {
                bool x = pos.x < -250f && pos.x > -350f;
                bool y = pos.y < 50f && pos.y > -50f;
                if (x && y)
                {
                    step = 2;
                    animator.SetInteger("step", step);
                }
            }
            if (step == 3)
            {
                bool x = pos.x > -50f && pos.x < 50f;
                bool y = pos.y > 250f && pos.y < 350f;
                if (x && y)
                {
                    step = 4;
                    animator.SetInteger("step", step);
                }
            }
        }
    }

    public void BeginDrag()
    {
        if (active)
        {

        }
    }

    public void Drag()
    {
        if (active)
        {

        }
    }

    public void Up(Vector2 pos)
    {
        if (active)
        {
            if (step == 2)
            {
                bool x = pos.x > 200f && pos.x < 400f;
                bool y = pos.y < 100f && pos.y > -100f;
                if (x && y)
                {
                    step = 3;
                    Time.timeScale = 1f;
                    StartCoroutine(Timer());
                }
                else
                {
                    step = 1;
                }
                animator.SetInteger("step", step);
            }
            if (step == 4)
            {
                bool x = pos.x > -50f && pos.x < 50f;
                bool y = pos.y < -250f && pos.y > -350f;
                if (x && y)
                {
                    active = false;
                    if (PlayerPrefs.GetString("FirstGame") == "")
                    {
                        PlayerPrefs.SetString("FirstGame", "true");
                    }
                    Time.timeScale = 1f;
                    menu.Restart();
                    PlayerPrefs.SetString("Teach", "false");
                    StartCoroutine(menu.StartGameNext(0.3f));
                    gameObject.SetActive(false);
                }
                else
                {
                    step = 3;
                    animator.SetInteger("step", step);
                }
            }
        }
    }

    public IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 0f;
    }

    public IEnumerator DisablerTouchIm()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        if (disabler == true)
        {
            if (disablertouchim == true)
            {
                disablertouchim = false;
                touchRT.sizeDelta = new Vector2(200f, 200f);
                circleRT.sizeDelta = new Vector2(50f, 50f);
            }
            else
            {
                disablertouchim = true;
                touchRT.sizeDelta = new Vector2(200f, 190f);
                circleRT.sizeDelta = new Vector2(70f, 70f);
            }
            StartCoroutine(DisablerTouchIm());
        }
    }
}