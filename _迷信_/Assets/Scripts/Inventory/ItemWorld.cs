using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWorld : MonoBehaviour
{

    //public static ItemWorld SpawnItemWorld(Vector3 _position, Item _item)
    //{
    //    Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, _position, Quaternion.identity);
    //    ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
    //    itemWorld.SetItem(_item);

    //    return itemWorld;
    //}

    //public static ItemWorld DropItem(Vector3 _position, Item _item)
    //{
    //    ItemWorld itemWorld = SpawnItemWorld(_position, _item);
    //    return itemWorld;
    //}

    //private Item item;
    //private SpriteRenderer spriteRenderer;

    //private void Awake()
    //{
    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //}

    //public void SetItem(Item _item)
    //{
    //    item = _item;
    //    spriteRenderer.sprite = item.GetSprite();
    //    spriteRenderer.enabled = false;
    //}

    //public Item GetItem()
    //{
    //    return item;
    //}

    //public void DestroySelf()
    //{
    //    Destroy(gameObject);
    //}
}
