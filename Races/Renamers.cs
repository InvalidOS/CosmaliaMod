using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using Terraria.ModLoader.IO;

namespace CosmaliaMod.Races
{
	public class RaceRenamers
	{
		Random rng = new Random();
		public static string ElfRename(string name) // I probably missed a lot.
		{
			name = name.ToLower();
			
			char[] nameArray = name.ToCharArray();
			name = string.Empty;
			//string charStore = string.Empty;
			/// Remove diacritics
			for (int c = 0; c < nameArray.Length; c++)
			{
				switch (CharUnicodeInfo.GetUnicodeCategory(nameArray[c]))
				{
					case UnicodeCategory.NonSpacingMark:
						break;
					case UnicodeCategory.SpacingCombiningMark:
						break;
					case UnicodeCategory.EnclosingMark:
						break;
					default:
						name += nameArray[c].ToString();
						break;
				}
			}
			
			nameArray = name.ToCharArray();
			name = string.Empty;
			for (int c = 0; c < nameArray.Length; c++)
			{
				switch (nameArray[c])
				{
					case 'ł':
						name += "l";
						break;
					case 'æ':
						name += "ae";
						break;
					case 'œ':
						name += "oe";
						break;
					case 'ø':
						name += "oe";
						break;
					case 'ð':
						name += "th";
						break;
					case 'þ':
						name += "th";
						break;
					case 'ß':
						name += "s";
						break;
					default:
						name += nameArray[c].ToString();
						break;
				}
			}
			
			name = name.Replace("qu", "kan").Replace("dr", "ryas").Replace("q", "k").Replace("tch",
			"s").Replace("era", "ana").Replace("er", "a").Replace("ph", "hen").Replace("bb",
			"de").Replace("ff", "far").Replace("ai", "ana").Replace("rr", "ran").Replace("lyn",
			"lan").Replace("atl", "atr").Replace("tl","ani").Replace("yn",
			"an").Replace("wh", "h").Replace("oy", "ye").Replace("oi", "ola").Replace("bl",
			"lenal").Replace("b", "l").Replace("oo", "a").Replace("ou", "a").Replace("ch",
			"s").Replace("sh", "s").Replace("u", "a").Replace("wr", "r").Replace("z",
			"s").Replace("j", "y").Replace("p", "g").Replace("ee", "ena").Replace("io",
			"ia").Replace("nth", "nasæn").Replace("th", "s").Replace("t", "n").Replace("or", 
			"ar").Replace("li", "en").Replace("w", "l").Replace("dd", "nian").Replace("se",
			"sa").Replace("d", "n").Replace("el", "as").Replace("sg", "se").Replace("asora",
			"asana").Replace("er", "en").Replace("x", "s").Replace("v", "ra").Replace("ay",
			"ayan").Replace("es", "en").Replace("ck", "kel").Replace("og", "aen").Replace("c",
			"k").Replace("rye", "rya").Replace("mm", "nan").Replace("m", "n").Replace("ll",
			"las").Replace("nn","nen").Replace("ninen", "neyen").Replace("nk",
			"nas").Replace("enen", "enin").Replace("ae", "æ").Replace("oe", "œ").Replace("æa",
			"æ").Replace("rai", "raye").Replace("gh","hb").Replace("hbl", "hay").Replace("hb",
			"he").Replace("ar","ane").Replace("ayn","ayen").Replace("enen", "enin");
			name = new CultureInfo("en-US").TextInfo.ToTitleCase(name);
			return name;
		}
	}
}