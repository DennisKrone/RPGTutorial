using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipment : MonoBehaviour
{
	public const string EquippedNotification = "Equipment.EquippedNotification";
	public const string UnEquippedNotification = "Equipment.UnEquippedNotification";

	public IList<Equippable> Items { get { return items.AsReadOnly(); } }

	List<Equippable> items = new List<Equippable>();

	public void Equip (Equippable item, EquipSlots slots)
	{
		UnEquip(slots);

		items.Add(item);
		item.transform.SetParent(transform);
		item.slots = slots;
		item.OnEquip();

		this.PostNotification(EquippedNotification, item);
	}

	public void UnEquip(Equippable item)
	{
		item.OnUnEquip();
		item.slots = EquipSlots.None;
		item.transform.SetParent(transform);
		items.Remove(item);

		this.PostNotification(UnEquippedNotification, item);
	}

	public void UnEquip(EquipSlots slots)
	{
		for(int i = items.Count -1; i >= 0; --i)
		{
			Equippable item = items[i];
			if((item.slots & slots) != EquipSlots.None)
			{
				UnEquip(item);
			}
		}
	}
}
