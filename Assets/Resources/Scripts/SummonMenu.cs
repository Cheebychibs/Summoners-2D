﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class SummonMenu : MonoBehaviour
{
	public Character selectedChar;
	public Object[] characters;
	public List<SummonOption> summonOptions;
	public HUB hub;
	//this will make things easier for the player
	public List<SummonOption> canAfford;
	public List<SummonOption> cannotAfford;
	public bool canMove;

	private int index;
	// Use this for initialization
	void Start ()
	{
		canMove = false;
		hub = GameObject.FindObjectOfType<HUB>();
		characters = Resources.LoadAll("Prefab/Characters/Units");
		index = 0;
		summonOptions = new List<SummonOption>();
		for(int i = 0; i < characters.Length; i++)
		{
			summonOptions.Add(((GameObject)(GameObject.Instantiate(Resources.Load("Prefab/SummonMenu/SummonMenuOption")))).GetComponent<SummonOption>());
			summonOptions[i].c = ((GameObject)characters[i]).GetComponent<Character>();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (canMove)//dont worry about any of this unless a player is on this menu
		{
			selectedChar = summonOptions[index].c;
			fillStatPortion();

			separateLists();//separate your list of characters into 2 lists
			sortByName();//sort those two lists
			combineLists();//bring those lists back together
			getInput();//see if the player wanted to move up/down the list
		}
	}

	/*
	 * Take the stats from the selected character, and display them
	 */ 
	void fillStatPortion()
	{
		GameObject.Find("SelectedStats").GetComponent<TextMesh>().text = "  ATK: "+summonOptions[index].c.attk+"\t\tRNG: "+ summonOptions[index].c.attkRange+"\n  DEF: "+ summonOptions[index].c .defense+ "\t\tMOV:"+ summonOptions[index].c.move+ "\nSPCH: "+ summonOptions[index].c .speech+ "\t\tLOY: "+ summonOptions[index].c.loyalty+ "\n"+summonOptions[index].c.extraDescription+"\n\t\t\tCost: "+ summonOptions[index].c.cost+ "\n\t\t\tMana: "+hub.getCurrentSummoner().mana;
		GameObject.Find("SelectedName").GetComponent<TextMesh>().text = selectedChar.name;
		print(selectedChar.iconPath);
		GameObject.Find("StatDisplay").transform.FindChild("Icon").GetComponent<SpriteRenderer>().sprite = Resources.Load<UnityEngine.Sprite>(selectedChar.iconPath);
	}

	/*
	 * I feel like reading this list of summonable things will be easier if they are set up so you see the things you CAN summon first, and then you see the things
	 * that you cannot afford. Each of these lists will also be alphabetized!
	 */ 
	void separateLists()
	{
		canAfford.Clear();
		cannotAfford.Clear();
		for(int i = 0; i < summonOptions.Count; i++)
		{
			if(summonOptions[i].c.cost > hub.getCurrentSummoner().mana)
			{
				cannotAfford.Add(summonOptions[i]);
			}
			else
			{
				canAfford.Add(summonOptions[i]);
			}
		}
	}

	//you take those two lists and put them back together. CanAfford is added first because you want to see those first.
	void combineLists()
	{
		summonOptions.Clear();
		for(int i = 0; i < canAfford.Count; i++)
		{
			summonOptions.Add(canAfford[i]);
		}
		for (int i = 0; i < cannotAfford.Count; i++)
		{
			summonOptions.Add(cannotAfford[i]);
		}
		//NOW WE FINALLY MAKE THESE STUPID OPTION OBJECTS
		for(int i = 0; i < summonOptions.Count; i++)
		{
			//for now these are hard coded, because their positions are pretty much statically based on the position of this first one, which is pretty much statically
			//based on the position of the menu, WHICH Im pretty sure is just gonna stay where it is.
			summonOptions[i].transform.position = new Vector3(-513.4f,(6.6f - 3.84f*i),-1f);
			summonOptions[i].transform.localScale = new Vector3(41.72f, 6, 1);
		}
	}

	void getInput()
	{
		summonOptions[index].GetComponent<SpriteRenderer>().color = Color.green;
		//move down the list
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			if(index+1 < summonOptions.Count)
			{
				summonOptions[index].GetComponent<SpriteRenderer>().color = Color.white;
				index++;
				summonOptions[index].GetComponent<SpriteRenderer>().color = Color.green;
			}
		}
		//move up the list
		else if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			if(index-1 > -1)
			{
				summonOptions[index].GetComponent<SpriteRenderer>().color = Color.white;
				index--;
				summonOptions[index].GetComponent<SpriteRenderer>().color = Color.green;
			}
		}
		//select a character
		else if(Input.GetKeyDown(KeyCode.Z))
		{
			SummonCharacter(selectedChar);
		}
		//go back to the map
		else if(Input.GetKeyDown(KeyCode.X))
		{
			hub.lastTimeX = Time.time;
			canMove = false;
			hub.cam.moveCamera(hub.cursor.transform.position);
			hub.cam.toggleChildren();
			hub.moveMenuHandler.canMove = true;
		}
	}

	void SummonCharacter(Character c)
	{
		print("WIP");
	}
	void sortByName()
	{
		canAfford = canAfford.OrderBy(g => g.c.name).ToList();
		print("CannotAfford has: " + cannotAfford.Count);
		cannotAfford = cannotAfford.OrderBy(g => g.c.name).ToList();
	}

	//Below this line, I hope to fill a bunch of sort options (currently only going to sort by name)
	void sortByCost()
	{

	}
}
