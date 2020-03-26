#pragma once

//========================================================================
// This conversion was produced by the Free Edition of
// Java to C++ Converter courtesy of Tangible Software Solutions.
// Order the Premium Edition at https://www.tangiblesoftwaresolutions.com
//========================================================================

#include <string>
#include <vector>
#include "stringhelper.h"

namespace mouthwash
{

	/// 
	/// <summary>
	/// @author MARTIN
	/// </summary>
	class AntiCurse 
	{

	private:
		std::wstring text;
		std::wstring parsedText;
		std::vector<std::wstring> const badWords;
		const std::wstring correctiveSymbol;

		/// 
		/// <param name="text"> </param>
	public:
		AntiCurse(const std::wstring &text);

		/// 
		/// <param name="text"> </param>
		/// <param name="listOfBadWord"> </param>
		AntiCurse(const std::wstring &text, std::vector<std::wstring> &listOfBadWord);

		/// 
		void run() override;

		/// 
		/// <param name="letter">
		/// @return </param>
	private:
		bool letterIsInAlphabet(wchar_t letter);

	public:
		virtual void setText(const std::wstring &text);

		virtual std::wstring getText();

		virtual std::wstring getParsedText();
	};

}
