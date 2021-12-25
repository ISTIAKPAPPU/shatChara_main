using UnityEngine;

public class JoyStickController : MonoBehaviour
{
    public GameObject joyStickObj;

    public static JoyStickController jc;

    private void Awake()
    {
        if (jc == null)
        {
            jc = this;
        }
    }

    public void ActiveJoystick()
    {
        joyStickObj.SetActive(true);
    }

    public void DeActiveJoystick()
    {
        joyStickObj.SetActive(false);
        joyStickObj.transform.GetComponent<MovementJoystick>().PointerUp();
    }
}