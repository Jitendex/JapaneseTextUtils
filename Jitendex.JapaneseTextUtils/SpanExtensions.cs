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

internal static class SpanExtensions
{
    public static bool All<T>(this ReadOnlySpan<T> items, Func<T, bool> predicate)
    {
        foreach (var item in items)
        {
            if (!predicate(item))
            {
                return false;
            }
        }
        return true;
    }

    public static bool Any<T>(this ReadOnlySpan<T> items, Func<T, bool> predicate)
    {
        foreach (var item in items)
        {
            if (predicate(item))
            {
                return true;
            }
        }
        return false;
    }
}
