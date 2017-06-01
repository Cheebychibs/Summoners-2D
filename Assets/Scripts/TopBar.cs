﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBar : MonoBehaviour
{
	public Character selectedChar;
	private Cursor cursor;
	private Character[] chars;
	private GameObject topBar;
	// Use this for initialization
	void Start ()
	{
		cursor = FindObjectOfType<Cursor>();
		chars = FindObjectsOfType<Character>();
		topBar = GameObject.Find("TopBar");
	}
	
	// Update is called once per frame
	void Update ()
	{
		chars = FindObjectsOfType<Character>();
		if (isCursorOnCharacter())
		{
			topBar.SetActive(true);
			FillTopInfo();
		}
		else
		{
			topBar.SetActive(false);
		}
	}
	bool isCursorOnCharacter()
	{
		int curX = cursor.getIntX();
		int curY = cursor.getIntY();
		for(int i = 0; i < chars.Length; i++)
		{
			if(chars[i].getIntX() == curX && chars[i].getIntY() == curY)
			{
				selectedChar = chars[i];
				return true;
			}
		}
		return false;
	}
	void FillTopInfo()
	{
		GameObject hp = topBar.transform.FindChild("HP").gameObject;
		GameObject hPBar = topBar.transform.FindChild("HPBar").gameObject;
		GameObject name = topBar.transform.FindChild("Name").gameObject;
		GameObject characterIcon = topBar.transform.FindChild("CharacterIcon").gameObject;
		GameObject stats = topBar.transform.FindChild("Stats").gameObject;
		GameObject playerNum = topBar.transform.FindChild("PlayerNum").gameObject;
		GameObject description = topBar.transform.FindChild("Description").gameObject;

		//set HP
		hp.GetComponent<TextMesh>().text = "HP: " + selectedChar.hp.ToString() + "/" + selectedChar.maxHp.ToString();
		//name
		name.GetComponent<TextMesh>().text = selectedChar.name;
		//stats
		stats.GetComponent<TextMesh>().text = "   ATK: "+selectedChar.attk + "\tRNG: " + selectedChar.attkRange + "\n   DEF: " + selectedChar.defense + "\tMOV: " + selectedChar.move+"\nSPCH: "+selectedChar.speech+"\t LOY: "+selectedChar.loyalty;
		//playerNumber
		playerNum.GetComponent<TextMesh>().text = "Player " + selectedChar.playerNumber;
		//description
		description.GetComponent<TextMesh>().text = selectedChar.extraDescription;

		//icon
		//math for HPbar
		//So as far as I can tell, -5 should be a constant as the length/position of the health bar. This may change if I can find out where the number really comes from.
		//if you're not me and this is interesting/you want to know why I used -5 to start with, ask me. Lets start a dialogue!
		hPBar.transform.localPosition = new Vector3(3 * (selectedChar.hp / selectedChar.maxHp)-4, 0.5f, 1);
	}
}
