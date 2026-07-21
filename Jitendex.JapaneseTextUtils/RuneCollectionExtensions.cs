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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jitendex.JapaneseTextUtils;

public static class RuneCollectionExtensions
{
    public static string FastToString(this ReadOnlySpan<Rune> runes)
        => string.Create
        (
            length: runes.SumUtf16SequenceLengths(),
            state: runes,
            action: static (destination, state) =>
            {
                int charsWritten = 0;
                foreach (var rune in state)
                {
                    charsWritten += rune.EncodeToUtf16(destination[charsWritten..]);
                }
            }
        );

    public static int SumUtf16SequenceLengths(this ReadOnlySpan<Rune> runes)
    {
        int sum = 0;
        foreach (var rune in runes)
        {
            sum += rune.Utf16SequenceLength;
        }
        return sum;
    }

    public static string FastToString(this Span<Rune> runes)
        => FastToString((ReadOnlySpan<Rune>)runes);

    public static string FastToString(this IReadOnlyList<Rune> runes)
        => string.Create
        (
            length: runes.Sum(static rune => rune.Utf16SequenceLength),
            state: runes,
            action: static (destination, state) =>
            {
                int charsWritten = 0;
                foreach (var rune in state)
                {
                    charsWritten += rune.EncodeToUtf16(destination[charsWritten..]);
                }
            }
        );

    public static string FastToString(this IList<Rune> runes)
        => FastToString((IReadOnlyList<Rune>)runes);
}
