using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LayerOrderHandler : MonoBehaviour
{
    //private SpriteRenderer sprite;
    //private bool isChild = false;
    private List<Renderer> renderList;
    [SerializeField] private bool isStatic = true;
    /*[HideInInspector] */public int layerOrder;
    [SerializeField] private LayerOrderHandler parentOrder;
    //private ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        //sprite = GetComponent<SpriteRenderer>();
        //sprite.sortingOrder = (int)(-transform.position.y * 10);
        //isChild = (transform.parent != null);

        renderList = GetComponents<Renderer>().ToList();
        
        if (transform.parent != null)
        {
            //parentOrder = GetComponentInParent<LayerOrderHandler>();
            parentOrder = transform.parent.gameObject.GetComponent<LayerOrderHandler>();
            if (parentOrder != null) if(parentOrder.gameObject == gameObject) parentOrder = null;
        }
        
        if (parentOrder != null) parentOrder.CalculateOrder();
        CalculateOrder();
    }

    public void CalculateOrder()
    {
        

		renderList ??= GetComponents<Renderer>().ToList();
        if (parentOrder != null) { layerOrder = parentOrder.layerOrder + 1; }
        else layerOrder = (int)(-transform.position.y * 10);
        foreach (var render in renderList)
        {
            render.sortingOrder = layerOrder;
        }
        var sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) sprite.sortingOrder++;
    }
    // Update is called once per frame
    void Update()
    {
		if (PlayerManager.instance.GetPlayerController() != null)
		{
			if (transform.position.y <= PlayerManager.instance.GetPlayerController().transform.position.y)
				GetComponent<SpriteRenderer>().sortingLayerName = "FrontOfPlayer";
			else
				GetComponent<SpriteRenderer>().sortingLayerName = "Default";
		}

		//if (!isStatic)
		//{
		//    CalculateOrder();
		//}
	}
}
