using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 之所以叫这个名字是因为防止和别的包的内容冲突,这个类的魔改肯定涉及卡牌功能
/// </summary>
public class CardCharacter : MonoBehaviour
{
    Text bloodText;
    public int blood;
    public int maxBlood;

    private void Start()
    {
        blood=maxBlood;
        bloodText=gameObject.GetComponent<Text>();
        bloodText.text=blood.ToString();
    }

    public void Hurt(int damage)
    {
        blood-=damage;
        if(blood<=0) blood=maxBlood;
        bloodText.text=blood.ToString();
    }

    public void Heal(int health)
    {
        blood+=health;
        if(blood>maxBlood)blood=maxBlood;
         bloodText.text=blood.ToString();
    }
    
}
