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

namespace Jitendex.JapaneseTextUtils;

public static class VerbTransform
{
    public static string? VerbToMasuStem(this string text)
        => MakeStem(text, GetMasuStemLast(text));

    public static string? VerbToTeStem(this string text)
        => MakeStem(text, GetTeStemLast(text));

    private static string? MakeStem(string text, char? stemLast)
        => stemLast is null ? null : string.Create
        (
            length: text.Length,
            state: (Text: text, StemLast: stemLast.Value),
            action: static (destination, state) =>
            {
                int finalIndex = state.Text.Length - 1;
                for (int i = 0; i < finalIndex; i++)
                {
                    destination[i] = state.Text[i];
                }
                destination[finalIndex] = state.StemLast;
            }
        );

    private static char? GetMasuStemLast(ReadOnlySpan<char> text)
        => text.IsEmpty ? default : text[^1] switch
        {
            'う' => 'い',
            'く' => 'き',
            'ぐ' => 'ぎ',
            'す' => 'し',
            'ず' => 'じ',
            'つ' => 'ち',
            'ぬ' => 'に',
            'ぶ' => 'び',
            'む' => 'み',
            'る' => 'り',
            _ => null
        };

    private static char? GetTeStemLast(ReadOnlySpan<char> text)
        => text.IsEmpty ? default : text[^1] switch
        {
            'う' => 'っ',
            'く' => 'い',
            'ぐ' => 'い',
            'す' => 'し',
            'ず' => 'じ',
            'つ' => 'っ',
            'ぬ' => 'ん',
            'ぶ' => 'ん',
            'む' => 'ん',
            'る' => 'っ',
            _ => null
        };
}
