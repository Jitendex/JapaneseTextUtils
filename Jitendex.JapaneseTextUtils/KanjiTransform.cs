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

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jitendex.JapaneseTextUtils;

public static class KanjiTransform
{
    public static Rune[] IterationMarksToKanji(this IList<Rune> runes)
    {
        var normalizedRunes = new Rune[runes.Count];

        // Replace iteration marks (々) and doubled iteration marks (々々) with their respective kanji.
        for (int i = 0; i < runes.Count; i++)
        {
            var currentRune = runes[i];
            var nextRune = runes.ElementAtOrDefault(i + 1);

            if (i > 1 && IsIterationMark(currentRune) && IsIterationMark(nextRune))
            {
                // Double repeater
                normalizedRunes[i] = normalizedRunes[i - 2];
                i++;
                normalizedRunes[i] = normalizedRunes[i - 2];
            }
            else if (i > 0 && IsIterationMark(currentRune))
            {
                // Single repeater
                normalizedRunes[i] = normalizedRunes[i - 1];
            }
            else
            {
                // No repeater
                normalizedRunes[i] = currentRune;
            }
        }

        return normalizedRunes;
    }

    private static bool IsIterationMark(Rune c) => c.Value switch
    {
        '々' or '〻' => true,
        _ => false
    };
}
