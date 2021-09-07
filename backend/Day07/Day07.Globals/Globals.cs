using System;

namespace Day07.Globals
{
    public static class Globals
    {
        private static readonly Guid _currentUser = Guid.NewGuid();
        //temporary solution
        public static Guid CurrentUser { get => _currentUser; }
    }
}
