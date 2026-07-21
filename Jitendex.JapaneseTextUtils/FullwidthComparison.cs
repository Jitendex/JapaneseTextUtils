/*
Copyright (c) 2026 Stephen Kraus
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

public static class FullwidthComparison
{
    public static bool IsFullwidthAlphanumeric(this char c) => IsFullwidthAlphanumeric((int)c);
    public static bool IsFullwidthAlphanumeric(this Rune c) => IsFullwidthAlphanumeric(c.Value);
    public static bool IsFullwidthAlphanumericOrDefault(this Rune c) => c.IsFullwidthAlphanumeric() || c == default;

    public static bool ContainsFullwidthAlphanumeric(this string text) => text.Any(IsFullwidthAlphanumeric);

    public static bool IsAllFullwidthAlphanumeric(this string text) => text.All(IsFullwidthAlphanumeric);
    public static bool AreAllFullwidthAlphanumeric(this IEnumerable<Rune> runes) => runes.All(IsFullwidthAlphanumeric);
    public static bool AreAllFullwidthAlphanumeric(this ReadOnlySpan<Rune> runes) => runes.All(IsFullwidthAlphanumeric);

    #pragma warning disable format
    private static bool IsFullwidthAlphanumeric(int c) => c switch
    {
        (>= 0xFF10) and (<= 0xFF19) => true, // ０-９
        (>= 0xFF21) and (<= 0xFF3A) => true, // Ａ-Ｚ
        (>= 0xFF41) and (<= 0xFF5A) => true, // ａ-ｚ
                                  _ => false
    };
    #pragma warning restore format
}
