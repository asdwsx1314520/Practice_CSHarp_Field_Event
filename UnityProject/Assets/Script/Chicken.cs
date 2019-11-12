using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [Header("移動速度"),Range(0,100.0f)]
    public float speed;

    [Header("跳躍高度"),Range(0,100.0f)]
    public float jump;

    [Header("對話內容")]
    public string talk = "咕咕咕~~~";

    [Header("是否取得雞蛋")]
    public bool egg;

    public Animator anim;
    public AnimatorStateInfo animing;

    static int walk = Animator.StringToHash("Base Layer.walk");
    static int run = Animator.StringToHash("Base Layer.run");
    static int eat = Animator.StringToHash("Base Layer.eat");

    private bool isRun;
    private bool isWalk;
    private bool isEat;

    public bool isAction;

    void Start()
    {
        print("遊戲開始");
    }
    
    void Update()
    {
        print(talk);

        animator_off();

        action_on();

        if (isAction && !isEat)
        {
            isAction = false;
        }
    }
    
    /// <summary>
	/// 關閉所有動畫
	/// </summary>
    void animator_off()
    {
        animing = anim.GetCurrentAnimatorStateInfo(0);

        if (animing.fullPathHash == walk){
            isWalk = true;
        }else{
            isWalk = false;
        }

        if (animing.fullPathHash == run){
            isRun = true;
        }else{
            isRun = false;
        }

        if (animing.fullPathHash == eat){
            isEat = true;
        }else{
            isEat = false;
        }

        anim.SetBool("walk", false);
        anim.SetBool("run", false);
        anim.SetBool("eat", false);
    }
    
    /// <summary>
	/// 動畫控制
	/// </summary>
    void action_on()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("run", true);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("walk", true);
        }
    }

    public void getIdle()
    {
        if (!isAction)
        {
            StartCoroutine(action_eat());
        }
    }

    public void getEat()
    {
        isAction = false;
    }

    IEnumerator action_eat()
    {
        if (isAction)
        {
           yield return null;
        }

        float number = Random.Range(0, 5);

        yield return new WaitForSeconds(number);

        anim.SetBool("eat", true);
        isAction = true;
    }
}
