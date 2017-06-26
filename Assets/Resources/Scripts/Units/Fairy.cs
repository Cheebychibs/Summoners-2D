﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : Character {
    public const int FAIRY_MANA = 3;
	// Use this for initialization
	protected override void Start ()
    {
        name = "Fairy";
        maxHp = 1;
        hp = 1;
        move = 5;
        attkRange = 1;
        attk = 1;
        defense = 0;
        cost = 2;
        extraDescription = "\n+3 mana per turn";
        canMove = true;
    }

    //Fairies add 3 to the player's mana pool each turn
    public override void EndTurn()
    {
		GameObject.Find("Summoner" + playerNumber + "(clone)").GetComponent<Summoner>().mana += 3;
    }
}
