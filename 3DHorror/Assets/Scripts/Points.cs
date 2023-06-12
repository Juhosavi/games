using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{

    public Text MyScoreText;
    private int ScoreNum;
    // Start is called before the first frame update
    void Start()
    {
        ScoreNum = 0;
        MyScoreText.text = "Score : " + ScoreNum;

    }
    private void OnTriggerEnter2D(Collider2D Diamond)
    {
        if (Diamond.tag == "Diamond")  //jos osutaan timanttiin
        {
            ScoreNum += 1; //yksi piste
            Destroy(Diamond.gameObject); // tuhoa timantti
            MyScoreText.text = "Score : " + ScoreNum; //päivitä pistet.
        }

    }
    // Update is called once per frame

}
