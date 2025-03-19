using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;
using UnityEngine.Timeline;
using Unity.VisualScripting;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField]
    private float typewriterSpeed = 50f;

    public bool IsRunning { get; private set; }

    private readonly Dictionary<HashSet<char>, float> punctuanions = new Dictionary<HashSet<char>, float>()
    {
        {new HashSet<char>() {'.','!', '?'}, 0.6f},
        {new HashSet<char>() {',',';', ':'}, 0.6f},
    };

    private Coroutine typingCoroutine;

    public void Run(string texToType, TMP_Text textLabel)
    {
        typingCoroutine = StartCoroutine(routine: TypeText(texToType, textLabel));
    }
    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }

    private IEnumerator TypeText(string texToType, TMP_Text textLabel)
    {
        IsRunning = true;
        textLabel.text = string.Empty;


        float t = 0;
        int charIndex = 0;

        while (charIndex < texToType.Length)
        {
            int lastCharIndex = charIndex;

            t += Time.deltaTime * typewriterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(value: charIndex, min: 0, max: texToType.Length);

            for (int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i >= texToType.Length - 1;

                textLabel.text = texToType.Substring(startIndex:0, length:i +1);


                if (IsPunctuation(texToType[i], out float waitTime) && !isLast && !IsPunctuation(texToType[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }


            

            yield return null;
        }
        IsRunning = false;
       

    }

    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach(KeyValuePair<HashSet<char>, float> puntuationCategory in punctuanions)
        {
            if (puntuationCategory.Key.Contains(character))
            {
                waitTime = puntuationCategory.Value;
                return true;
            }
        }
        waitTime = default;
        return false;
    }
}
