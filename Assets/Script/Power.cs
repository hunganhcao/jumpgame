using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Power :Singleton<Power>
{
    [SerializeField]
    private Slider powerSlider;
    [SerializeField]
    private Image targetImg;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private TextMeshProUGUI textScore;
    bool isPowerup = true;
    bool isDirection = true;
    float power = 0;
    float powerSpeed=150f ;

    float startValue;
    float endValue;
    float xImg;
    float space;

    float changeRate;
    
    // Start is called before the first frame update
    void Start()
    {
        PowerActive();
        textScore.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPowerup)
        {
            PowerActive();
			if (Input.GetButtonDown("Jump"))
			{
				//check and speed up
				CheckSpace();
				StartPower();

			}
		}
        
    }

	private void CheckSpace()
	{
		isPowerup = false;
        float val = powerSlider.value;
        if(val>startValue && val < endValue)
        {
            powerSpeed *= 1.2f;
			if (val > startValue + 2 * space && val < endValue - 2 * space)
			{
				changeRate = 0.5f;
			}
			else if(val> startValue + 1 * space && val < endValue - 1 * space)
			{
                changeRate = 0.3f;
			}
            else
            {
                changeRate=0.2f;
            }
		}
        else
        {
            changeRate = 0f;
        }
        Debug.Log(changeRate.ToString("F3",CultureInfo.InvariantCulture));
        player.GetComponent<ThirdPersonController>().SpeedUp(1f+changeRate);
		
	}

	void PowerActive()
    {
        if(isDirection)
        {
            power += Time.deltaTime * powerSpeed;
            if (power > 100)
            {
                isDirection = false;
                power = 100;
            }
        }
        else
        {
            power -= Time.deltaTime * powerSpeed; 
            if(power < 0 ) {
                isDirection = true;
                power = 0;
            }
        }
        powerSlider.value = power/100;
        
    }
    public void EndPower()
    {
        isPowerup= false;
		powerSlider.gameObject.SetActive(false);
        
	}
    public void StartPower()
    {
        //enable slider
        powerSlider.value = 0;
        isPowerup=true;
		powerSlider.gameObject.SetActive(true);
        //
        float slideWitdh= powerSlider.GetComponent<RectTransform>().sizeDelta.x;
		float imageWitdh = targetImg.GetComponent<RectTransform>().sizeDelta.x;
        //set position img
        xImg = Random.Range(-(slideWitdh / 2 - imageWitdh / 2), slideWitdh / 2 - imageWitdh / 2);
		targetImg.GetComponent<RectTransform>().anchoredPosition = new Vector2(xImg, 0);
        //dung de tinh 5 khoang
        startValue= (xImg-imageWitdh/2+slideWitdh/2)/slideWitdh;
        endValue= (xImg+imageWitdh/2+slideWitdh/2)/ slideWitdh;
        space=(endValue-startValue)/5;
	}
    public void ViewScore(float score)
    {
        textScore.gameObject.SetActive (true);
        textScore.text= (score/2).ToString()+" m";
    }

}
