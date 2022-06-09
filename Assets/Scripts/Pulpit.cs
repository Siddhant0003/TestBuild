using CasualGame.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Pulpit : MonoBehaviour
{
    public Time time;
    public IObjectPooling IObjectPoolingListner;
    public IGameManager gameManagerListner;
    public TextMesh timer;
    public bool create;

    public void Init(IObjectPooling IObjectPoolingListner , IGameManager gameManagerListner)
    {
        this.IObjectPoolingListner = IObjectPoolingListner;
        this.gameManagerListner = gameManagerListner;
    }

    private void OnEnable()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        float duration = Random.Range(4, 5); 
        float normalizedTime = 0;

        while (normalizedTime <= duration)
        {
           
              if (Mathf.Round(normalizedTime)== 2f && !create)
          
            {
                create = true;
                gameManagerListner.GenerateObject(IObjectPoolingListner.GetPooledObject());
            }

           normalizedTime += Time.deltaTime / duration;        
            timer.text = normalizedTime.ToString("F2");

           
            if (Mathf.RoundToInt(normalizedTime) ==3)
            {
                this.gameObject.SetActive(false);
                create = false;
            }

            yield return null;
        }
    }
    
}

public class Timer
{
    public int nextGenerationTime = 3;

}

