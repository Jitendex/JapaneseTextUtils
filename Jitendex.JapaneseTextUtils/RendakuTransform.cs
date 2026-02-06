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

using System.Collections.Immutable;
using System.Linq;

namespace Jitendex.JapaneseTextUtils;

public static class RendakuTransform
{
    public static ImmutableArray<string> ToRendakuForms(this string text) => FirstToRendakuChars(text) switch
    {
        [] => [],
        var rendakuChars => rendakuChars
            .Select(rendaku => rendaku + text[1..])
            .ToImmutableArray()
    };

    private static ImmutableArray<char> FirstToRendakuChars(string x) => x.FirstOrDefault() switch
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
