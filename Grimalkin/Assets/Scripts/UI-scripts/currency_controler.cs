using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class currency_controler : MonoBehaviour
{

    // UI to adjust currency
    public GameObject money_text_obj;
    public GameObject credit_text_obj;
    private TextMeshProUGUI money_text;
    private TextMeshProUGUI credit_text;
    public float fade_out_time = 1.25f;


    // Start is called before the first frame update
    void Start()
    {
        money_text = money_text_obj.GetComponent<TextMeshProUGUI>();
        credit_text = credit_text_obj.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // increaes / decrease money by "change"
    public void change_money(int change)
    {
        StartCoroutine(change_money_grad(change));
    }

    // increaes / decrease credits by "change"
    public void change_credits(int change)
    {
        StartCoroutine(change_credits_grad(change));
    }



    private IEnumerator change_credits_grad(int change)
    {
        //Debug.Log("fadingout");
        TextMeshProUGUI text_to_fade = credit_text;
        float current_credits = float.Parse(credit_text.text);
        float duration = fade_out_time; //Fade out duration.



        ////////////////// alter the player of the chnage with colour and arrow
        /////// fade out ////
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0.75f, 0.3f, currentTime / duration);
            text_to_fade.color = new Color(text_to_fade.color.r, text_to_fade.color.g, text_to_fade.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        ///////// change text
        if (change < 0) // if it is a decrease
        {
            credit_text.text = $"<color=red>{current_credits + change} ↓</color>";
        }
        else // if it is an icnrease
        {
            credit_text.text = $"<color=green>{current_credits + change} ↑</color>";
        }

        ////// fade in
        currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0.75f, 0.3f, 1 - (currentTime / duration));
            text_to_fade.color = new Color(text_to_fade.color.r, text_to_fade.color.g, text_to_fade.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }


        ////////////////////// make perminate change
        /////// fade out ////
        currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0.75f, 0.3f, currentTime / duration);
            text_to_fade.color = new Color(text_to_fade.color.r, text_to_fade.color.g, text_to_fade.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        ///////// change text
        credit_text.text = $"{current_credits + change}";

        ////// fade in
        currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0.75f, 0.3f, 1 - (currentTime / duration));
            text_to_fade.color = new Color(text_to_fade.color.r, text_to_fade.color.g, text_to_fade.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    private IEnumerator change_money_grad(int change)
    {
        //Debug.Log("fadingout");
        TextMeshProUGUI text_to_fade = money_text;
        float current_money = float.Parse(money_text.text);
        float duration = fade_out_time; //Fade out duration.



        ////////////////// alter the player of the chnage with colour and arrow
        /////// fade out ////
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0.75f, 0.3f, currentTime / duration);
            text_to_fade.color = new Color(text_to_fade.color.r, text_to_fade.color.g, text_to_fade.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        ///////// change text
        if (change < 0) // if it is a decrease
        {
            money_text.text = $"<color=red>{current_money + change} ↓</color>";
        }
        else // if it is an icnrease
        {
            money_text.text = $"<color=green>{current_money + change} ↑</color>";
        }

        ////// fade in
        currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0.75f, 0.3f, 1 - (currentTime / duration));
            text_to_fade.color = new Color(text_to_fade.color.r, text_to_fade.color.g, text_to_fade.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }


        ////////////////////// make perminate change
        /////// fade out ////
        currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0.75f, 0.3f, currentTime / duration);
            text_to_fade.color = new Color(text_to_fade.color.r, text_to_fade.color.g, text_to_fade.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        ///////// change text
        money_text.text = $"{current_money + change}";

        ////// fade in
        currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0.75f, 0.3f, 1 - (currentTime / duration));
            text_to_fade.color = new Color(text_to_fade.color.r, text_to_fade.color.g, text_to_fade.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

}