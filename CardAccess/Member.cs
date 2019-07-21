using System;
using System.Collections.Generic;
using System.Text;

namespace CardAccess
{
    class Member : IMember
    {
        public static IList<IMember> Refresh()
        {
            var mCol = new List<IMember>();
            mCol.Add(new Member { MemberNo = "1", FirstName = "Linus", LastName = "Björnlund", TagNumber = "34234836567", Access = new List<string> { "Maskin Svets 1", "Maskin Svets 2", "Maskin svarv" } });
            mCol.Add(new Member { MemberNo = "2", FirstName = "Johan", LastName = "Slottner", TagNumber = "836620464462", Access = new List<string> { } });
            return mCol;
        }
        public string MemberNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TagNumber { get; set; }
        public IList<string> Access { get; set; }
    }
}
