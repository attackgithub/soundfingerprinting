﻿namespace SoundFingerprinting.LCS
{
    using System.Collections.Generic;
    using System.Linq;
    using SoundFingerprinting.Query;

    internal static class OverlappingRegionFilter
    {
        public static IEnumerable<Matches> MergeOverlappingSequences(List<Matches> sequences, double permittedGap)
        {
            for (int current = 0; current < sequences.Count; ++current)
            {
                for (int next = current + 1; next < sequences.Count; ++next)
                {
                    if (sequences[current].TryCollapseWith(sequences[next], permittedGap, out var c))
                    {
                        sequences.RemoveAt(next);
                        sequences.RemoveAt(current);
                        sequences.Add(c);
                        current = -1;
                        break;
                    }
                }
            }

            return sequences.OrderByDescending(sequence => sequence.EntriesCount);
        }
    }
}
