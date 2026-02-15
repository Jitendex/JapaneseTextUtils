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

    public static string KatakanaToHiragana(this string text) => KatakanaToHiragana((ReadOnlySpan<char>)text);
    public static string HiraganaToKatakana(this string text) => HiraganaToKatakana((ReadOnlySpan<char>)text);

    public static string KatakanaToHiragana(this Span<char> text) => KatakanaToHiragana((ReadOnlySpan<char>)text);
    public static string HiraganaToKatakana(this Span<char> text) => HiraganaToKatakana((ReadOnlySpan<char>)text);

    public static string KatakanaToHiragana(this Span<Rune> text) => KatakanaToHiragana((ReadOnlySpan<Rune>)text);
    public static string HiraganaToKatakana(this Span<Rune> text) => HiraganaToKatakana((ReadOnlySpan<Rune>)text);

    private static int HiraganaToKatakana(int x) => x switch
    {
        (>= 0x3041) and (<= 0x3096) => x + 0x60,  // ぁ through ゖ
            0x309D   or     0x309E  => x + 0x60,  // ゝ and ゞ
                                  _ => x
    };

    private static int KatakanaToHiragana(int x) => x switch
    {
        (>= 0x30A1) and (<= 0x30F6) => x - 0x60,  // ァ through ヶ
            0x30FD   or     0x30FE  => x - 0x60,  // ヽ and ヾ
                                  _ => x
    };

    public static string KatakanaToHiragana(this ReadOnlySpan<char> text)
        => string.Create
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

    public static string HiraganaToKatakana(this ReadOnlySpan<char> text)
        => string.Create
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

    public static string KatakanaToHiragana(this ReadOnlySpan<Rune> text)
        => string.Create
        (
            length: text.SumUtf16SequenceLengths(),
            state: text,
            action: static (destination, state) =>
            {
                int charsWritten = 0;
                foreach (var rune in state)
                {
                    var transformedRune = rune.KatakanaToHiragana();
                    charsWritten += transformedRune.EncodeToUtf16(destination[charsWritten..]);
                }
            }
        );

    public static string HiraganaToKatakana(this ReadOnlySpan<Rune> text)
        => string.Create
        (
            length: text.SumUtf16SequenceLengths(),
            state: text,
            action: static (destination, state) =>
            {
                int charsWritten = 0;
                foreach (var rune in state)
                {
                    var transformedRune = rune.HiraganaToKatakana();
                    charsWritten += transformedRune.EncodeToUtf16(destination[charsWritten..]);
                }
            }
        );
}
