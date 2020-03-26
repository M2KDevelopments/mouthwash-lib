//========================================================================
// This conversion was produced by the Free Edition of
// Java to C# Converter courtesy of Tangible Software Solutions.
// Order the Premium Edition at https://www.tangiblesoftwaresolutions.com
//========================================================================

using System;
using System.Threading;

namespace mouthwash
{
	/// 
	/// <summary>
	/// @author MARTIN
	/// </summary>
	public class AntiCurse : ThreadStart
	{

		private string text;
		private string parsedText;
		private readonly string[] badWords;
		private readonly string correctiveSymbol;

		/// 
		/// <param name="text"> </param>
		public AntiCurse(string text)
		{
			this.text = text;
			this.parsedText = text;
			this.badWords = new string[]{"bad", "martin", "okay"};
			this.correctiveSymbol = "*****";
		}

		/// 
		/// <param name="text"> </param>
		/// <param name="listOfBadWord"> </param>
		public AntiCurse(string text, params string[] listOfBadWord)
		{
			this.text = text;
			this.parsedText = text;
			badWords = listOfBadWord;
			correctiveSymbol = "*****";
		}

		/// 
		public override void run()
		{

			for (int i = 0; i < badWords.Length; i++)
			{
				string currentBadWord = badWords[i];

				//Phase 1: Replace all bad words with exact match
				parsedText = parsedText.replaceAll(currentBadWord, correctiveSymbol);

				//Phase 2: Find Exaggerated bad words. for instance 'Bad' --> 'Baaaaadd'
				int searchingIndex = 0;
				while (searchingIndex < parsedText.Length)
				{

					string firstLetterOfCurrentBadWord = currentBadWord[0].ToString().ToLower();

					//Get index of first letter
					int indexFirstLetter = parsedText.ToLower().IndexOf(" " + firstLetterOfCurrentBadWord, searchingIndex, StringComparison.Ordinal);

					//Get index of last letter
					if (indexFirstLetter != -1)
					{
						int indexLastLetter1 = parsedText.ToLower().IndexOf(" ", indexFirstLetter + 1, StringComparison.Ordinal);
						int indexLastLetter2 = parsedText.ToLower().IndexOf("\t", indexFirstLetter + 1, StringComparison.Ordinal);
						int indexLastLetter3 = parsedText.ToLower().IndexOf("\n", indexFirstLetter + 1, StringComparison.Ordinal);
						int indexLastLetter = parsedText.ToLower().Length - 1;

						if (indexLastLetter1 != -1)
						{
							indexLastLetter = indexLastLetter1;
						}
						else if (indexLastLetter2 != -1)
						{
							indexLastLetter = indexLastLetter2;
						}
						else if (indexLastLetter3 != -1)
						{
							indexLastLetter = indexLastLetter3;
						}

						//get potential bad word
						string word = parsedText.Substring(indexFirstLetter + 1, indexLastLetter - (indexFirstLetter + 1));

						//following sequence of letters to uncover bad word
						int indexOfCurrentWordLetter = 0;
						bool wordIsBad = true;
						for (int j = 0; j < word.Length; j++)
						{

							if (!(currentBadWord[indexOfCurrentWordLetter].ToString().ToLower().Equals(word[j].ToString().ToLower())))
							{

								//if the current character is not a letter from the alphabet
								if (!letterIsInAlphabet(word.ToLower()[j]))
								{
									continue;
								}

								//look at the next letter is continuing the bad word sequence
								if (indexOfCurrentWordLetter < currentBadWord.Length - 1)
								{
									if (currentBadWord[indexOfCurrentWordLetter + 1].ToString().ToLower().Equals(word[j].ToString().ToLower()))
									{
										//make loop repeat with different index
										indexOfCurrentWordLetter++;
										--j;
									}
									else
									{
										wordIsBad = false;
									}
								}
								else
								{
									wordIsBad = false;
								}

							}
							else
							{
								if (j == word.Length - 1)
								{
									//if the letter of the last word matches the last letter of the criteria
									if (!word[j].ToString().ToLower().Equals(currentBadWord[currentBadWord.Length - 1].ToString().ToLower()))
									{
										wordIsBad = false;
									}
								}
							}
						}

						//replace word if it is bad
						if (wordIsBad)
						{

							parsedText = parsedText.replaceAll(word, correctiveSymbol);
							//reset loop
							searchingIndex = -1;
						}

					}

					//continue loop
					searchingIndex++;
				}
			}
		}

		/// 
		/// <param name="letter">
		/// @return </param>
		private bool letterIsInAlphabet(char letter)
		{

			char[] alphabet = new char[]{'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};

			bool exist = false;
			for (int i = 0; i < alphabet.Length; i++)
			{
				if (alphabet[i] == letter)
				{
					exist = true;
				}
			}

			return exist;
		}

		public virtual string Text
		{
			set
			{
				this.text = value;
				this.parsedText = value;
			}
			get
			{
				return text;
			}
		}


		public virtual string ParsedText
		{
			get
			{
				return parsedText;
			}
		}
	}

}