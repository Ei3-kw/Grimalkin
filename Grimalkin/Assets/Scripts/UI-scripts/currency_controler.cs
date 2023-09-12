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
        float current_money = float.Parse(money_text.text);

        // first show the arrows of the increase with colour
        StartCoroutine(fade_out_text(money_text));
        if (change < 0) // if it is a decrease
        {
            money_text.text = $"<color=red>{current_money + change}.00 ↓</color>";
        }
        else // if it is an icnrease
        {
            money_text.text = $"<color=green>{current_money + change}.00 ↑</color>";
        }
        StartCoroutine(fade_in_text(money_text));

        // now show the final new text
        StartCoroutine(fade_out_text(money_text));
        money_text.text = $"{current_money - change}.00";
        StartCoroutine(fade_in_text(money_text));
    }

    // increaes / decrease credits by "change"
    public void change_credits(int change)
    {
        float current_credits = float.Parse(credit_text.text);

        // first show the arrows of the increase with colour
        StartCoroutine(fade_out_text(credit_text));
        if (change < 0) // if it is a decrease
        {
            credit_text.text = $"<color=red>{current_credits + change} ↓</color>";
        }
        else // if it is an icnrease
        {
            credit_text.text = $"<color=green>{current_credits + change} ↑</color>";
        }
        StartCoroutine(fade_in_text(credit_text));

        // now show the final new text
        StartCoroutine(fade_out_text(credit_text));
        credit_text.text = $"{current_credits - change}";
        StartCoroutine(fade_in_text(credit_text));
    }



    private IEnumerator fade_out_text(TextMeshProUGUI text_to_fade)
    {
        /////// fade out ////
        float duration = fade_out_time; //Fade out duration.
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0.75f, 0.3f, currentTime / duration);
            text_to_fade.color = new Color(text_to_fade.color.r, text_to_fade.color.g, text_to_fade.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    private IEnumerator fade_in_text(TextMeshProUGUI text_to_fade)
    {
        /////// fade in ////
        float duration = fade_out_time; //Fade out duration.
        float currentTime = 0f;
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