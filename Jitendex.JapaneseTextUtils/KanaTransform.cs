/*
Copyright (c) 2025-2026 Stephen Kraus
SPDX-License-Identifier: GPL-3.0-or-later

This file is part of JapaneseTextUtils.

JapaneseTextUtils is free software: you can redistribute it and/or modify it under the
terms of the GNU General Public License as published by the Free Software Foundation,
either version 3 of the License, or (at your option) any later version.

JapaneseTextUtils is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with JapaneseTextUtils.
If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Text;

namespace Jitendex.JapaneseTextUtils;

public static class KanaTransform
{
    public static char KatakanaToHiragana(this char c) => (char)KatakanaToHiragana((int)c);
    public static char HiraganaToKatakana(this char c) => (char)HiraganaToKatakana((int)c);

    public static Rune KatakanaToHiragana(this Rune c) => new(KatakanaToHiragana(c.Value));
    public static Rune HiraganaToKatakana(this Rune c) => new(HiraganaToKatakana(c.Value));

    private static int KatakanaToHiragana(int x) => IsConvertibleToHiragana(x) ? x - 0x60 : x;
    private static int HiraganaToKatakana(int x) => IsConvertibleToKatakana(x) ? x + 0x60 : x;

    private static bool IsConvertibleToHiragana(char x) => IsConvertibleToHiragana((int)x);
    private static bool IsConvertibleToKatakana(char x) => IsConvertibleToKatakana((int)x);

    private static bool IsConvertibleToHiragana(int x) => x switch
    {
        (>= 0x30A1) and (<= 0x30F6) => true,  // ァ through ヶ
            0x30FD   or     0x30FE  => true,  // ヽ and ヾ
                                  _ => false
    };

    private static bool IsConvertibleToKatakana(int x) => x switch
    {
        (>= 0x3041) and (<= 0x3096) => true,  // ぁ through ゖ
            0x309D   or     0x309E  => true,  // ゝ and ゞ
                                  _ => false
    };

    public static string KatakanaToHiragana(this string text)
    {
        if (!IsConvertibleToHiragana(text))
        {
            return text;
        }
        return string.Create
        (
            length: text.Length,
            state: text,
            action: static (destination, state) =>
            {
                for (int i = 0; i < state.Length; i++)
                {
                    destination[i] = state[i].KatakanaToHiragana();
                }
            }
        );
    }

    public static string HiraganaToKatakana(this string text)
    {
        if (!IsConvertibleToKatakana(text))
        {
            return text;
        }
        return string.Create
        (
            length: text.Length,
            state: text,
            action: static (destination, state) =>
            {
                for (int i = 0; i < state.Length; i++)
                {
                    destination[i] = state[i].HiraganaToKatakana();
                }
            }
        );
    }

    public static ReadOnlySpan<char> KatakanaToHiragana(this ReadOnlySpan<char> text)
    {
        if (!IsConvertibleToHiragana(text))
        {
            return text;
        }
        Span<char> destination = new char[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
            destination[i] = text[i].KatakanaToHiragana();
        }
        return destination;
    }

    public static ReadOnlySpan<char> HiraganaToKatakana(this ReadOnlySpan<char> text)
    {
        if (!IsConvertibleToKatakana(text))
        {
            return text;
        }
        Span<char> destination = new char[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
            destination[i] = text[i].HiraganaToKatakana();
        }
        return destination;
    }

    public static ReadOnlySpan<Rune> KatakanaToHiragana(this ReadOnlySpan<Rune> text)
    {
        if (!IsConvertibleToHiragana(text))
        {
            return text;
        }
        Span<Rune> destination = new Rune[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
            destination[i] = text[i].KatakanaToHiragana();
        }
        return destination;
    }

    public static ReadOnlySpan<Rune> HiraganaToKatakana(this ReadOnlySpan<Rune> text)
    {
        if (!IsConvertibleToKatakana(text))
        {
            return text;
        }
        Span<Rune> destination = new Rune[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
            destination[i] = text[i].HiraganaToKatakana();
        }
        return destination;
    }

    private static bool IsConvertibleToHiragana(this ReadOnlySpan<char> characters)
    {
        foreach (var character in characters)
        {
            if (IsConvertibleToHiragana(character))
            {
                return true;
            }
        }
        return false;
    }

    private static bool IsConvertibleToKatakana(this ReadOnlySpan<char> characters)
    {
        foreach (var character in characters)
        {
            if (IsConvertibleToKatakana(character))
            {
                return true;
            }
        }
        return false;
    }

    private static bool IsConvertibleToHiragana(this ReadOnlySpan<Rune> runes)
    {
        foreach (var rune in runes)
        {
            if (IsConvertibleToHiragana(rune.Value))
            {
                return true;
            }
        }
        return false;
    }

    private static bool IsConvertibleToKatakana(this ReadOnlySpan<Rune> runes)
    {
        foreach (var rune in runes)
        {
            if (IsConvertibleToKatakana(rune.Value))
            {
                return true;
            }
        }
        return false;
    }
}
