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

public static class RendakuTransform
{
    public static string[] ToRendakuForms(this ReadOnlySpan<char> text)
    {
        var rendakuChars = FirstToRendakuChars(text);
        if (rendakuChars.Length == 0)
        {
            return [];
        }
        var rendakuForms = new string[rendakuChars.Length];
        for (int i = 0; i < rendakuChars.Length; i++)
        {
            rendakuForms[i] = string.Create
            (
                length: text.Length,
                state: new State { Text = text, RendakuChar = rendakuChars[i] },
                action: static (destination, state) =>
                {
                    destination[0] = state.RendakuChar;
                    for (int j = 1; j < state.Text.Length; j++)
                    {
                        destination[j] = state.Text[j];
                    }
                }
            );
        }
        return rendakuForms;
    }

    private readonly ref struct State
    {
        public readonly ReadOnlySpan<char> Text { get; init; }
        public readonly char RendakuChar { get; init; }
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
            'カ' => ['ガ'],
            'キ' => ['ギ'],
            'ク' => ['グ'],
            'ケ' => ['ゲ'],
            'コ' => ['ゴ'],
            'サ' => ['ザ'],
            'シ' => ['ジ'],
            'ス' => ['ズ'],
            'セ' => ['ゼ'],
            'ソ' => ['ゾ'],
            'タ' => ['ダ'],
            'チ' => ['ヂ', 'ジ'],
            'ツ' => ['ヅ', 'ズ'],
            'テ' => ['デ'],
            'ト' => ['ド'],
            'ハ' => ['バ', 'パ'],
            'ヒ' => ['ビ', 'ピ'],
            'フ' => ['ブ', 'プ'],
            'ヘ' => ['ベ', 'ペ'],
            'ホ' => ['ボ', 'ポ'],
            _ => []
        };
}
