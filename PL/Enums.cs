using System;
using System.Collections;
using System.Collections.Generic;

namespace PL
{
    internal class EngineersTeam : IEnumerable
    {
        static readonly IEnumerable<BO.EngineerLevelEnum> s_enums =
            (Enum.GetValues(typeof(BO.EngineerLevelEnum)) as IEnumerable<BO.EngineerLevelEnum>)!;

        public IEnumerator GetEnumerator() => s_enums.GetEnumerator();

        //        namespace PL;
        //    internal class SemestersCollection : IEnumerable
        //    {
        //        static readonly IEnumerable<BO.SemesterNames> s_enums =
        //    (Enum.GetValues(typeof(BO.SemesterNames)) as IEnumerable<BO.SemesterNames>)!;

        //        public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
        //    }

    }
}