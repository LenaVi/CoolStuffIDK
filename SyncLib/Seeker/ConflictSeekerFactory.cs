﻿namespace SyncLib.Seeker
{
    public class ConflictSeekerFactory
    {
        public static BaseSeeker GetConflictSeeker(bool noDelete, string master, string slave)
        {
            switch (noDelete)
            {
                case true:
                    return new DefaultConflictSeeker(master, slave);
                default:
                    return new RemoveConflictSeeker(master, slave);
            }
        }
    }
}
