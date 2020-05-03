﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
	public Item currentItem;
	public List<Item> items = new List<Item>();
	public int nuberOfKeys;

	public void AddItem(Item itemToAdd)
	{
		if (itemToAdd.isKey)
		{
			nuberOfKeys++;
		}
		else
		{
			if (!items.Contains(itemToAdd))
			{
				items.Add(itemToAdd);
			}
		}
	}

}
