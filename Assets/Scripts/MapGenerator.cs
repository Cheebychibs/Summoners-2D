﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public string[,] map;
	private float spacer;
	private Cursor cursor;
	// Use this for initialization
	void Start ()
	{
		cursor = FindObjectOfType<Cursor>();
		spacer = cursor.size / 1.5f;
		string tempMap = "MMGGGGGGGG MGGGGGGGGG GGGWWWWGGG GGGGGGGGGM GGGGGGGGMM";
		string[] n = tempMap.Split(" "[0]);
		MakeMap(n);

		for(int i = 0; i < n.Length; i++)
		{
			for(int j = 0; j < n[0].Length; j++)
			{
				if(map[i,j] == "M")
				{
					GameObject m = (GameObject)Instantiate(Resources.Load("Prefab/Mountain"));
					m.transform.position = new Vector3(spacer* j, spacer * i, 0);
				}
				else if (map[i,j] == "G")
				{
					GameObject m = (GameObject)Instantiate(Resources.Load("Prefab/Grass"));
					m.transform.position = new Vector3(spacer * j, spacer * i, 0);
				}
				else if (map[i,j] == "W")
				{
					GameObject m = (GameObject)Instantiate(Resources.Load("Prefab/Water"));
					m.transform.position = new Vector3(spacer * j, spacer * i, 0);
				}
			}
		}
		cursor.transform.position = new Vector3(spacer * 5, spacer * 2, 0);
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	void MakeMap(string[] n)
	{
		map = new string[n.Length, n[0].Length];
		for(int i = 0; i < n.Length; i++)
		{
			for(int j = 0; j < n[i].Length; j++)
			{
				map[i, j] = n[i].Substring(j, 1);
				print("I: " + i + " J:" + j);
			}
		}
	}
}
