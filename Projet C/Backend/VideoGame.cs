﻿using Projet_C.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Projet_C.Backend
{
    public class VideoGame
	{
		private int id;
		private string name;
		private string console;
		private int creditCost;

		public VideoGame(string name, string cons)
		{
			Console=cons;
			Name=name;

		}
		public int Id { get; set; }
		public string Name { get; set; }
		public string Console { get; set; }
		public int CreditCost
		{
			get { return creditCost; }
			set 
			{ 
				if(value>0 && value<=5)
					creditCost = value;
				else
					creditCost = int.MaxValue;
			}
		}


		}
}
