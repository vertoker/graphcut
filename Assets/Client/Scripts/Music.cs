using System.Collections;
using UnityEngine;

public class Music : GlobalFunctions//Плохой генератор музыки
{
    public Menu menu;
    public float activity = 0f;
    public AudioSource musicSource1;
    public AudioSource musicSource2;
    public AudioSource musicSource3;
    public AudioSource musicSourceBackground1;
    public AudioSource musicSourceBackground2;
    public AudioSource musicSourceBackground3;
    public AudioClip[] notes;
    public AudioClip[] notesBackground;
    private int lGarmonics;
    private int[] garmonics = new int[] 
    {
        10, 13, 17,
        8, 12, 15,
        5, 8, 12,
        3, 6, 10,
        6, 10, 13,
        13, 17, 20,
        13, 18, 22,
        12, 15, 20,
        15, 18, 22,
        4, 8, 11,
        5, 9, 12,
        3, 7, 10,
        1, 5, 8,
        12, 15, 18,
        12, 17, 20,
        15, 18, 22,
        11, 15, 18,
        16, 20, 23,
        12, 16, 20,
        4, 11, 15,
        7, 11, 14,
        12, 15, 19,
        9, 12, 16,
        9, 13, 16,
        5, 8, 13,
        6, 11, 15,
        2, 5, 9,
        14, 17, 21,
        12, 13, 17,
        10, 12, 17,
        3, 5, 6,
        15, 17, 18,
        10, 13, 15,
        3, 5, 8,
        15, 17, 20,
        3, 10, 19,
        4, 11, 20,
        5, 12, 21,
        6, 13, 22,
        7, 14, 23,
        8, 15, 24,
        1, 8, 11,
        2, 9, 12,
        3, 10, 13,
        4, 11, 14,
        5, 12, 15,
        10, 15, 18,
        12, 17, 20,
        13, 18, 22
    };

    public void Start()
    {
        lGarmonics = garmonics.Length - 3;
        StartCoroutine(ControlActivity());
        StartCoroutine(NotesSpawn());
        //StartCoroutine(NotesBackGround());
    }

    public IEnumerator NotesSpawn()
    {
        if (activity > 1.5f)
        {
            activity = 1.5f;
        }
        yield return new WaitForSecondsRealtime(1f - activity * 0.5f);
        if (menu.sliderMusic.value != 0)
        {
            int strGarm = Random.Range(0, lGarmonics / 3 - 1) * 3;
            musicSource1.clip = notes[garmonics[strGarm] - 1];
            musicSource2.clip = notes[garmonics[strGarm + 1] - 1];
            musicSource3.clip = notes[garmonics[strGarm + 2] - 1];

            strGarm = Random.Range(0, lGarmonics / 3 - 1) * 3;
            musicSourceBackground1.clip = notesBackground[garmonics[strGarm] - 1];
            musicSourceBackground2.clip = notesBackground[garmonics[strGarm + 1] - 1];
            musicSourceBackground3.clip = notesBackground[garmonics[strGarm + 2] - 1];

            musicSource1.Play();
            musicSource2.Play();
            musicSource3.Play();
            
            musicSourceBackground1.Play();
            musicSourceBackground2.Play();
            musicSourceBackground3.Play();
        }
        StartCoroutine(NotesSpawn());
    }

    public IEnumerator NotesBackGround()
    {
        if (activity > 1.5f)
        {
            activity = 1.5f;
        }
        yield return new WaitForSecondsRealtime(2f - activity);
        if (menu.sliderMusic.value != 0)
        {
            int strGarm = Random.Range(0, lGarmonics / 3 - 1) * 3;
            musicSourceBackground1.clip = notesBackground[garmonics[strGarm] - 1];
            musicSourceBackground2.clip = notesBackground[garmonics[strGarm + 1] - 1];
            musicSourceBackground3.clip = notesBackground[garmonics[strGarm + 2] - 1];
            musicSourceBackground1.Play();
            musicSourceBackground2.Play();
            musicSourceBackground3.Play();
        }
        StartCoroutine(NotesBackGround());
    }

    public IEnumerator ControlActivity()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        activity = Stable(activity, 0f, 1f);
        activity = activity - 0.01f;
        if (activity < 0f)
        {
            activity = 0f;
        }
        StartCoroutine(ControlActivity());
    }
}