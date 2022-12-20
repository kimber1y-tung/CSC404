using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetDownCooldown : MonoBehaviour
{
    [SerializeField]
    private Image imageCooldown;
    [SerializeField]
    private TMP_Text countdown;
    private bool isCoolingDown = false;
    //the value of 8 is derived from the 2 second activation window plus 6 second cooldown
    private float coolDownLength = 8f;
    private float coolDownTimer = 0f;

    void Start()
    {
        imageCooldown.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GetDown();
        }
        if (isCoolingDown)
        {
            UpdateCoolDown();
        }
    }

    void UpdateCoolDown()
    {
        coolDownTimer -= Time.deltaTime;

        if (coolDownTimer <= 0)
        {
            isCoolingDown = false;
            countdown.text = "";
            imageCooldown.fillAmount = 0f;
        }
        else
        {
            //uncomment to display countdown:
            //countdown.text = Mathf.RoundToInt(coolDownTimer).ToString();
            imageCooldown.fillAmount = coolDownTimer / coolDownLength;
        }
    }

    void GetDown()
    {
        if (!isCoolingDown)
        {
            isCoolingDown = true;
            coolDownTimer = coolDownLength;
        }
    }
}
