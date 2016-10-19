using UnityEngine;
using System.Collections;
using  System.Xml;
public class test : MonoBehaviour
{
    void Start()
    {
       hehe x=new hehe();
       print(x.a);//0
       x.ChangeDate(4);
        print(x.a);//4
        object o = x;
        print(((hehe)o).a);//4
        ((hehe)o).ChangeDate(2);
        print(((hehe)o).a);//4

    }

    struct  hehe
    {
        public int a ;

        public void ChangeDate(int b)
        {
            a = b;
        }
    }
  
    class test3
    {
        public int gg = 5;
        public test3()
        {
            print("3 init");
           registerEvent();
        }
        public virtual void hehe(object sender,DispatchData data)
        {

            print("receive now in parent");
        }

        public void registerEvent()
        {
            print("registerEvent now");
            EventDispatcher.GetInstance().RegisterEvent(hehe);
        }
    }

    class test4:test3
    {
        public test4()
        {
            print("4 init");
        }
        public override void hehe(object sender, DispatchData data)
        {
            print("receive now in son" );
        }
    }
}
