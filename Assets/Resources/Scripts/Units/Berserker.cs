﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserker : Character
{
	// Use this for initialization
	protected override void Start ()
	{
		name = "Berserker";
		base.Start();
		maxHp = 15;
		hp = 15;
		move = 3;
		attkRange = 1;
		attk = 8;
		defense = 0;
		cost = 20;
		zeal = 10;
		canMove = true;
		description = "Strong axe loving murderers.";
		topBarDescription = "These powerful juggernauts destroy anything in their path.";
	}
}
