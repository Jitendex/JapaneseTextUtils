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
using System.Collections.Immutable;

namespace Jitendex.JapaneseTextUtils;

public static class RendakuTransform
{
    public static ImmutableArray<string> ToRendakuForms(this string text)
    {
        var rendakuChars = FirstToRendakuChars(text);
        if (rendakuChars.Length == 0)
        {
            return [];
        }
        var builder = ImmutableArray.CreateBuilder<string>(rendakuChars.Length);
        foreach (var rendakuChar in rendakuChars)
        {
            builder.Add(string.Create
            (
                length: text.Length,
                state: (text, rendakuChar),
                action: static (destination, state) =>
                {
                    destination[0] = state.rendakuChar;
                    for (int i = 1; i < state.text.Length; i++)
                    {
                        destination[i] = state.text[i];
                    }
                }
            ));
        }
        return builder.MoveToImmutable();
    }

    private static ReadOnlySpan<char> FirstToRendakuChars(ReadOnlySpan<char> x)
        => x.Length == 0 ? [] : x[0] switch
        {
            'か' => ['が'],
            'き' => ['ぎ'],
            'く' => ['ぐ'],
            'け' => ['げ'],
            'こ' => ['ご'],
            'さ' => ['ざ'],
            'し' => ['じ'],
            'す' => ['ず'],
            'せ' => ['ぜ'],
            'そ' => ['ぞ'],
            'た' => ['だ'],
            'ち' => ['ぢ', 'じ'],
            'つ' => ['づ', 'ず'],
            'て' => ['で'],
            'と' => ['ど'],
            'は' => ['ば', 'ぱ'],
            'ひ' => ['び', 'ぴ'],
            'ふ' => ['ぶ', 'ぷ'],
            'へ' => ['べ', 'ぺ'],
            'ほ' => ['ぼ', 'ぽ'],
            _ => []
        };
}
