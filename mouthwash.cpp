//========================================================================
// This conversion was produced by the Free Edition of
// Java to C++ Converter courtesy of Tangible Software Solutions.
// Order the Premium Edition at https://www.tangiblesoftwaresolutions.com
//========================================================================

#include "mouthwash.h"

namespace mouthwash
{

	AntiCurse::AntiCurse(const std::wstring &text) : badWords(std::vector<std::wstring>{L"bad", L"martin", L"okay"}), correctiveSymbol(L"*****")
	{
		this->text = text;
		this->parsedText = text;
	}

	AntiCurse::AntiCurse(const std::wstring &text, std::vector<std::wstring> &listOfBadWord) : badWords(listOfBadWord), correctiveSymbol(L"*****")
	{
		this->text = text;
		this->parsedText = text;
	}

	void AntiCurse::run()
	{

		for (int i = 0; i < badWords.size(); i++)
		{
			std::wstring currentBadWord = badWords[i];

			//Phase 1: Replace all bad words with exact match
			parsedText = parsedText.replaceAll(currentBadWord, correctiveSymbol);

			//Phase 2: Find Exaggerated bad words. for instance 'Bad' --> 'Baaaaadd'
			int searchingIndex = 0;
			while (searchingIndex < parsedText.length())
			{

				std::wstring firstLetterOfCurrentBadWord = StringHelper::toString(currentBadWord[0])->toLowerCase();

				//Get index of first letter
				int indexFirstLetter = StringHelper::toLower(parsedText)->find(L" " + firstLetterOfCurrentBadWord, searchingIndex);

				//Get index of last letter
				if (indexFirstLetter != -1)
				{
					int indexLastLetter1 = StringHelper::toLower(parsedText)->find(L" ", indexFirstLetter + 1);
					int indexLastLetter2 = StringHelper::toLower(parsedText)->find(L"\t", indexFirstLetter + 1);
					int indexLastLetter3 = StringHelper::toLower(parsedText)->find(L"\n", indexFirstLetter + 1);
					int indexLastLetter = StringHelper::toLower(parsedText)->length() - 1;

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
					std::wstring word = parsedText.substr(indexFirstLetter + 1, indexLastLetter - (indexFirstLetter + 1));

					//following sequence of letters to uncover bad word
					int indexOfCurrentWordLetter = 0;
					bool wordIsBad = true;
					for (int j = 0; j < word.length(); j++)
					{

						if (!(StringHelper::toString(currentBadWord[indexOfCurrentWordLetter])->toLowerCase()->equals(StringHelper::toString(word[j])->toLowerCase())))
						{

							//if the current character is not a letter from the alphabet
							if (!letterIsInAlphabet(StringHelper::toLower(word)->charAt(j)))
							{
								continue;
							}

							//look at the next letter is continuing the bad word sequence
							if (indexOfCurrentWordLetter < currentBadWord.length() - 1)
							{
								if (StringHelper::toString(currentBadWord[indexOfCurrentWordLetter + 1])->toLowerCase()->equals(StringHelper::toString(word[j])->toLowerCase()))
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
							if (j == word.length() - 1)
							{
								//if the letter of the last word matches the last letter of the criteria
								if (!StringHelper::toString(word[j])->toLowerCase()->equals(StringHelper::toString(currentBadWord[currentBadWord.length() - 1])->toLowerCase()))
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

	bool AntiCurse::letterIsInAlphabet(wchar_t letter)
	{

		std::vector<wchar_t> alphabet = {L'a', L'b', L'c', L'd', L'e', L'f', L'g', L'h', L'i', L'j', L'k', L'l', L'm', L'n', L'o', L'p', L'q', L'r', L's', L't', L'u', L'v', L'w', L'x', L'y', L'z'};

		bool exist = false;
		for (int i = 0; i < alphabet.size(); i++)
		{
			if (alphabet[i] == letter)
			{
				exist = true;
			}
		}

		return exist;
	}

	void AntiCurse::setText(const std::wstring &text)
	{
		this->text = text;
		this->parsedText = text;
	}

	std::wstring AntiCurse::getText()
	{
		return text;
	}

	std::wstring AntiCurse::getParsedText()
	{
		return parsedText;
	}
}
