using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderI : MonoBehaviour
{
    public Vector3 OrderSixtySix = Vector3.left;


    void update()
    {
        SwayForward();
    }

    void SwayForward()
    {
        if (this.transform.position.x > -2.0f)
        {
            OrderSixtySix = Vector3.left;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        }

        this.transform.Translate(OrderSixtySix * Time.smoothDeltaTime);

    }

}
