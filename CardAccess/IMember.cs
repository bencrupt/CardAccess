using System.Collections.Generic;

namespace CardAccess
{
    interface IMember
    {
        IList<string> Access { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string MemberNo { get; set; }
        string TagNumber { get; set; }
    }
}