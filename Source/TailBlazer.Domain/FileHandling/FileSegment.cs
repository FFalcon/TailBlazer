using System;

namespace TailBlazer.Domain.FileHandling
{
    public class FileSegment : IEquatable<FileSegment>
    {
        public int Index { get; }
        public long Start { get;  }
        public long End { get;  }
        public FileSegmentType Type { get;  }
        public long Size => End - Start;

        public FileSegment(int index, long start, long end, FileSegmentType type)
        {
            Index = index;
            Start = start;
            End = end;
            Type = type;
        }

        public FileSegment(FileSegment previous, long end)
        {
            Index = previous.Index;
            Start = previous.Start;
            End = end;
            Type = previous.Type;
        }

        #region Equality

        public static bool operator ==(FileSegment left, FileSegment right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FileSegment left, FileSegment right)
        {
            return !Equals(left, right);
        }

        public bool Equals(FileSegment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Index == other.Index && Start == other.Start && End == other.End && Type == other.Type;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FileSegment) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Index;
                hashCode = (hashCode*397) ^ Start.GetHashCode();
                hashCode = (hashCode*397) ^ End.GetHashCode();
                hashCode = (hashCode*397) ^ (int) Type;
                return hashCode;
            }
        }

        #endregion

        public override string ToString()
        {
            return $"{Index} {Type}. {Start}->{End} [{Size.FormatWithAbbreviation()}] ";
        }
    }
}